using System.Text;
using TQVaultAE.FileFormats.Chr;
using TQVaultAE.TitanQuestDataProviders.Decoders.ChrValueProviders;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    // TODO works, but files always seem to be structured in the same way, so parsing correct BlockNames should be possible
    #region CHRDOCU
    // CHR FILE STRUCTURE
    // headerVersion - Int32
    // Followed by data blocks
    // usually label size (Int32) followed by the label (string)
    // Then a primitive type
    // A label can mark the start of a complex block (begin_block)
    // complex blocks can contain other blocks and/or primitives
    // complex blocks end on end_block label

    // File Structure:
    // headerVersion: Int32
    // playerCharacterClass: String
    // uniqueId: Guid
    // streamData: Stream
    // playerClassTag: String
    // playerLevel: Int32
    // playerVersion: Int32
    // BLOCK0 #PlayerMetadata
    // BLOCK1 #Skills
    // BLOCK2 #??UIState??
    // BLOCK3 #PlayerLevel
    // BLOCK4 #Temp (not sure what it is used for)
    // BLOCK5 #PlayerStatistics
    // controllerStreamed: bool
    // BLOCK6 #PlayerInventory
    // BLOCK7 #??Unknown??
    // BLOCK8 #PlayerEquipment
    // BLOCK9 #Skillbar
    // Description: Unknown
    #endregion CHRDOCU

    internal class ChrDecoder
    {
        private const string Label_BeginBlock = "begin_block";
        private const string Label_EndBlock = "end_block";

        internal static async Task<ChrFile> DecodeAsync(FileStream stream, string path)
        {
            // TODO verify it is returning the correct type.
            string fileType = path[^3..].ToUpperInvariant();

            using BinaryReader reader = new(stream, Encoding.UTF8);
            ChrBlock root = ParseCharacterFile(stream, reader, fileType);

            return new ChrFile(path, root);
        }

        // Find better name for CHR files, that represents all of them. CHR is not a good naming here.
        private static ChrLikeValueProvider? s_valueProvider;

        public static ChrBlock ParseCharacterFile(FileStream stream, BinaryReader reader, string fileType)
        {
            if (fileType != "CHR" && fileType != "DXB" && fileType != "DXG")
                throw new ArgumentException("File type is not supported. Use CHR, DXB, or DXG.");

            s_valueProvider = fileType switch
            {
                "CHR" => new ChrValueProvider(),
                "DXB" => new DxbValueProvider(),
                "DXG" or _ => new DxgValueProvider(),
            };

            ChrBlock root = new("Root");

            int blockCount = 0;

            if (fileType == "DXB")
                reader.BaseStream.Position += 4; // Skip CRC header

            while (stream.Position < stream.Length - 4)  // DXB Workaround
                ParseNextToken(reader, root, ref blockCount);

            return root;
        }

        private static void ParseNextToken(BinaryReader reader, ChrBlock currentBlock, ref int blockCount, int level = 0)
        {
            try
            {
                if (reader.BaseStream.Position >= reader.BaseStream.Length)
                    return;

                int labelLength = reader.ReadInt32();

                if (labelLength <= 0 || labelLength > 512) // Fallback in case of corrupted or invalid data
                    return;

                byte[] labelBytes = reader.ReadBytes(labelLength);
                string label = Encoding.UTF8.GetString(labelBytes);

                if (label == Label_BeginBlock)
                {
                    reader.BaseStream.Position += 4;
                    ChrBlock subBlock = new($"Block_{level}-{blockCount++}");

                    currentBlock.Children.Add(subBlock);

                    int subBlockCount = 0;
                    while (true)
                    {
                        long currentPos = reader.BaseStream.Position;
                        int nextLength = reader.ReadInt32();
                        byte[] nextLabelBytes = reader.ReadBytes(nextLength);
                        string nextLabel = Encoding.UTF8.GetString(nextLabelBytes);

                        if (nextLabel == Label_EndBlock)
                        {
                            List<byte> trailingBytes = [];

                            while (true)
                            {
                                // Workaround for DXB files, as the last block has no trailing bytes.
                                if (reader.BaseStream.Length - reader.BaseStream.Position < 4)
                                    break;

                                trailingBytes.Add(reader.ReadByte());

                                if (trailingBytes.Count >= 4
                                    && trailingBytes[trailingBytes.Count - 1] == 0x00
                                    && trailingBytes[trailingBytes.Count - 2] == 0x00
                                    && trailingBytes[trailingBytes.Count - 3] == 0x00
                                    && trailingBytes[trailingBytes.Count - 4] != 0x00)
                                {
                                    reader.BaseStream.Position = reader.BaseStream.Position - 4;
                                    break;
                                }
                            }

                            break;
                        }

                        reader.BaseStream.Position = currentPos;
                        ParseNextToken(reader, subBlock, ref subBlockCount, level + 1);
                    }
                }
                else
                {
                    byte[] dataValue = s_valueProvider!.ReadPrimitiveValue(reader, label, out Type type);
                    currentBlock.Children.Add(new ChrBlock(label, dataValue, type));
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
