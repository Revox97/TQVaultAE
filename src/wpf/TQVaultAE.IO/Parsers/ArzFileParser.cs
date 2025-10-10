using System.IO;
using TQVaultAE.IO.Records;

namespace TQVaultAE.IO.Parsers
{
    /// <summary>
    /// Initializes a new instance of the ArzFile class.
    /// </summary>
    public class ArzFileParser(IRecordInfoProvider recordInfoProvider, ITitanQuestDataService tqData)
    {
		private readonly ITitanQuestDataService _tqData = tqData;
		private readonly IRecordInfoProvider _recordInfoProvider = recordInfoProvider;

        /// <summary>
        /// Reads the ARZ file.
        /// </summary>
        /// <returns>true on success</returns>
        public bool Read(ArzFile file)
		{
			try
			{
                // ARZ header file format
                //
                // 0x000000 int32
                // 0x000004 int32 start of dbRecord table
                // 0x000008 int32 size in bytes of dbRecord table
                // 0x00000c int32 numEntries in dbRecord table
                // 0x000010 int32 start of string table
                // 0x000014 int32 size in bytes of string table
                using FileStream instream = new(file.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                using BinaryReader reader = new(instream);

                try
                {
                    int[] header = new int[6];

                    for (int i = 0; i < 6; ++i)
                        header[i] = reader.ReadInt32();

                    int firstTableStart = header[1];
                    int firstTableCount = header[3];
                    int secondTableStart = header[4];

                    ReadStringTable(file, secondTableStart, reader);
                    ReadRecordTable(file, firstTableStart, firstTableCount, reader);

                    // 4 final int32's from file
                    // first int32 is numstrings in the stringtable
                    // second int32 is something ;)
                    // 3rd and 4th are crap (timestamps maybe?)
                    for (int i = 0; i < 4; ++i)
                        _ = reader.ReadInt32();
                }
                catch (IOException)
                {
                    throw;
                }
            }
            catch (IOException)
            {
				return false;
			}

			return true;
		}

		/// <summary>
		/// Gets the DBRecord for a particular ID.
		/// </summary>
		/// <param name="recordId">string ID of the record will be normalized internally</param>
		/// <returns>DBRecord corresponding to the string ID.</returns>
		public DBRecordCollection? GetItem(ArzFile file, RecordId recordId)
		{
			if (recordId is null)
                return null;

			RecordInfo rawRecord;

			if (file.RecordInfo.TryGetValue(recordId, out RecordInfo? value))
            {
				rawRecord = value;
                return _recordInfoProvider.Decompress(file, rawRecord);
            }

            return null;
		}

		/// <summary>
		/// Gets a database record without adding it to the cache.
		/// </summary>
		/// <remarks>
		/// The Item property caches the DBRecords, which is great when you are only using a few 100 (1000?) records and are requesting
		/// them many times.  Not great if you are looping through all the records as it eats alot of memory.  This method will create
		/// the record on the fly if it is not in the cache so when you are done with it, it can be reclaimed by the garbage collector.
		/// Great for when you want to loop through all the records for some reason.  It will take longer, but use less memory.
		/// </remarks>
		/// <param name="recordId">String ID of the record.  Will be normalized internally.</param>
		/// <returns>Decompressed RecordInfo record</returns>
		public DBRecordCollection GetRecordNotCached(ArzFile file, RecordId recordId) => _recordInfoProvider.Decompress(file, file.RecordInfo[recordId]);

		/// <summary>
		/// Reads the whole string table into memory from a stream.
		/// </summary>
		/// <remarks>
		/// string Table Format
		/// first 4 bytes is the number of entries
		/// then
		/// one string followed by another...
		/// </remarks>
		/// <param name="pos">position within the file.</param>
		/// <param name="reader">input BinaryReader</param>
		/// <param name="outStream">output StreamWriter.</param>
		private void ReadStringTable(ArzFile file, int pos, BinaryReader reader)
		{
			reader.BaseStream.Seek(pos, SeekOrigin.Begin);
			int numstrings = reader.ReadInt32();

			file.Strings = new string[numstrings];

			for (int i = 0; i < numstrings; ++i)
				file.Strings[i] = _tqData.ReadCString(reader);
		}

		/// <summary>
		/// Reads the entire record table into memory from a stream.
		/// </summary>
		/// <param name="pos">position within the file.</param>
		/// <param name="numEntries">number of entries in the file.</param>
		/// <param name="reader">input BinaryReader</param>
		/// <param name="outStream">output StreamWriter.</param>
		private void ReadRecordTable(ArzFile file, int pos, int numEntries, BinaryReader reader)
		{
			reader.BaseStream.Seek(pos, SeekOrigin.Begin);

			for (int i = 0; i < numEntries; ++i)
			{
				RecordInfo recordInfo = new();

				_recordInfoProvider.Decode(recordInfo, reader, 24, file); // 24 is the offset of where all record data begins
				file.RecordInfo.Add(recordInfo.Id, recordInfo);
			}
		}
    }
}
