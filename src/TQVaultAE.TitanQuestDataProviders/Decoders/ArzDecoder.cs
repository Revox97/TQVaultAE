using System.Buffers.Binary;
using System.Text;
using TQVaultAE.FileFormats.Arz;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    // TODO Migrate to binary reader
    internal sealed class ArzDecoder
    {
        internal static async Task<ArzFile> DecodeAsync(FileStream stream, string path)
        {
            using BinaryReader reader = new(stream, Encoding.UTF8);
            ArzHeaders headers = ReadHeaders(reader);

            string[] infoItems = ReadDbrTable(reader, headers.InfoTableStart);
            ArzRecord[] records = await ReadRecordTableAsync(reader, headers.RecordTableStart, headers.RecordTableCount, infoItems, path).ConfigureAwait(false);

            ArzRecord root = await new ArzRecordStructureProvider().GetArzRecordStructureAsync([.. records]).ConfigureAwait(false);
            return new ArzFile(path, root);
        }

        // ARZ header file format
        // 0x000000 int32
        // 0x000004 int32 start of dbRecord table
        // 0x000008 int32 size in bytes of dbRecord table
        // 0x00000c int32 numEntries in dbRecord table
        // 0x000010 int32 start of string table
        // 0x000014 int32 size in bytes of string table
        private static ArzHeaders ReadHeaders(BinaryReader reader)
        {
            reader.BaseStream.Position += sizeof(int); // Skip first int - Is this the file version, if so dont skip

            // TODO Verify, that it is reading littleendian
            return new ArzHeaders()
            {
                RecordTableStart = reader.ReadInt32(),
                RecordTableSize = reader.ReadInt32(),
                RecordTableCount = reader.ReadInt32(),
                InfoTableStart = reader.ReadInt32(),
                InfoTableSize = reader.ReadInt32(),
            };
        }

        // Reads the whole string table into memory using ReadOnlySpan for zero-copy parsing.
        // Info?? Table Format:
        // first 4 bytes is the number of entries
        // then one string followed by another...
        private static string[] ReadDbrTable(BinaryReader reader, int start)
        {
            reader.BaseStream.Position = start;

            int itemCount = reader.ReadInt32();
            string[] dbrTable = new string[itemCount];

            for (int i = 0; i < itemCount; i++)
                dbrTable[i] = ReadString(reader);

            return dbrTable;
        }

        private static string ReadString(BinaryReader reader)
        {
            int stringLength = reader.ReadInt32();
            byte[] content = reader.ReadBytes(stringLength);
            return Encoding.UTF8.GetString(content);
        }

        private static async Task<ArzRecord[]> ReadRecordTableAsync(BinaryReader reader, int start, int recordCount, string[] infoRecords, string path)
        {
            reader.BaseStream.Position = start;
            ArzRecord[] records = new ArzRecord[recordCount];

            ArzRecordDecoder.Initialize(path, infoRecords);

            for (int i = 0; i < recordCount; ++i)
                records[i] = await ReadRecordAsync(reader).ConfigureAwait(false);

            return records;
        }

        private static async Task<ArzRecord> ReadRecordAsync(BinaryReader reader)
        {
            ArzRecord result = await ArzRecordDecoder.ReadRecordAsync(reader).ConfigureAwait(false);
            return await ArzRecordDecoder.ReadRecordPropertiesFromRecordAsync(result).ConfigureAwait(false);
        }
    }
}
