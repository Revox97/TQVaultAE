namespace TQVaultAE.Logging.LogWriters
{
    internal interface ILogWriter
    {
        void WriteDebug(string message);
        void WriteInfo(string message);
        void WriteWarning(string message);
        void WriteException(string message, Exception ex);
    }
}
