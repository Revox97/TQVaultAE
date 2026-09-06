namespace TQVaultAE.Backup
{
    public class BackupService
    {
        private IBackupStrategy _backupStrategy;

        public BackupService()
        {
            // TODO Set correct backup Strategy
            _backupStrategy = default;
        }
    }
}
