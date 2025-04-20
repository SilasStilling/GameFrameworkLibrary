using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using GameFrameworkLibrary.Models.Base;
using System.Xml.Linq;

namespace GameFrameworkLibrary.Configuration
{
    /// <summary>
    /// Thrown when an error is found in the configuration file.
    /// </summary>
    public class ConfigurationException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationException"/> class with a specified error message.
        /// </summary>
        /// <param name="message">The message that describes the configuration error.</param>
        public ConfigurationException(string message) : base(message) { }
    }
    public class ConfigLoader 
    {
        public (WorldSettings worldSettings, LoggerSettings loggingConfig) Load(string xmlFile)
        {
            // 1) Validate existence and load XML document
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException($"Config file not found: {xmlFile}");

            // 2) Load the XML document
            var doc = XDocument.Load(xmlFile);

            // 3) Grab the root <Configuration> element
            var root = doc.Root
                ?? throw new ConfigurationException("Invalid configuration file: missing <Configuration> root.");

            // 4) Read the world settings
            var worldSettings = new WorldSettings
            {
                WorldWidth = ReadInt(root, "WorldWidth"),
                WorldHeight = ReadInt(root, "WorldHeight"),
                GameLevel = ReadEnum<GameLevel>(root, "GameLevel")
            };

            // 5) Initialize an empty LoggerSettings to hold logging settings
            var loggerSettings = new LoggerSettings();

            // 6) Parse the optional <Logging> section if present
            if (root.Element("Logging") is XElement loggingRoot)
            {
                // Read optional <GlobalSourceLevel>
                var globalLevel = loggingRoot.Element("GlobalSourceLevel");
                if (globalLevel != null
                    && Enum.TryParse<SourceLevels>(globalLevel.Value, out var gl))
                {
                    loggerSettings.LogLevel = gl;
                }

                ParseLogging(loggingRoot, loggerSettings);
            }

            return (worldSettings, loggerSettings);
        }

        #region Methods
        /// <summary>
        /// Parses the <Logging> section, filling in cfg.LogLevel and cfg.Listeners.
        /// </summary>
        private static void ParseLogging(XElement loggingRoot, LoggerSettings config)
        {
            var container = loggingRoot.Elements("Listeners");
            if (container == null)
                return;

            // 1) Find all the <Listener> children
            var listeners = container
                .Elements("Listener")               // all <Listener> entries

                // 2) Keep only those with a non-empty “type” attribute
                .Where(x => !string.IsNullOrWhiteSpace((string?)x.Attribute("type")))

                // 3) Turn each XElement into a ListenerConfig
                .Select(x =>
                {
                    // read required type and set FilterLevel to default level from read file
                    var cfg = new ListenerConfig
                    {
                        // a) required “type” attribute
                        Type = (string)x.Attribute("type")!,
                        FilterLevel = config.LogLevel,
                    };

                    // b) override level with optional <FilterLevel>
                    if (Enum.TryParse<SourceLevels>((string?)x.Element("FilterLevel"), out var lvl))
                        cfg.FilterLevel = lvl;

                    // c) everything else → Settings dictionary
                    cfg.Settings = x.Elements()
                                       .Where(e => e.Name.LocalName != "FilterLevel")
                                       .ToDictionary(
                                            e => e.Name.LocalName,
                                            e => e.Value
                                       );
                    return cfg;
                })

                // 4) Execute the query and collect results
                .ToList();

            // 5) If any were found, add them to our LoggerSettings
            if (listeners.Count > 0)
            {
                config.Listeners.AddRange(listeners);
            }
        }

        /// <summary>
        /// Reads an enum child element of type T and throws if missing or invalid.
        /// </summary>
        private static T ReadEnum<T>(XElement root, string name) where T : struct
        {
            var element = root.Element(name)
                ?? throw new ConfigurationException($"Missing <{name}> in config");

            if (!Enum.TryParse(element.Value, out T value))
                throw new ConfigurationException($"Invalid enum value for <{name}>: '{element.Value}'");

            return value;
        }

        /// <summary>
        /// Reads an integer child element and throws if missing or invalid.
        /// </summary>
        private static int ReadInt(XElement root, string name)
        {
            var element = root.Element(name)
                ?? throw new ConfigurationException($"Missing <{name}> in config");

            if (!int.TryParse(element.Value, out int value))
                throw new ConfigurationException($"Invalid integer for <{name}>: '{element.Value}'");

            return value;
        }
        #endregion
    }
}

