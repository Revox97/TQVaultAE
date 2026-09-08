using System.Runtime.Versioning;

namespace TQVaultAE.Logging.LogWriters
{
    [SupportedOSPlatform("windows")]
    internal class WindowsLogWriter : ILogWriter
    {
        [SupportedOSPlatform("windows")]
        public void WriteDebug(string message)
        {
            throw new NotImplementedException();
        }

        [SupportedOSPlatform("windows")]
        public void WriteException(string message, Exception ex)
        {
            throw new NotImplementedException();
        }

        [SupportedOSPlatform("windows")]
        public void WriteInfo(string message)
        {
            throw new NotImplementedException();
        }

        [SupportedOSPlatform("windows")]
        public void WriteWarning(string message)
        {
            throw new NotImplementedException();
        }
    }
}
