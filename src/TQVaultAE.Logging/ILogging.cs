namespace TQVaultAE.Logging
{
    public interface ILogging
    {
        ILogger this[string loggerName] { get; }
    }
}