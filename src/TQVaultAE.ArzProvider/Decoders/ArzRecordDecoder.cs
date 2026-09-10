using System.Collections.Concurrent;
using System.IO.MemoryMappedFiles;
using System.Text;
using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    internal static class ArzRecordDecoder
    {
        private const int PropertySize = 4;

        private static string s_arzPath = string.Empty;
        private static string[] s_infoRecords = [];
        private static bool s_initialized = false;

        private static readonly LazyConcurrentDictionary<string, MemoryMappedFile> s_mmfCache = new();
	    private static readonly LazyConcurrentDictionary<string, long> s_fileSizeCache = new();

        public static void Initialize(string arzPath, string[] infoRecords)
        {
            s_arzPath = arzPath;
            s_infoRecords = infoRecords;
            s_initialized = true;
        }

        internal static async Task<ArzRecord> ReadRecordAsync(BinaryReader reader)
        {
            if (!s_initialized)
                throw new InvalidOperationException("ArzRecordDecoder must be initialized first.");

            int infoEntityIndex = reader.ReadInt32();
            string type = ReadString(reader);

            long offset = reader.ReadInt32();
            long dataOffset = offset + 24;
            int dataLength = reader.ReadInt32();

            reader.BaseStream.Position += sizeof(int) * 2; // Skip last two parameters

            return new ArzRecord(type, string.Empty, s_infoRecords[infoEntityIndex], dataOffset, dataLength);
        }

        // TODO Move into separate class
        private static string ReadString(BinaryReader reader)
        {
            int stringLength = reader.ReadInt32();
            byte[] content = reader.ReadBytes(stringLength);
            return Encoding.UTF8.GetString(content);
        }

        // TODO Consider lazy loading, lets see how the performance is. Maybe loading the database completely at startup is the best idea.
        // TODO absolute original TQVault mess migrate to use the same binary reader
        internal static async Task<ArzRecord> ReadRecordPropertiesFromRecord(ArzRecord record)
        {
            if (!s_initialized)
                throw new InvalidOperationException("ArzRecordDecoder must be initialized first.");

            int dataLength = record.DataLength;

            if (dataLength <= 0)
                return record;

            ReadOnlySpan<byte> compressedData = GetReadOnlySpan(s_arzPath, ((int)record.DataOffset) + 2, record.DataLength - 2);
            byte[] data = compressedData.Length > 0 ? Compression.DecompressZlib(compressedData) : [];

            int dwordCount = data.Length / PropertySize;

            if (data.Length % PropertySize != 0)
                throw new InvalidDataException("Record data must be a multiple of 4.");

            using MemoryStream stream = new(data);
            using BinaryReader propertyReader = new(stream);
            int currentDwordCount = 0;

            while (currentDwordCount < dwordCount)
                record.Properties.Add(ReadProperty(propertyReader, ref currentDwordCount));

            return record;
        }

        private static ArzRecordProperty ReadProperty(BinaryReader reader, ref int currentDwordCount)
        {
            ArzRecordPropertyType dataType = (ArzRecordPropertyType)reader.ReadInt16();
            short valCount = reader.ReadInt16();
            int variableID = reader.ReadInt32();
            string variableName = s_infoRecords[variableID] ?? throw new ArgumentNullException(string.Format("Error while parsing arz record , variable is NULL"));

            ArzRecordProperty recordProperty = new(variableName, dataType, valCount);

            if (valCount < 1)
                throw new ArgumentException(string.Format("Error while parsing arz record , variable {0}, bad valCount {1}", variableName, valCount));

            // increment dword count
            currentDwordCount += 2 + valCount;

            for (int j = 0; j < valCount; ++j)
            {
                switch (recordProperty.DataType)
                {
                    case ArzRecordPropertyType.Integer:
                    case ArzRecordPropertyType.Boolean:
                        recordProperty[j] = reader.ReadInt32();
                        break;
                    case ArzRecordPropertyType.Float:
                        // Convert int32 bits to float using BitConverter
                        int intBits = reader.ReadInt32();
                        byte[] bytes = BitConverter.GetBytes(intBits);
                        recordProperty[j] = BitConverter.ToSingle(bytes, 0);
                        break;
                    case ArzRecordPropertyType.String:
                        int id = reader.ReadInt32();
                        recordProperty[j] = s_infoRecords[id]?.Trim() ?? string.Empty;
                        break;
                    default:
                        recordProperty[j] = reader.ReadInt32();
                        break;
                }
            }

            return recordProperty;
        }

        private static ReadOnlySpan<byte> GetReadOnlySpan(string filePath, int offset, int length)
        {
            MemoryMappedFile mmf = GetOrCreateMmf(filePath);

            if (mmf is null)
                return [];

            try
            {
                using MemoryMappedViewAccessor accessor = mmf.CreateViewAccessor(offset, length, MemoryMappedFileAccess.Read);
                if (accessor.SafeMemoryMappedViewHandle.IsInvalid)
                    return [];

                byte[] span = new byte[length];
                accessor.ReadArray(0, span, 0, length);

                return span;
            }
            catch (Exception ex)
            {
                return [];
            }
        }

        private static MemoryMappedFile GetOrCreateMmf(string filePath)
        {
            return s_mmfCache.GetOrAddAtomic(filePath, path =>
            {
                try
                {
                    s_fileSizeCache.GetOrAddAtomic(path, s =>
                    {
                        var fileInfo = new FileInfo(path);
                        return fileInfo.Length;
                    });
                    return MemoryMappedFile.CreateFromFile(path, FileMode.Open, null, 0, MemoryMappedFileAccess.Read);
                }
                catch (Exception ex)
                {
                    return null!;
                }
            });
        }
    }

    // TODO Move into separate file
    public class LazyConcurrentDictionary<TKey, TValue> : ConcurrentDictionary<TKey, Lazy<TValue>>
    {
        private volatile int _version;

        /// <summary>
        /// Gets the current version number that increments on any modification.
        /// </summary>
        public int Version => _version;

        private void IncrementVersion() => Interlocked.Increment(ref _version);

        public TValue GetOrAddAtomic(TKey key, Func<TKey, TValue> valueFactory)
        {
            bool keyExisted = ContainsKey(key);
            Lazy<TValue> lazyResult = GetOrAdd(key
                , k => new Lazy<TValue>(() => valueFactory(k), LazyThreadSafetyMode.ExecutionAndPublication)
            );

            // Only increment version when a new key was added (not when retrieving existing)
            if (!keyExisted)
                IncrementVersion();

            return lazyResult.Value;
        }

        public TValue AddOrUpdateAtomic(TKey key, TValue addValue)
        {
            Lazy<TValue> lazyResult = AddOrUpdate(key
                , new Lazy<TValue>(() => addValue, LazyThreadSafetyMode.ExecutionAndPublication)
                , (k, oldValue) => new Lazy<TValue>(() => addValue, LazyThreadSafetyMode.ExecutionAndPublication)
            );
            IncrementVersion();
            return lazyResult.Value;
        }

        public TValue AddOrUpdateAtomic(TKey key, TValue addValue, Func<TKey, TValue, TValue> updateValueFactory)
        {
            Lazy<TValue> lazyResult = AddOrUpdate(key
                , new Lazy<TValue>(() => addValue, LazyThreadSafetyMode.ExecutionAndPublication)
                , (k, oldValue) => new Lazy<TValue>(() => updateValueFactory(k, oldValue.Value), LazyThreadSafetyMode.ExecutionAndPublication)
            );
            IncrementVersion();
            return lazyResult.Value;
        }

        public TValue AddOrUpdateAtomic(TKey key, Func<TKey, TValue> addValueFactory, Func<TKey, TValue, TValue> updateValueFactory)
        {
            Lazy<TValue> lazyResult = AddOrUpdate(key
                , k => new Lazy<TValue>(() => addValueFactory(k), LazyThreadSafetyMode.ExecutionAndPublication)
                , (k, oldValue) => new Lazy<TValue>(() => updateValueFactory(k, oldValue.Value), LazyThreadSafetyMode.ExecutionAndPublication)
            );
            IncrementVersion();
            return lazyResult.Value;
        }

        public new void Clear()
        {
            base.Clear();
            IncrementVersion();
        }

        public bool TryRemove(TKey key, out TValue value)
        {
            // For reference types: use base.TryRemove and extract value from Lazy
            // For value types: the Lazy wrapper behavior is complex
            // Fall back to removing and hoping value was created
            if (base.TryRemove(key, out Lazy<TValue>? lazyValue))
            {
                try
                {
                    value = lazyValue!.Value;
                }
                catch
                {
                    // Value wasn't created yet - can't retrieve the original value
                    // Return default for value types, or we can't help
                    value = default!;
                }
                IncrementVersion();
                return true;
            }
            value = default!;
            return false;
        }
    }
}
