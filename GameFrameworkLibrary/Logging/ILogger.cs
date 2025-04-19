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
    /// </summary>
    public interface ILogger
    {

        /// <summary>
        /// Logs a message with the specified severity level and log type.
        /// </summary>
        /// <param name="level">The severity level of the log (e.g., Information, Warning, Error).</param>
        /// <param name="logType">The type of log (e.g., Debug, System, Game).</param>
        /// <param name="message">The message to log.</param>
        void Log(
            TraceEventType level,
            LogType logType,
            string message,
            int offset = 1);
    }
}
