using System.Globalization;
using System.IO;
using System.Text;

namespace TQVaultAE.IO.Parsers
{
    public class ArcParser(string path)
    {
        private readonly string _path = path;

        private ArcDirEntry[] _records = [];
        private ArcPartEntry[] _parts = [];

        // Format of an ARC file
        // 0x08 - 4 bytes = # of files
        // 0x0C - 4 bytes = # of parts
        // 0x18 - 4 bytes = offset to directory structure
        //
        // Format of directory structure
        // 4-byte int = offset in file where this part begins
        // 4-byte int = size of compressed part
        // 4-byte int = size of uncompressed part
        // these triplets repeat for each part in the arc file
        // After these triplets are a bunch of null-terminated strings
        // which are the sub filenames.
        // After the subfilenames comes the subfile data:
        // 4-byte int = 3 == indicates start of subfile item  (maybe compressed flag??)
        //          1 == maybe uncompressed flag??
        // 4-byte int = offset in file where first part of this subfile begins
        // 4-byte int = compressed size of this file
        // 4-byte int = uncompressed size of this file
        // 4-byte crap
        // 4-byte crap
        // 4-byte crap
        // 4-byte int = numParts this file uses
        // 4-byte int = part# of first part for this file (starting at 0).
        // 4-byte int = length of filename string
        // 4-byte int = offset in directory structure for filename

        // TODO Heavy refactor required, what is this BS. Guess this village idiot wrote it
        public ArcFile? Read()
        {
            try
            {
                using FileStream arcFileStream = new(_path, FileMode.Open, FileAccess.Read);
                using BinaryReader reader = new(arcFileStream);

                // TODO Return something better than null
                if (!VerifyHeader(reader))
                    return null!;

                InitilizeDataStructure(reader);
                ReadParts(reader);
                ReadDirectories(reader);
                ReadFileNames(reader);

                return CreateArcFile();
            }
            catch (Exception ex)
            {
                // TODO Log exception
            }

            return null!;
        }

        private static bool VerifyHeader(BinaryReader reader)
        {
            byte firstByte = reader.ReadByte();

            // TODO What mean those magic numbers
            return firstByte != 0x41 && firstByte != 0x52 && firstByte != 0x43 && reader.BaseStream.Length < 0x21;
        }

        private void InitilizeDataStructure(BinaryReader reader)
        {
                // TODO Yet another magic number, most likely content start
                reader.BaseStream.Seek(0x08, SeekOrigin.Begin);
                int numRecords = reader.ReadInt32();
                int numParts = reader.ReadInt32();

                _records = new ArcDirEntry[numRecords];
                _parts = new ArcPartEntry[numParts];
        }

        private void ReadParts(BinaryReader reader)
        {
            byte tocOffsetBegin = 0x18;
            reader.BaseStream.Seek(tocOffsetBegin, SeekOrigin.Begin);
            int tocOffset = reader.ReadInt32();

            if (reader.BaseStream.Length < (tocOffset + 12))
                return;

            reader.BaseStream.Seek(tocOffset, SeekOrigin.Begin);

            for (int i = 0; i < _parts.Length; ++i)
            {
                _parts[i] = new ArcPartEntry()
                {
                    FileOffset = reader.ReadInt32(),
                    CompressedSize = reader.ReadInt32(),
                    RealSize = reader.ReadInt32()
                };
            }
        }

        private void ReadDirectories(BinaryReader reader)
        {
            int fileRecordOffset = _records.Length * 44;

            reader.BaseStream.Seek(-1 * fileRecordOffset, SeekOrigin.End);

            for (int i = 0; i < _records.Length; ++i)
            {
                // storageType = 3 - compressed / 1- non compressed
                int storageType = reader.ReadInt32();

                ArcDirEntry arcDirectory = new()
                {
                    StorageType = storageType,
                    FileOffset = reader.ReadInt32(),
                    CompressedSize = reader.ReadInt32(),
                    RealSize = reader.ReadInt32()
                };

                _ = reader.ReadInt32();
                _ = reader.ReadInt32();

                int numberOfParts = reader.ReadInt32();

                arcDirectory.Parts = numberOfParts > 0
                    ? (new ArcPartEntry[numberOfParts])
                    : [];

                int firstPart = reader.ReadInt32();

                _ = reader.ReadInt32();
                _ = reader.ReadInt32();

                if (storageType != 1 && arcDirectory.IsActive)
                {
                    for (int k = 0; k < arcDirectory.Parts.Length; ++k)
                        arcDirectory.Parts[k] = _parts[k + firstPart];
                }

                _records[i] = arcDirectory;
            }
        }

        private void ReadFileNames(BinaryReader reader)
        {
            int fileNamesOffset = (int)reader.BaseStream.Position;

            reader.BaseStream.Seek(fileNamesOffset, SeekOrigin.Begin);

            byte[] buffer = new byte[2048];
            ASCIIEncoding asciiEncoding = new();

            for (int i = 0; i < _records.Length; ++i)
            {
                if (!_records[i].IsActive)
                    continue;

                int bufferSize = ReadToBuffer(reader, ref buffer);

                if (bufferSize>= 1)
                {
                    char[] chars = new char[asciiEncoding.GetCharCount(buffer, 0, bufferSize - 1)];
                    asciiEncoding.GetChars(buffer, 0, bufferSize - 1, chars, 0);

                    _records[i].FileName = new string(chars);
                }
                else
                {
                    _records[i].FileName = string.Format(CultureInfo.InvariantCulture, "Null File {0}", i);
                }
            }
        }

        private static int ReadToBuffer(BinaryReader reader, ref byte[] buffer)
        {
            int bufferSize = 0;

            while ((buffer[bufferSize++] = reader.ReadByte()) != 0x00)
            {
                if (buffer[bufferSize - 1] == 0x03)
                {
                    reader.BaseStream.Seek(-1, SeekOrigin.Current);
                    bufferSize--;
                    buffer[bufferSize] = 0x00;

                    break;
                }

                if (bufferSize >= buffer.Length)
                {
                    // TODO log error // throw exception
                }
            }

            return bufferSize;
        }

        private ArcFile CreateArcFile()
        {
            Dictionary<string, ArcDirEntry> directories = new(_records.Length);

            for (int i = 0; i < _records.Length; ++i)
            {
                if (_records[i].IsActive)
                    directories.Add(_records[i].FileName, _records[i]);
            }

            return new ArcFile(_path)
            {
                DirectoryEntries = directories,
            };
        }
    }
}
