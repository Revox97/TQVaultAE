using System.Text;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    internal class ArcRecordDecoder
    {
        public static async Task<ArcFileRecord> ReadRecordAsync(BinaryReader reader, int fileRecordCount, ArcPartEntry[] partEntries)
        {
            ArcDirEntrySpan dirEntry = ReadArcDirectoryEntry(reader);

            ArcFileRecord record = new()
            {
                StorageType = dirEntry.StorageType,
                FileOffset = dirEntry.FileOffset,
                CompressedSize = dirEntry.CompressedSize,
                RealSize = dirEntry.RealSize,
                Parts = dirEntry.NumberOfParts > 0 && dirEntry.FirstPart >= 0 && dirEntry.FirstPart + dirEntry.NumberOfParts <= fileRecordCount
                    ? new ArcPartEntry[dirEntry.NumberOfParts] : []
            };

            if (record.Parts.Length > 0)
            {
                for (int k = 0; k < dirEntry.NumberOfParts; k++)
                {
                    int partIndex = k + dirEntry.FirstPart;
                    record.Parts[k] = partEntries[partIndex];
                }
            }

            long currentPosition = reader.BaseStream.Position;
            ArcFileRecord result = await ReadRecordPropertiesFromRecordAsync(reader, record).ConfigureAwait(false);
            reader.BaseStream.Position = currentPosition;

            return result;
        }

        internal static async Task<ArcFileRecord> ReadRecordPropertiesFromRecordAsync(BinaryReader reader, ArcFileRecord record)
        {
            int start = record.FileOffset;
            reader.BaseStream.Position = start;

            if (record.StorageType is ArcStorageType.Compressed)
                return await ReadCompressedArcRecordAsync(reader, record).ConfigureAwait(false);

            if (record.StorageType is ArcStorageType.Real)
                return await ReadUnCompressedArcRecordAsync(reader, record).ConfigureAwait(false);

            return record;
        }

        private static async Task<ArcFileRecord> ReadUnCompressedArcRecordAsync(BinaryReader reader, ArcFileRecord record)
        {
            byte[] raw = reader.ReadBytes(record.RealSize);
            return record;
        }

        private static async Task<ArcFileRecord> ReadCompressedArcRecordAsync(BinaryReader reader, ArcFileRecord record)
        {
            reader.BaseStream.Position += 2;
            byte[] compressedData = reader.ReadBytes(record.CompressedSize - 2);
            byte[] data = compressedData.Length > 0 ? Compression.DecompressZlib(compressedData) : [];

            using MemoryStream stream = new(data);
            using BinaryReader recordReader = new(stream);

            byte[] contentType = recordReader.ReadBytes(3);

            if (contentType[0] == 0x54 && contentType[1] == 0x45 && contentType[2] == 0x58) // TEX File read raw data
            {
                // TODO Read TEXT file
                recordReader.BaseStream.Position -= 3;
                record.Content = ReadTexContent(stream);
                record.ContentType = ArcRecordType.TexFile;
                return record;
            }

            if (contentType[0] == 0xFF && contentType[1] == 0xFE) // String Content
            {
                record.Content = ReadStringEntries(recordReader);
                record.ContentType = ArcRecordType.StringCollection;
                return record;
            }

            return record;
        }

        private static TexFile ReadTexContent(MemoryStream stream)
        {
            return TexFile.Read(stream);
        }

        private static Dictionary<string, string> ReadStringEntries(BinaryReader reader)
        {
            reader.BaseStream.Position -= 1;
            byte[] dataBytes = reader.ReadBytes((int)reader.BaseStream.Length);
            string result = Encoding.Unicode.GetString(dataBytes);

            Dictionary<string, string> entries = [];

            foreach (string line in result.Split(Environment.NewLine))
            {
                if (line.StartsWith("//"))
                    continue;

                string[] content = line.Split('=');

                if (content.Length != 2)
                    continue; // Log error

                // Some entries seem to exist twice for some reason
                // TODO Handle duplicates correct
                if (entries.Keys.Any(x => x == content[0]))
                    continue;

                entries.Add(content[0], content[1]);
            }

            return entries;
        }

        private static ArcDirEntrySpan ReadArcDirectoryEntry(BinaryReader reader)
        {
            ArcStorageType storageType = (ArcStorageType)reader.ReadInt32();
            int fileOffset = reader.ReadInt32();
            int compressedSize = reader.ReadInt32();
            int realSize = reader.ReadInt32();

            reader.BaseStream.Position += 12; // Skip unnecessary data -> For the future find out what it is and add it

            int numberOfParts = reader.ReadInt32();
            int firstPart = reader.ReadInt32();
            int filenameLength = reader.ReadInt32();
            int filenameOffset = reader.ReadInt32();

            return new ArcDirEntrySpan()
            {
                StorageType = storageType,
                FileOffset = fileOffset,
                CompressedSize = compressedSize,
                RealSize = realSize,
                NumberOfParts = numberOfParts,
                FirstPart = firstPart,
                FilenameLength = filenameLength,
                FilenameOffset = filenameOffset
            };
        }

        private readonly struct ArcDirEntrySpan
        {
            public ArcStorageType StorageType { get; init; }
            public int FileOffset { get; init; }
            public int CompressedSize { get; init; }
            public int RealSize { get; init; }
            public int NumberOfParts { get; init; }
            public int FirstPart { get; init; }
            public int FilenameLength { get; init; }
            public int FilenameOffset { get; init; }
        }

        private static string? ReadArcNullTerminatedString(BinaryReader reader)
        {
            List<byte> result = [];

            while (reader.BaseStream.Position + result.Count < reader.BaseStream.Length)
            {
                byte currentByte = reader.ReadByte();

                if (currentByte == 0x00)
                    return result.Count > 1 ? Encoding.ASCII.GetString([.. result]) : string.Empty;

                if (currentByte == 0x03)
                    return result.Count > 0 ? Encoding.ASCII.GetString([.. result]) : null;

                result.Add(currentByte);
            }

            // No terminator found - return what we have
            return result.Count > 0 ? Encoding.ASCII.GetString([.. result]) : null;
        }
    }
}
