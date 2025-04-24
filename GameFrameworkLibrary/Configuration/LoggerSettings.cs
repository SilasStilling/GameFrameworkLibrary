using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Configuration
{
    /// <summary>
    /// Represents the configuration for a single log listener.
    /// </summary>
    public class ListenerConfig
    {
        /// <summary>
        /// The type of the listener (e.g., "Console", "File").
        /// </summary>
        public required string Type { get; set; }

        /// <summary>
        /// The minimum log level that this listener will process.
        /// Defaults to <see cref="SourceLevels.All"/>.
        /// </summary>
        public SourceLevels FilterLevel { get; set; } = SourceLevels.All;

        /// <summary>
        /// Additional settings for the listener, stored as key-value pairs.
        /// For example, file paths for file-based listeners.
        /// </summary>
        public Dictionary<string, string> Settings { get; set; } = new();
    }

    /// <summary>
    /// Represents the logging configuration for the application.
    /// </summary>
    public class LoggerSettings
    {
        /// <summary>
        /// The global log level for the application.
        /// Determines the minimum severity of messages that will be logged.
        /// Defaults to <see cref="SourceLevels.All"/>.
        /// </summary>
        public SourceLevels LogLevel { get; set; } = SourceLevels.All;

        /// <summary>
        /// A collection of log listeners, each represented by a <see cref="ListenerConfig"/>.
        /// These listeners define where and how log messages are output (e.g., console, file).
        /// </summary>
        public List<ListenerConfig> Listeners { get; set; } = new();
    }
}