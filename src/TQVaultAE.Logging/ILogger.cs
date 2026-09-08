namespace TQVaultAE.Logging
{
    public interface ILogger
    {
        string Name { get; init; }
        string Path { get; init; }
        LogLevel LogLevel { get; init; }
    }
}
