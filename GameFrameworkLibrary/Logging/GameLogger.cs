using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Logging
{
    /// <summary>
    /// Provides a centralized logging utility for the game framework.
    /// This static class uses a <see cref="TraceSource"/> to log messages with various levels and categories.
    /// </summary>
    public static class GameLogger
    {
        private static readonly TraceSource _ts = new("GameFramework", SourceLevels.All);

        /// <summary>
        /// Initializes the logger with a default console trace listener.
        /// </summary>
        static GameLogger()
        {
            var consoleLogger = new ConsoleTraceListener();
            consoleLogger.Filter = new EventTypeFilter(SourceLevels.All);
            _ts.Listeners.Add(consoleLogger);
        }

        /// <summary>
        /// Logs a message with the specified severity level, log type, and optional offset.
        /// </summary>
        /// <param name="level">The severity level of the log (e.g., Information, Warning, Error).</param>
        /// <param name="logType">The category of the log (e.g., Game, Combat, Inventory).</param>
        /// <param name="message">The message to log.</param>
        /// <param name="offset">An optional offset to adjust the log ID (default is 1).</param>
        public static void Log(TraceEventType level, LogType logType, string message, int offset = 1)
        {
            int id = (int)logType + offset;
            _ts.TraceEvent(level, id, message);
            _ts.Flush();
        }

        /// <summary>
        /// Gets the underlying <see cref="TraceSource"/> used by the logger.
        /// </summary>
        public static TraceSource Trace => _ts;
    }
}