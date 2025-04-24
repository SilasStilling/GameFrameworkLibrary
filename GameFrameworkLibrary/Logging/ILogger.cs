using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Defines a contract for logging messages with a specified severity level and log type.
    /// This interface provides a standardized way to log events across the game framework.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Logs a message with the specified severity level, log type, and optional offset.
        /// </summary>
        /// <param name="level">The severity level of the log (e.g., Information, Warning, Error).</param>
        /// <param name="logType">The category of the log (e.g., Game, Combat, Inventory).</param>
        /// <param name="message">The message to log.</param>
        /// <param name="offset">An optional offset to adjust the log ID (default is 1).</param>
        void Log(
            TraceEventType level,
            LogType logType,
            string message,
            int offset = 1);
    }
}