namespace TQVaultAE.Logging
{
    public class Logger : ILogger
    {
        public string Name { get; init; } = string.Empty;
        public string Path { get; init; } = string.Empty;
        public LogLevel LogLevel { get; init; } = LogLevel.Error;
    }
}
