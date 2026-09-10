using TQVaultAE.TitanQuestDataProviders.Model;

namespace TQVaultAE.TitanQuestDataProviders.Decoders
{
    // TODO Find better name
    internal class ArzRecordStructureProvider
    {
        private const string TypeDirectory = "directory";
        private ArzRecord _result = null!;

        private static readonly SemaphoreSlim s_recordCreationSemaphore = new(1, 1);

        internal async Task<ArzRecord> GetArzRecordStructureAsync(List<ArzRecord> records)
        {
            string baseFolder = records[0].Info![..records[0].Info!.IndexOf('\\')];
            _result = new(TypeDirectory, baseFolder);

            List<Task> tasks = [];
            foreach (ArzRecord record in records)
                tasks.Add(ProcessRecord(record));

            Task.WaitAll(tasks);
            return _result;
        }

        private async Task ProcessRecord(ArzRecord record)
        {
            if (record.Info is null)
                return;

            string[] path = record.Info.Split('\\');
            ArzRecord currentElement = _result;

            for (int i = 1; i < path.Length; i++)
            {
                string subPath = path[i];

                ArzRecord? child = currentElement.Children.SingleOrDefault(x => x.Name == subPath);

                if (child is not null)
                {
                    currentElement = child;
                    continue;
                }

                ArzRecord newElement = await CreateRecordAsync(record, subPath).ConfigureAwait(false);
                currentElement.Children.Add(newElement);
                currentElement = newElement;
            }
        }

        private async Task<ArzRecord> CreateRecordAsync(ArzRecord record, string subPath)
        {
            try
            {
                s_recordCreationSemaphore.Wait();

                if (!subPath.EndsWith(".dbr"))
                    return new ArzRecord(TypeDirectory, subPath);

                record.Name = subPath;
                return record;
            }
            finally
            {
                s_recordCreationSemaphore.Release();
            }
        }
    }
}
