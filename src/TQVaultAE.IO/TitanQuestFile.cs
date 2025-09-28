using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using TQVaultAE.IO.Parsers;

namespace TQVaultAE.IO
{
    internal partial class TitanQuestFile
    {
        public const string ExtPlayer = ".chr";
		/// <summary>
		/// The character's private stash.
		/// </summary>
		public const string ExtSharedStash = ".dxb";

		/// <summary>
		/// The character's private stash backup.
		/// </summary>
		public const string ExtSharedStashBackup = ".dxg";

        /// <summary>
        /// List of TQ save file extensions
        /// </summary>
        public static string[] AllowedExtensions => [ExtPlayer, ExtSharedStashBackup, ExtSharedStash];


		public byte[] Content { get; private set; }

		public string Path { get; private set; }

		public string Ext { get; private set; }

		public TitanQuestFileRecord[] Records { get; private set; }

		public TitanQuestFileRecord[] Childs { get; private set; }

		private TitanQuestVersion? _version = null;

		public TitanQuestVersion Version
		{
			get
			{
				// Define version by analysing records
				FindFileVersion();
                return _version is not null ? _version.Value : TitanQuestVersion.Original;
			}
			set => _version = value;
		}

		public bool IsStashFile
		{
			get => Ext.Equals(ExtSharedStash, StringComparison.InvariantCultureIgnoreCase)
				|| Ext.Equals(ExtSharedStashBackup, StringComparison.InvariantCultureIgnoreCase);
		}

        public bool IsPlayerFile => Ext.Equals(ExtPlayer, StringComparison.InvariantCultureIgnoreCase);

        // TODO Replace mapper
        //public IMapper Mapper { get; }

        //private TitanQuestFile(IMapper mapper) => Mapper = mapper;

        // public static TitanQuestFile ReadFile(string path, IMapper mapper)
		// {
		// 	    return new TitanQuestFile(mapper)
		// 	    {
		// 	    	Path = path,
		// 	    	Ext = System.IO.Path.GetExtension(path).ToLower(),
		// 	    	Content = File.ReadAllBytes(path),
		// 	    };
		// }

        private TitanQuestFile() { }

        public static TitanQuestFile ReadFile(string path)
		{
			return new TitanQuestFile()
			{
				Path = path,
				Ext = System.IO.Path.GetExtension(path).ToLower(),
				Content = File.ReadAllBytes(path),
			};
		}

        //[GeneratedRegex(@"(?<Len>.\x00{3})(?<Key>(?<Open>\(\*)?(?<Name>[a-zA-Z0-9_\.]+)(?<Close>\))?(?<Ext>\[i\])?)"
        //    , RegexOptions.Singleline)]
        [GeneratedRegex(@"(?<Len>.\x00{3})(?<Key>(?<Open>\(\*)?(?<Name>[a-zA-Z0-9_\.]+)(?<Close>\))?(?<Ext>" + @"\[i\])?)", RegexOptions.Singleline)]
        public static partial Regex KeyMatchRegex();

		/// <summary>
		/// Parse file data to raw records
		/// </summary>
		public void Parse()
		{
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            //Encoding encoding1252 = Encoding.GetEncoding(1252);
            //string contentAsString = encoding1252.GetString(Content);

            // TODO Use file parser here instead
            ChrFileRecord result = new ChrFileParser().Parse(Content);

            // Regex save file / Where the magic lies
    //        List<Match> keyMatches = [.. KeyMatchRegex().Matches(contentAsString).Cast<Match>().Where(m => m.Success)];

    //        Records = [.. keyMatches.Select(m => new TitanQuestFileRecord(this, m))
				//// Remove all keys that don't match keylen (false match.Success)
				//.Where(m => m.KeyLengthAsInt == m.KeyName.Length)];
        }

		private const int PlayerHeaderVersionValueTq = 1;
		private const int PlayerHeaderVersionValueTqIt = 2;
		private const int PlayerHeaderVersionValueTqAe = 3;

		private void FindFileVersion()
		{
			if (_version.HasValue)
                return;

			// Determine version
			switch (Ext)
			{
				case ExtPlayer:
                    string headerVersionName = EnumValueProvider.GetValue(TitanQuestFilePlayerRecordKey.HeaderVersion);
                    TitanQuestFileRecord? headerVersionKey = Records.FirstOrDefault(k => k.KeyName == headerVersionName);
                    int fileVersionValue = BitConverter.ToInt32(new ArraySegment<byte>(Content, headerVersionKey.ValueStart, sizeof(int)).ToArray(), 0);

					if (fileVersionValue == PlayerHeaderVersionValueTq)
						_version = TitanQuestVersion.Original;
					else if (fileVersionValue == PlayerHeaderVersionValueTqIt)
						_version = TitanQuestVersion.ImmortalThrone;
					else if (fileVersionValue == PlayerHeaderVersionValueTqAe)
						_version = TitanQuestVersion.AnniversaryEdition;
					break;

				case ExtSharedStash:
				case ExtSharedStashBackup:
					// Is there any hint by analysing file content ?
					_version = TitanQuestVersion.AnniversaryEdition;
					break;
				default:
					throw new ArgumentException("Must be a file with extension chr, dxb, dxg");
			}
		}


		/// <summary>
		/// Analyse Parsed Records to produce fine detailed informations
		/// </summary>
		public void Analyse()
		{
			FindFileVersion();

            List<TitanQuestFileRecord> records = [.. Records.Select(m => Ext switch
            {
                ExtPlayer => new TitanQuestFileRecordToTitanQuestFilePlayerRecordMapper().Map(m),
                ExtSharedStash or ExtSharedStashBackup => (TitanQuestFileRecord)new TitanQuestFileRecordToTitanQuestFilePlayerTransferStashRecordMapper().Map(m),
                _ => throw new ArgumentException("Must be a file with extension chr, dxb, dxg"),
            })];

            // Try read values
            records.ForEach(r => r.ReadValue());

            List<TitanQuestFileRecord> falseKeys = [];

			for (int i = 1; i < records.Count; i++)
			{
				TitanQuestFileRecord falsekey = records[i];

				for (int ii = 0; ii < i; ii++)
				{
					if (falseKeys.Contains(records[ii]))
						continue;// Je passe sur les éléments déja écartés

                    TitanQuestFileRecord legitkey = records[ii];
					if (falsekey.RegExMatch.Index == legitkey.ValueStart)
					{
						falseKeys.Add(falsekey);
						break;
					}
				}
			}

            // Cleanup
            int rem = records.RemoveAll(falseKeys.Contains);

			// Reveal unknown keys based on enumlist
			// Already done ! They don't have known DataType

			// check if some bytes are not include in records
			List<byte> orphans = [];
			List<KeyValuePair<int, TitanQuestFileRecord>> orphansRecords = [];
			TitanQuestFileRecord? curr = null;

			for (int cursor = 0; cursor < Content.Length; cursor++)
			{
				for (int ii = 0; ii < records.Count; ii++)
				{
					curr = records[ii];

					if (
						curr.DataType == TitanQuestFileDataType.Unknown
						&& curr.RegExMatch.Index <= cursor && cursor < curr.ValueStart // byte is part of an unknown key
					)
					{
						MakeUnknownSegment(records, orphans, orphansRecords, ii);
						cursor = curr.ValueStart - 1; // Move cursor to farthest known position (-1 to compensate cursor++)

                        // TODO use some kind of break instead
						goto skip;
					}
					else if (
						curr.DataType != TitanQuestFileDataType.Unknown
						&& curr.RegExMatch.Index <= cursor && cursor <= curr.ValueEnd
					)
					{
						MakeUnknownSegment(records, orphans, orphansRecords, ii);
						cursor = curr.ValueEnd; // Move cursor to farthest known position

                        // TODO use some kind of break instead
						goto skip;
					}
				}
				orphans.Add(Content[cursor]);
			skip:;
			}

			MakeUnknownSegment(records, orphans, orphansRecords, records.Count);// In cas there there is unknonwn trailing bytes

			for (int i = orphansRecords.Count - 1; i >= 0; i--)
				records.Insert(orphansRecords[i].Key, orphansRecords[i].Value);

			// Legitimate CRC on StashFiles
			if (IsStashFile)
			{
                TitanQuestFileRecord? crc = records.FirstOrDefault();
				if (crc is not null && crc.IsUnknownSegment)// Should be true
				{
					crc.DataType = TitanQuestFileDataType.Int;
					crc.DataAsInt = BitConverter.ToInt32(crc.DataAsByteArray, 0);
					crc.KeyName = TitanQuestFilePlayerTransferStashKey.CRC.ToString();
					crc.KeyLengthAsInt = crc.KeyName.Length;
					crc.IsKeyValue = true;
				}
			}

            Records = [.. records];
            Childs = [.. MakeTreeRecords().nodes];
		}

		private static void MakeUnknownSegment(List<TitanQuestFileRecord> records, List<byte> orphans, List<KeyValuePair<int, TitanQuestFileRecord>> orphansRecords, int ii)
		{
            if (orphans.Count != 0)
			{
                int previdx = ii - 1;
                int valueStart = previdx == -1 ? 0 : records[previdx].DataType == TitanQuestFileDataType.Unknown ? records[previdx].ValueStart : records[previdx].ValueEnd + 1;

				orphansRecords.Add(new KeyValuePair<int, TitanQuestFileRecord>(ii, new TitanQuestFileRecord()
                {
                    KeyName = TitanQuestFileRecord.UnknownSegment,
                    KeyLengthAsInt = TitanQuestFileRecord.UnknownSegment.Length,
                    ValueStart = valueStart,
                    ValueEnd = valueStart + orphans.Count - 1,
                    DataAsByteArray = [.. orphans],
                    IsKeyValue = true,
                }));

				orphans.Clear();
			}
		}


		private (List<TitanQuestFileRecord> nodes, int newidx) MakeTreeRecords(TitanQuestFileRecord? parent = null, int idx = 0)
		{
            List<TitanQuestFileRecord> currentLvl = [];

			for (; idx < Records.Length; idx++)
			{
                TitanQuestFileRecord k = Records[idx];
				k.Parent = parent;

				if (k.IsSubStructureOpening)
				{
                    (List<TitanQuestFileRecord> nodes, int newidx) = MakeTreeRecords(k, idx + 1);
					idx = newidx;
                    k.Childs.AddRange([.. nodes]);
				}

				currentLvl.Add(k);

				if (k.IsStructureClosing)
                    break;
			}
			return (currentLvl, idx);
		}
    }
}
