using System.Text;

namespace TQVaultAE.IO.Parsers
{
    internal class ChrFileParser
    {
        private const int CodePage1252 = 1252;
        private const byte Encoding_Null = 0x0;
        private const byte Encoding_FileStart = 0x0D;
        private const byte Encoding_RawDelimiter = 0x0E;
        private const byte Encoding_Ascii_Underscore = 0x5F;
        private const byte Encoding_Ascii_0 = 0x30;
        private const byte Encoding_Ascii_9 = 0x39;
        private const byte Encoding_Ascii_A = 0x41;
        private const byte Encoding_Ascii_Z = 0x5A;
        private const byte Encoding_Ascii_a = 0x61;
        private const byte Encoding_Ascii_z = 0x7A;
        private const byte Encoding_Ascii_Bracket_Close = 0x29;
        private const byte Encoding_Ascii_StartCodePage = 0x80;
        private const byte Encoding_Ascii_EndCodePage = 0xFF;

        private byte[] _content = [];
        private int _currentPosition;
        private readonly Encoding _encoding;

        private static readonly Dictionary<string, ChrRecordType> s_keyTypeMap = new() {
            { "headerVersion", ChrRecordType.Int },
            { "playerCharacterClass", ChrRecordType.String },
            { "uniqueId", ChrRecordType.Id },
            { "streamData", ChrRecordType.Raw },
            { "playerClassTag", ChrRecordType.String },
            { "playerLevel", ChrRecordType.Int },
            { "playerVersion", ChrRecordType.Int },
            { "begin_block", ChrRecordType.BeginBlock },
            { "end_block", ChrRecordType.EndBlock },
        };

        internal ChrFileParser()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            _encoding = Encoding.GetEncoding(CodePage1252);
        }

        public ChrFileRecord Parse(byte[] content)
        {
            try
            {
                _content = content;
                _currentPosition = 0;

                ChrFileRecord topLevelElement = new()
                {
                    Type = ChrRecordType.ChrFile,
                    Start = _currentPosition,
                    End = _content.Length - 1,
                };

                VerifyFileStart();

                while(_currentPosition + 1 < _content.Length)
                {
                    ChrFileRecord? result = ReadDataRecord();

                    if (result is not null)
                        topLevelElement.Children.Add(result);
                }

                return topLevelElement;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void VerifyFileStart()
        {
            ushort currentChar = GetChar();
            if (currentChar != Encoding_FileStart)
                throw new ArgumentException("The file must be of type .chr");

            IncrementCurrentPosition();
        }

        private void IncrementCurrentPosition(int times = 1) => _currentPosition += times;

        private ChrFileRecord? ReadDataRecord()
        {
            int start = _currentPosition;

            int keyStart = _currentPosition;
            string key = ReadKey();
            int keyEnd = _currentPosition - 1;

            if (s_keyTypeMap.TryGetValue(key, out ChrRecordType recordType))
            {
                object? value = ReadValue(recordType, out int valueStart, out int valueEnd);
                int end = _currentPosition - 1;

                return new ChrFileRecord()
                {
                    Key = key,
                    Value = value,
                    Type = recordType,
                    KeyStart = keyStart,
                    KeyTo = keyEnd,
                    ValueStart = valueStart,
                    ValueEnd = valueEnd,
                    Start = start,
                    End = end
                };
            }

            return null!;
        }

        private bool CheckForSeperator(int offset = 0)
        {
            int position = _currentPosition + offset;
            return _content[position] == Encoding_Null && _content[position + 1] == Encoding_Null && _content[position + 2] == Encoding_Null;
        }

        private string ReadKey()
        {
            while (_currentPosition < _content.Length - 3 && !CheckForSeperator())
                IncrementCurrentPosition();

            IncrementCurrentPosition(3);
            return ReadString(-1, out _, out _);
        }

        private object? ReadValue(ChrRecordType type, out int valueStart, out int valueEnd)
        {
            if (type == ChrRecordType.Int)
                return ReadInt(out valueStart, out valueEnd);

            if (type == ChrRecordType.String)
            { 
                int length = ReadInt(out _, out _);
                IncrementCurrentPosition(3);
                return ReadString(length, out valueStart, out valueEnd);
            }

            if (type == ChrRecordType.Id)
                return ReadId(out valueStart, out valueEnd);

            if (type == ChrRecordType.Raw)
            {
                IncrementCurrentPosition(4);
                return ReadRaw(out valueStart, out valueEnd);
            }

            if (type == ChrRecordType.BeginBlock)
                return ReadStartBlock(out valueStart, out valueEnd);

            if (type == ChrRecordType.EndBlock)
                return ReadEndBlock(out valueStart, out valueEnd);

            valueStart = _currentPosition;
            valueEnd = _currentPosition;
            return null!; 
        }

        private byte[] ReadEndBlock(out int valueStart, out int valueEnd)
        {
            valueStart = _currentPosition;
            byte[] result = ReadRaw(out _, out _);
            valueEnd = _currentPosition;

            return result;
        }

        // TODO implement
        private ChrFileRecord ReadStartBlock(out int valueStart, out int valueEnd)
        {
            valueStart = _currentPosition;
            byte[] beginBlockValue = ReadRaw(out _, out _);
            valueEnd = _currentPosition;

            List<ChrFileRecord> children = [];

            while (_currentPosition < _content.Length)
            {
                ChrFileRecord? result = ReadDataRecord();

                if (result is not null)
                {
                    children.Add(result);

                    if (result.Key == "end_block")
                        break;
                }
            }

            return new()
            {
                Start = valueStart,
                Value = beginBlockValue,
                End = valueEnd,
                Children = children,
                Key = "begin_block",
                Type = ChrRecordType.BeginBlock
            };
        }

        private byte[] ReadRaw(out int valueStart, out int valueEnd)
        {
            valueStart = _currentPosition;
            List<byte> result = [];

            while (_currentPosition < _content.Length &&
                  (_content[_currentPosition] != Encoding_RawDelimiter && !CheckForSeperator()))
            {
                result.Add(_content[_currentPosition]);
                IncrementCurrentPosition();
            }

            valueEnd = _currentPosition;
            IncrementCurrentPosition();
            return [.. result];
        }

        private int ReadInt(out int valueStart, out int valueEnd)
        {
            valueStart = _currentPosition;

            if (CheckForSeperator())
                IncrementCurrentPosition(3);

            int result = _content[_currentPosition];
            valueEnd = _currentPosition;

            IncrementCurrentPosition();
            return result;
        }

        private string ReadId(out int valueStart, out int valueEnd)
        {
            valueStart = _currentPosition;
            string result = string.Empty;

            while (_currentPosition < _content.Length && !CheckForSeperator())
            {
                result += Convert.ToChar(GetChar());
                IncrementCurrentPosition();
            }

            valueEnd = _currentPosition - 1;
            return result;
        }

        private string ReadString(int length, out int valueStart, out int valueEnd)
        {
            valueStart = _currentPosition;
            string result = string.Empty;

            if (length != -1)
            {
                while (_currentPosition < valueStart + length)
                {
                    result += Convert.ToChar(GetChar());
                    IncrementCurrentPosition();
                }
            }
            else
            {
                ushort currentChar = GetChar();

                while (_currentPosition < _content.Length && !CheckForSeperator() &&
                     ((currentChar >= Encoding_Ascii_0 && currentChar <= Encoding_Ascii_9)
                   || (currentChar >= Encoding_Ascii_A && currentChar <= Encoding_Ascii_Z)
                   || (currentChar >= Encoding_Ascii_a && currentChar <= Encoding_Ascii_z)
                   || (currentChar >= Encoding_Ascii_StartCodePage && currentChar <= Encoding_Ascii_EndCodePage)
                   || (currentChar == Encoding_Ascii_Underscore)))
                {
                    result += Convert.ToChar(currentChar);
                    IncrementCurrentPosition();

                    // Block start or end, additional chars are ignored for key
                    if (result == "begin_block" || result == "end_block")
                        break;
                        
                    currentChar = GetChar();
                }

                // TODO Figure out when exactly which value is used to prevent decoding issues
                if (currentChar == Encoding_Ascii_Bracket_Close || currentChar == 0x09)
                    IncrementCurrentPosition();
            }

            valueEnd = _currentPosition;
            return result;
        }

        private ushort GetChar(int offset = 0)
        {
            string result = _encoding.GetString([_content[_currentPosition + offset] ]);
            return result[0];
        }
    }
}
