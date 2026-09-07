using System.Buffers.Binary;
using System.Text;
using TQVaultAE.Arz.Model;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    internal sealed class ArzDecoder
    {
        internal static async Task<ArzFile> DecodeAsync(byte[] content, string path)
        {
            Span<byte> bytes = content.AsSpan();
            ArzHeaders headers = ReadHeaders(bytes);
            string[] infoItems = ReadDbrTable(bytes, headers.InfoTableStart, headers.InfoTableSize);
            ArzRecord[] records = ReadRecordTable(bytes, headers.RecordTableStart, headers.RecordTableSize, headers.RecordTableCount, ref infoItems);

            return new ArzFile()
            {
                FileName = path,
                Infos = infoItems,
                Records = records
            };
        }

        // ARZ header file format
        // 0x000000 int32
        // 0x000004 int32 start of dbRecord table
        // 0x000008 int32 size in bytes of dbRecord table
        // 0x00000c int32 numEntries in dbRecord table
        // 0x000010 int32 start of string table
        // 0x000014 int32 size in bytes of string table
        private static ArzHeaders ReadHeaders(Span<byte> content)
        {
            int[] fileHeaders = new int[6];

            for (int i = 0; i < 6; i++)
            {
                int index = i * 4;
                fileHeaders[i] = BinaryPrimitives.ReadInt32LittleEndian(content[index..(index + 4)]);
            }

            return new ArzHeaders()
            {
                RecordTableStart = fileHeaders[1],
                RecordTableSize = fileHeaders[2],
                RecordTableCount = fileHeaders[3],
                InfoTableStart = fileHeaders[4],
                InfoTableSize = fileHeaders[5],
            };
        }

        // Reads the whole string table into memory using ReadOnlySpan for zero-copy parsing.
        // Info?? Table Format:
        // first 4 bytes is the number of entries
        // then one string followed by another...
        private static string[] ReadDbrTable(Span<byte> content, int start, int size)
        {
            int count = BinaryPrimitives.ReadInt32LittleEndian(content[start..(start + 4)]);
            string[] dbrTable = new string[count];

            ReadOnlySpan<byte> dbrTableContent = content[start..(start + size)];
            int offset = sizeof(int);

            for (int i = 0; i < count; i++)
                dbrTable[i] = ReadCString(dbrTableContent, ref offset);

            return dbrTable;
        }

        // TODO, if required for more files -> move into separate class
        private static string ReadCString(ReadOnlySpan<byte> data, ref int offset)
        {
            int length = BinaryPrimitives.ReadInt32LittleEndian(data[offset..]);
            offset += sizeof(int);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding encoding1252 = Encoding.GetEncoding(1252);

            ReadOnlySpan<byte> stringData = data.Slice(offset, length);
            offset += length;
            return encoding1252.GetString(stringData);
        }

        private static ArzRecord[] ReadRecordTable(Span<byte> content, int start, int size, int recordCount, ref string[] strings)
        {
            ReadOnlySpan<byte> recordTableContent = content[start..(start + size)];

            int offset = 0;
            ArzRecord[] records = new ArzRecord[recordCount];

            for (int i = 0; i < recordCount; ++i)
                records[i] = ReadRecord(recordTableContent, ref offset, ref strings);

            return records;
        }

        // Record Entry Format
        // 0x0000 int32 stringEntryID (dbr filename)
        // 0x0004 int32 string length
        // 0x0008 string (record type)
        // 0x00XX int32 offset
        // 0x00XX int32 length in bytes
        // 0x00XX int32 timestamp? TODO Figure out what these are
        // 0x00XX int32 timestamp? TODO Figure out what these are
        private static ArzRecord ReadRecord(ReadOnlySpan<byte> content, ref int offset, ref string[] strings)
        {
            int infoEntityIndex = BinaryPrimitives.ReadInt32LittleEndian(content[offset..]);
            offset += sizeof(int);

            string type = ReadCString(content, ref offset);
            offset += sizeof(int);

            int dataLength = BinaryPrimitives.ReadInt32LittleEndian(content[offset..]);
            offset += sizeof(int) * 3;

            return new ArzRecord()
            {
                DataLength = dataLength,
                Type = type,
                Info = strings[infoEntityIndex]
            };
        }
    }
}
