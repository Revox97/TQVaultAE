using System.Globalization;
using System.Text;

namespace TQVaultAE.Logger
{
    internal class MessageBuilder
    {
        private readonly string _timestamp;
        private readonly string _logLevel;
        private readonly string _message;
        private readonly string? _exception = null;

        internal MessageBuilder(LogLevel logLevel, string message, Exception? exception)
        {
            _timestamp = DateTime.Now.ToString(CultureInfo.InvariantCulture);
            _message = message;
            _logLevel = GetLevelString(logLevel);

            if (exception is not null)
                _exception = $"{exception.Message}\n{exception.StackTrace}";
        }

        private static string GetLevelString(LogLevel level)
        {
            return level switch
            {
                LogLevel.Debug => "DEBUG",
                LogLevel.Info => "INFO ",
                LogLevel.Warning => "WARN ",
                _ => "ERROR",
            };
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(_timestamp).Append(" | ").Append(_logLevel).Append(" | ").Append(_message);

            if (_exception is not null)
                sb.AppendLine(_exception);

            return sb.ToString();
        }
    }
}
