using System.Text;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    // TODO Handle file already read situations, but this belongs into arcprovider not here
    // TODO Also there seems to be a record ID, that might be needed
    internal sealed class ArcDecoder
    {
        private const int MinimumFileLength = 0x21;
        private const int EntryCountHeaderOffset = 0x08;
        private const int PartCountHeaderOffset = 0x0C;
        private const int TocOffsetHeaderOffset = 0x18;
        private const int Ascii_A = 0x41;
        private const int Ascii_R = 0x52;
        private const int Ascii_C = 0x43;
        private const int NullTeminator = 0x00;
        // 0x03 marker indicates inactive/null file
        private const int InactiveOrNullFileIndicator = 0x03;
        private const int FileRecordSize = 44;

        internal static async Task<ArcFile> DecodeAsync(FileStream stream, string path)
        {
            if (stream.Length < MinimumFileLength)
                throw new InvalidDataException($"File must have a minimum length of {MinimumFileLength}.");

            using BinaryReader reader = new(stream);
            return await DecodeRawContentToArcAsync(reader, path).ConfigureAwait(false);
        }

        private static async Task<ArcFile> DecodeRawContentToArcAsync(BinaryReader reader, string path)
        {
            ArcHeaders headers = ReadHeaders(reader);

            bool isTocOffsetValid = headers.PartEntryCount <= 0 || reader.BaseStream.Length >= headers.TocOffset + 12;
            if (!isTocOffsetValid)
                return null!;

            int partsSize = headers.PartEntryCount * 12;
            reader.BaseStream.Position = headers.TocOffset;
            ArcPartEntry[] arcPartEntries = ReadArcPartEntries(reader, headers.PartEntryCount);

            int fileRecordsSize = FileRecordSize * headers.NumberOfFileRecords;
            long fileRecordsStart = reader.BaseStream.Length - fileRecordsSize;
            reader.BaseStream.Position = fileRecordsStart;

            ArcFileRecord[] fileRecordEntries = headers.NumberOfFileRecords > 0 && fileRecordsStart < reader.BaseStream.Length
                    ? await ReadFileRecordEntries(reader, headers.NumberOfFileRecords, arcPartEntries).ConfigureAwait(false) : [];

            int fileNamesOffset = headers.TocOffset + partsSize;
            long fileNamesSize = fileRecordsStart - fileNamesOffset;

            if (fileNamesSize > 0 && fileNamesOffset >= 0 && fileNamesOffset < reader.BaseStream.Length)
            {
                reader.BaseStream.Position = fileNamesOffset;
                ReadFileNamesToDirectoryEntries(reader, fileRecordEntries, headers.NumberOfFileRecords);
            }

            return new ArcFile()
            {
                FileName = path,
                Records = [.. fileRecordEntries]
            };
        }

        private static ArcHeaders ReadHeaders(BinaryReader reader)
        {
            byte[] fileType = reader.ReadBytes(3);
            if (fileType[0] != Ascii_A || fileType[1] != Ascii_R || fileType[2] != Ascii_C)
                throw new InvalidDataException("File must be of type ARC.");

            reader.BaseStream.Position = EntryCountHeaderOffset;
            int entryCount = reader.ReadInt32();
            reader.BaseStream.Position = PartCountHeaderOffset;
            int partCount = reader.ReadInt32();
            reader.BaseStream.Position = TocOffsetHeaderOffset;
            int tocOffset = reader.ReadInt32();

            return new ArcHeaders()
            {
                FileType = "ARC",
                NumberOfFileRecords = entryCount,
                PartEntryCount = partCount,
                TocOffset = tocOffset
            };
        }

        private static ArcPartEntry[] ReadArcPartEntries(BinaryReader reader, int partEntryCount)
        {
            ArcPartEntry[] partEntries = new ArcPartEntry[partEntryCount];

            for (int i = 0; i < partEntryCount; i++)
            {
                partEntries[i] = new ArcPartEntry()
                {
                    FileOffset = reader.ReadInt32(),
                    CompressedSize = reader.ReadInt32(),
                    RealSize = reader.ReadInt32()
                };
            }

            return partEntries;
        }

        private static async Task<ArcFileRecord[]> ReadFileRecordEntries(BinaryReader reader, int fileRecordCount, ArcPartEntry[] partEntries)
        {
            ArcFileRecord[] fileRecordEntries = new ArcFileRecord[fileRecordCount];

            for (int i = 0; i < fileRecordCount; i++)
                fileRecordEntries[i] = await ArcRecordDecoder.ReadRecordAsync(reader, fileRecordCount, partEntries).ConfigureAwait(false);

            return fileRecordEntries;
        }

        private static void ReadFileNamesToDirectoryEntries(BinaryReader reader, ArcFileRecord[] directoryEntries, int numberOfEntries)
        {
            for (int i = 0; i < numberOfEntries; i++)
            {
                bool hasActiveFileNameEntry = directoryEntries[i].IsActive;
                if (!hasActiveFileNameEntry)
                    continue;

                string? filename = ReadArcNullTerminatedString(reader);
                directoryEntries[i].FileName = filename is not null && filename.Length > 0 ? filename : $"Null File {i}";
            }
        }

        /// <summary>
        /// Reads a null-terminated ASCII string from a span, handling the 0x03 marker
        /// used in ARC files to mark inactive/null file entries.
        /// Matches the original algorithm behavior from ArcFileProvider.ReadARCToC.
        /// </summary>
        /// <param name="span">The span containing the data.</param>
        /// <param name="offset">Starting offset for reading.</param>
        /// <param name="maxBufferSize">Maximum buffer size (typically 2048 for ARC files).</param>
        /// <param name="bytesConsumed">Returns the number of bytes consumed including the terminator.</param>
        /// <returns>
        /// The ASCII string without the terminator, "Null File {index}" format for 0x03 markers,
        /// or null if offset is out of bounds.
        /// </returns>
        private static string? ReadArcNullTerminatedString(BinaryReader reader)
        {
            List<byte> result = [];

            while (reader.BaseStream.Position + result.Count < reader.BaseStream.Length)
            {
                byte currentByte = reader.ReadByte();

                if (currentByte == NullTeminator)
                    return result.Count > 1 ? Encoding.ASCII.GetString([.. result]) : string.Empty;

                if (currentByte == InactiveOrNullFileIndicator)
                    return result.Count > 0 ? Encoding.ASCII.GetString([.. result]) : null;

                result.Add(currentByte);
            }

            // No terminator found - return what we have
            return result.Count > 0 ? Encoding.ASCII.GetString([.. result]) : null;
        }
    }
}
