
namespace TQVaultAE.Logger.Sinks
{
    internal class ConsoleSink : ISink
    {
        private readonly ConsoleColor _originalColor = Console.ForegroundColor;

        public void Write(LogLevel logLevel, string message, Exception? exception = null)
        {
            SetConsoleColor(logLevel);
            Console.Write(new MessageBuilder(logLevel, message, exception).ToString());
            Console.ForegroundColor = _originalColor;
        }

        private static void SetConsoleColor(LogLevel logLevel)
        {
            Console.ForegroundColor = logLevel switch
            {
                LogLevel.Debug => ConsoleColor.Gray,
                LogLevel.Info => ConsoleColor.White,
                LogLevel.Warning => ConsoleColor.Yellow,
                _ => ConsoleColor.Red,
            };
        }
    }
}
