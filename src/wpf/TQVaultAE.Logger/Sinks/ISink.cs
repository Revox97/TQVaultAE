namespace TQVaultAE.Logger.Sinks
{
    internal interface ISink
    {
        void Write(LogLevel logLevel, string message, Exception? details = null);
    }
}
