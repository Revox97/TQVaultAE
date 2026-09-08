using TQVaultAE.Logging.LogWriters;

namespace TQVaultAE.Logging
{
    /// <summary>
    /// Represents a collection of <see cref="ILogger"/>s.
    /// </summary>
    public class Logging : ILogging
    {
        private readonly List<ILogger> _loggers;
        private readonly ILogWriter _logWriter;

        /// <summary>
        /// Gets an <see cref="ILogger"/> by its <paramref name="loggerName"/>.
        /// </summary>
        /// <param name="loggerName">The name of the <see cref="ILogger"/>.</param>
        /// <returns>A <see cref="ILogger"/>.</returns>
        /// <exception cref="KeyNotFoundException">Thrown, if no <see cref="ILogger"/> with the <paramref name="loggerName"/> does exist.</exception>
        public ILogger this[string loggerName]
        {
            get => _loggers.SingleOrDefault(x => x.Name.Equals(loggerName, StringComparison.InvariantCultureIgnoreCase))
                ?? throw new KeyNotFoundException($"Logger with name '{loggerName}' does not exist.");
        }

        internal Logging(List<ILogger> loggers, ILogWriter logWriter)
        {
            _loggers = loggers;
            _logWriter = logWriter;
        }
    }
}
