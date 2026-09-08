using TQVaultAE.Logging.LogWriters;

namespace TQVaultAE.Logging
{
    public sealed class LoggingFactory : ILoggingFactory
    {
        private readonly List<ILogger> _loggers = [];

        public ILoggingFactory AddLogger(string name, string path, LogLevel logLevel)
        {
            _loggers.Add(new Logger()
            {
                Name = name,
                Path = path,
                LogLevel = logLevel,
            });

            return this;
        }

        public ILogging Build()
        {
            if (OperatingSystem.IsOSPlatform("windows"))
                return new Logging(_loggers, new WindowsLogWriter());

            // TODO Adjust for future Linux support
            return new Logging(_loggers, null!);
        }
    }
}
