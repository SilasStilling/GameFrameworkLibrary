using GameFrameworkLibrary.Interfaces;
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
    /// Adapts a <see cref="TraceSource"/> to the <see cref="ILogger"/> interface.
    /// This class provides a bridge between the logging system and the game framework's logging abstraction.
    /// </summary>
    public class GameLoggerAdapter : ILogger
    {
        private readonly TraceSource _trace;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameLoggerAdapter"/> class with the specified <see cref="TraceSource"/>.
        /// </summary>
        /// <param name="trace">The <see cref="TraceSource"/> to use for logging.</param>
        public GameLoggerAdapter(TraceSource trace)
        {
            _trace = trace;
        }

        /// <summary>
        /// Logs a message with the specified severity level, log type, and optional offset.
        /// </summary>
        /// <param name="level">The severity level of the log (e.g., Information, Warning, Error).</param>
        /// <param name="logType">The category of the log (e.g., Game, Combat, Inventory).</param>
        /// <param name="message">The message to log.</param>
        /// <param name="offset">An optional offset to adjust the log ID (default is 1).</param>
        public void Log(TraceEventType level, LogType logType, string message, int offset = 1)
        {
            int id = (int)logType + offset;
            _trace.TraceEvent(level, id, message);
            _trace.Flush();
        }
    }
}