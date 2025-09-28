using System.Text;
using System.Text.RegularExpressions;

namespace TQVaultAE.IO
{
    internal class TitanQuestFileRecord
    {
        /// <summary>
		/// Unhandled data
		/// </summary>
		public const string UnknownSegment = "Unknown segment";

		internal static readonly Encoding Encoding1252 = Encoding.GetEncoding(1252); // Encoding.GetEncoding(1252);
		internal static readonly Encoding EncodingUTF16 = Encoding.Unicode;

		public Match RegExMatch { get; set; }

		public string KeyLength { get; set; }
		public int KeyLengthAsInt { get; set; }

		public string KeyRaw { get; set; }
		public string KeyName { get; set; }

		public TitanQuestFileDataType DataType { get; set; } = TitanQuestFileDataType.Unknown;

		public TitanQuestFile File { get; set; }

        public int KeyIndex => RegExMatch is null ? ValueStart : RegExMatch.Index;

        public int ValueStart { get; set; }
		public int ValueEnd { get; set; }

		public string DataAsStr { get; set; }
		public byte[] DataAsByteArray { get; set; }
		public int? DataAsInt { get; set; }
		public float? DataAsFloat { get; set; }
		public bool IsSubStructureOpening => KeyName == TitanQuestFilePlayerRecordKey.BeginBlock.ToString();
		public bool IsStructureClosing => KeyName == TitanQuestFilePlayerRecordKey.EndBlock.ToString();
		public bool IsUnknownSegment => KeyName == UnknownSegment;
		public bool HasError => DataType == TitanQuestFileDataType.Unknown || IsUnknownSegment;
		public bool IsDataTypeError => DataType == TitanQuestFileDataType.Unknown && !IsUnknownSegment;
		public TitanQuestFileRecord Parent { get; internal set; }

		public List<TitanQuestFileRecord> Childs { get; internal set; } = [];

		/// <summary>
		/// Indicate that the key is irrelevant as data. Only data have a meaning.
		/// </summary>
		public bool IsKeyValue { get; internal set; } = false;

        public TitanQuestFileRecord() { }

		public TitanQuestFileRecord(TitanQuestFile file, Match m)
		{
			RegExMatch = m;
			File = file;

			KeyLength = m.Groups["Len"].Value;
			KeyName = m.Groups["Key"].Value;
			KeyRaw = m.Groups["Key"].Value;

            byte[] barray = Encoding1252.GetBytes([.. KeyLength]);
			KeyLengthAsInt = BitConverter.ToInt32(barray, 0);

			if (KeyLengthAsInt < KeyName.Length)
			{
				// Cut remaining chars
				KeyName = KeyName[..KeyLengthAsInt];
			}

			ValueStart = RegExMatch.Index + sizeof(int) + KeyLengthAsInt;
		}

		/// <summary>
		/// Read the value of this key accordingly to the datatype defined for the version of this file
		/// </summary>
		public virtual void ReadValue()
		{
            int len = 0;
			ValueEnd = 0;
			switch (DataType)
			{
				case TitanQuestFileDataType.Int:
					ValueEnd = ValueStart + sizeof(int) - 1; // -1 because ValueStart is first relevant byte
					DataAsByteArray = new ArraySegment<byte>(File.Content, ValueStart, sizeof(int)).ToArray();
					DataAsInt = BitConverter.ToInt32(DataAsByteArray, 0);
					DataAsFloat = BitConverter.ToSingle(DataAsByteArray, 0);
					break;
				case TitanQuestFileDataType.Float:
					ValueEnd = ValueStart + sizeof(float) - 1;
					DataAsByteArray = new ArraySegment<byte>(File.Content, ValueStart, sizeof(float)).ToArray();
					DataAsInt = BitConverter.ToInt32(DataAsByteArray, 0);
					DataAsFloat = BitConverter.ToSingle(DataAsByteArray, 0);
					break;
				case TitanQuestFileDataType.String1252:
					// Read StrLen
					len = BitConverter.ToInt32(new ArraySegment<byte>(File.Content, ValueStart, sizeof(int)).ToArray(), 0);
					// Read Str
					ValueEnd = ValueStart + sizeof(int) - 1 + len;
					DataAsByteArray = new ArraySegment<byte>(File.Content, ValueStart + sizeof(int), len).ToArray();
					DataAsStr = Encoding1252.GetString(DataAsByteArray);
					break;
				case TitanQuestFileDataType.StringUTF16:
					// Read StrLen
					len = BitConverter.ToInt32(new ArraySegment<byte>(File.Content, ValueStart, sizeof(int)).ToArray(), 0);
					// Read Str
					ValueEnd = ValueStart + sizeof(int) - 1 + (len * 2);
					DataAsByteArray = new ArraySegment<byte>(File.Content, ValueStart + sizeof(int), len * 2).ToArray();// * 2 because UTF16 has 2 byte encoding
					DataAsStr = EncodingUTF16.GetString(DataAsByteArray); //ReadUTF16String();
					break;
				case TitanQuestFileDataType.ByteArrayVar:
					// Read Len
					len = BitConverter.ToInt32(new ArraySegment<byte>(File.Content, ValueStart, sizeof(int)).ToArray(), 0);
					// Read bytes
					ValueEnd = ValueStart + sizeof(int) - 1 + len;
					DataAsByteArray = new ArraySegment<byte>(File.Content, ValueStart + sizeof(int), len).ToArray();
					break;
				case TitanQuestFileDataType.ByteArray16:
					// Read bytes
					ValueEnd = ValueStart + 16 - 1;
					DataAsByteArray = new ArraySegment<byte>(File.Content, ValueStart, 16).ToArray();
					break;
			}
		}

		/// <summary>
		/// Define the datatype of this record
		/// </summary>
        // TODO can most likely be removed
		public virtual void DefineDataType()
		{
			throw new NotImplementedException();
		}

		internal string GetDataAsString(bool displayDataDecimal)
		{
			string data = string.Empty;

			switch (DataType)
			{
				case TitanQuestFileDataType.Int:
                    if (DataAsInt is null)
                        throw new Exception(); // TODO Define exact exception

					data = displayDataDecimal ? DataAsInt.Value.ToString() : DataAsInt.Value.ToString("X8");
					break;
				case TitanQuestFileDataType.Float:
                    if (DataAsFloat is null)
                        throw new Exception(); // TODO Define exact exception

					data = displayDataDecimal ? DataAsFloat.Value.ToString() : DataAsInt.Value.ToString("X8"); // TODO is this on purpose?
					break;
				case TitanQuestFileDataType.String1252:
				case TitanQuestFileDataType.StringUTF16:
					data = DataAsStr;
					break;
				case TitanQuestFileDataType.ByteArrayVar:
				case TitanQuestFileDataType.ByteArray16:
				case TitanQuestFileDataType.Unknown:
					data = string.Join(" ", DataAsByteArray.Select(b => displayDataDecimal ? b.ToString() : b.ToString("X2")));
					break;
			}
			return data;
		}
    }
}
