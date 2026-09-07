using System.Buffers.Binary;
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
        private const int PartEntrySize = 12;
        private const int FileRecordSize = 44;

        internal static async Task<ArcFile> DecodeAsync(byte[] content, string path)
        {
            return content.Length >= MinimumFileLength
                ? DecodeRawContentToArc(content.AsSpan(), path)
                : throw new InvalidDataException($"File must have a minimum length of {MinimumFileLength}.");
        }

        private static ArcFile DecodeRawContentToArc(Span<byte> content, string path)
        {
            ArcHeaders headers = ReadHeaders(content);

            bool isTocOffsetValid = headers.PartEntryCount <= 0 || content.Length >= headers.TocOffset + 12;
            if (!isTocOffsetValid)
                return null!;

            int partsSize = headers.PartEntryCount * 12;
            Span<byte> arcPartEntriesRaw = content[headers.TocOffset..(headers.TocOffset + partsSize)];
            ArcPartEntry[] arcPartEntries = ReadArcPartEntries(arcPartEntriesRaw, headers.PartEntryCount);

            int fileRecordsSize = FileRecordSize * headers.NumberOfFileRecords;
            int fileRecordsStart = content.Length - fileRecordsSize;
            Span<byte> recordsRaw = content[fileRecordsStart..];

            ArcFileRecordEntry[] fileRecordEntries = headers.NumberOfFileRecords > 0 && fileRecordsStart < content.Length
                    ? ReadFileRecordEntries(recordsRaw, headers.NumberOfFileRecords, arcPartEntries) : [];

            int fileNamesOffset = headers.TocOffset + partsSize;
            int fileNamesSize = fileRecordsStart - fileNamesOffset;

            if (fileNamesSize > 0 && fileNamesOffset >= 0 && fileNamesOffset < content.Length)
            {
                Span<byte> fileNamesContent = content[fileNamesOffset..(fileNamesOffset + fileNamesSize)];
                ReadFileNamesToDirectoryEntries(fileNamesContent, fileRecordEntries, headers.NumberOfFileRecords);
            }

            return new ArcFile()
            {
                FileName = path,
                Records = [.. fileRecordEntries]
            };
        }

        private static ArcHeaders ReadHeaders(Span<byte> content)
        {
            if (content[0] != Ascii_A || content[1] != Ascii_R || content[2] != Ascii_C)
                throw new InvalidDataException("File must be of type ARC.");

            int entryCount = BinaryPrimitives.ReadInt32LittleEndian(content[EntryCountHeaderOffset..]);
            int partCount = BinaryPrimitives.ReadInt32LittleEndian(content[PartCountHeaderOffset..]);
            int tocOffset = BinaryPrimitives.ReadInt32LittleEndian(content[TocOffsetHeaderOffset..]);

            return new ArcHeaders()
            {
                FileType = "ARC",
                NumberOfFileRecords = entryCount,
                PartEntryCount = partCount,
                TocOffset = tocOffset
            };
        }

        private static ArcPartEntry[] ReadArcPartEntries(Span<byte> content, int partEntryCount)
        {
            ArcPartEntry[] partEntries = new ArcPartEntry[partEntryCount];

            for (int i = 0; i < partEntryCount; i++)
            {
                int offset = i * PartEntrySize;
                partEntries[i] = new ArcPartEntry()
                {
                    FileOffset = BinaryPrimitives.ReadInt32LittleEndian(content[offset..]),
                    CompressedSize = BinaryPrimitives.ReadInt32LittleEndian(content[(offset + 4)..]),
                    RealSize = BinaryPrimitives.ReadInt32LittleEndian(content[(offset + 8)..]),
                };
            }

            return partEntries;
        }

        private static ArcFileRecordEntry[] ReadFileRecordEntries(Span<byte> content, int fileRecordCount, ArcPartEntry[] arcPartEntries)
        {
            ArcFileRecordEntry[] fileRecordEntries = new ArcFileRecordEntry[fileRecordCount];

            for (int i = 0; i < fileRecordCount; i++)
            {
                int offset = i * FileRecordSize;
                ArcDirEntrySpan dirEntry = ReadArcDirectoryEntry(content, offset);

                fileRecordEntries[i] = new ArcFileRecordEntry
                {
                    StorageType = dirEntry.StorageType,
                    FileOffset = dirEntry.FileOffset,
                    CompressedSize = dirEntry.CompressedSize,
                    RealSize = dirEntry.RealSize,
                    Parts = dirEntry.NumberOfParts > 0 && dirEntry.FirstPart >= 0 && dirEntry.FirstPart + dirEntry.NumberOfParts <= fileRecordCount
                        ? new ArcPartEntry[dirEntry.NumberOfParts] : []
                };

                if (fileRecordEntries[i].Parts.Length > 0)
                {
                    for (int k = 0; k < dirEntry.NumberOfParts; k++)
                    {
                        int partIndex = k + dirEntry.FirstPart;
                        fileRecordEntries[i].Parts[k] = arcPartEntries[partIndex];
                    }
                }
            }

            return fileRecordEntries;
        }

        private static ArcDirEntrySpan ReadArcDirectoryEntry(ReadOnlySpan<byte> span, int offset) => new()
        {
            StorageType = (ArcStorageType)BinaryPrimitives.ReadInt32LittleEndian(span[offset..]),
            FileOffset = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 4)..]),
            CompressedSize = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 8)..]),
            RealSize = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 12)..]),
            NumberOfParts = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 28)..]),
            FirstPart = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 32)..]),
            FilenameLength = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 36)..]),
            FilenameOffset = BinaryPrimitives.ReadInt32LittleEndian(span[(offset + 40)..])
        };

        private static void ReadFileNamesToDirectoryEntries(Span<byte> content, ArcFileRecordEntry[] directoryEntries, int numberOfEntries)
        {
            int currentOffset = 0;

            for (int i = 0; i < numberOfEntries; i++)
            {
                bool hasActiveFileNameEntry = directoryEntries[i].IsActive;
                if (!hasActiveFileNameEntry)
                    continue;

                string? filename = ReadArcNullTerminatedString(content, currentOffset, 2048, out int bytesConsumed);
                directoryEntries[i].FileName = filename is not null && filename.Length > 0 ? filename : $"Null File {i}";

                currentOffset += bytesConsumed;
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
        private static string? ReadArcNullTerminatedString(ReadOnlySpan<byte> span, int offset, int maxBufferSize, out int bytesConsumed)
        {
            bytesConsumed = 0;
            if (offset < 0 || offset >= span.Length)
                return null;

            int bufferSize = 0;

            while (offset + bufferSize < span.Length && bufferSize < maxBufferSize)
            {
                byte currentByte = span[offset + bufferSize];
                bufferSize++;

                int nullTeminator = 0x00;

                if (currentByte == nullTeminator)
                {
                    bytesConsumed = bufferSize;
                    return bufferSize == 1
                        ? string.Empty
                        : Encoding.ASCII.GetString(span.Slice(offset, bufferSize - 1));
                }

                int inactiveOrNullFileIndicator = 0x03;

                if (currentByte == inactiveOrNullFileIndicator)
                {
                    // 0x03 marker indicates inactive/null file
                    // Match original behavior: backup, set buffer[bufferSize-1] = 0x00, break
                    bufferSize--; // Back up to exclude 0x03
                    bytesConsumed = bufferSize + 1; // But count it as consumed
                    if (bufferSize == 0)
                        return null; // No filename available

                    return Encoding.ASCII.GetString(span.Slice(offset, bufferSize));
                }
            }

            // No terminator found - return what we have
            bytesConsumed = bufferSize;
            return bufferSize == 0 ? null : Encoding.ASCII.GetString(span.Slice(offset, bufferSize));
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
    }
}
