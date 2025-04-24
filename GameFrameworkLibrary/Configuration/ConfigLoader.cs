using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Diagnostics;
using System.Xml.Linq;
using GameFrameworkLibrary.Enums;

namespace GameFrameworkLibrary.Configuration
{
    /// <summary>
    /// The ConfigLoader class is responsible for loading and parsing configuration data
    /// from an XML file. It extracts settings for the game world and logging system
    /// and maps them to strongly-typed objects.
    /// </summary>
    public class ConfigLoader
    {
        /// <summary>
        /// Loads the configuration from the specified XML file and returns the parsed
        /// world settings and logging configuration.
        /// </summary>
        /// <param name="xmlFile">The path to the XML configuration file.</param>
        /// <returns>A tuple containing WorldSettings and LoggerSettings objects.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the XML file does not exist.</exception>
        /// <exception cref="ConfigurationException">Thrown if the XML structure is invalid.</exception>
        public (WorldSettings worldSettings, LoggerSettings loggingConfig) Load(string xmlFile)
        {
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException($"Config file not found: {xmlFile}");

            var doc = XDocument.Load(xmlFile);

            var root = doc.Root
                ?? throw new ConfigurationException("Invalid configuration file: missing <Configuration> root.");

            // Parse world settings
            var worldSettings = new WorldSettings
            {
                WorldWidth = ReadInt(root, "WorldWidth"),
                WorldHeight = ReadInt(root, "WorldHeight"),
                GameLevel = ReadEnum<GameLevel>(root, "GameLevel")
            };

            // Parse logging settings
            var loggerSettings = new LoggerSettings();

            if (root.Element("Logging") is XElement loggingRoot)
            {
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
        /// Parses the logging configuration from the XML and populates the LoggerSettings object.
        /// </summary>
        /// <param name="loggingRoot">The root XML element for logging configuration.</param>
        /// <param name="config">The LoggerSettings object to populate.</param>
        private static void ParseLogging(XElement loggingRoot, LoggerSettings config)
        {
            var container = loggingRoot.Elements("Listeners");
            if (container == null)
                return;

            var listeners = container
                .Elements("Listener")
                .Where(x => !string.IsNullOrWhiteSpace((string?)x.Attribute("type")))
                .Select(x =>
                {
                    var cfg = new ListenerConfig
                    {
                        Type = (string)x.Attribute("type")!,
                        FilterLevel = config.LogLevel,
                    };

                    if (Enum.TryParse<SourceLevels>((string?)x.Element("FilterLevel"), out var lvl))
                        cfg.FilterLevel = lvl;

                    cfg.Settings = x.Elements()
                                       .Where(e => e.Name.LocalName != "FilterLevel")
                                       .ToDictionary(
                                            e => e.Name.LocalName,
                                            e => e.Value
                                       );
                    return cfg;
                })
                .ToList();

            if (listeners.Count > 0)
            {
                config.Listeners.AddRange(listeners);
            }
        }

        /// <summary>
        /// Reads an enum value from the XML element and converts it to the specified type.
        /// </summary>
        /// <typeparam name="T">The enum type to parse.</typeparam>
        /// <param name="root">The root XML element.</param>
        /// <param name="name">The name of the XML element to read.</param>
        /// <returns>The parsed enum value.</returns>
        /// <exception cref="ConfigurationException">Thrown if the element is missing or invalid.</exception>
        private static T ReadEnum<T>(XElement root, string name) where T : struct
        {
            var element = root.Element(name)
                ?? throw new ConfigurationException($"Missing <{name}> in config");

            if (!Enum.TryParse(element.Value, out T value))
                throw new ConfigurationException($"Invalid enum value for <{name}>: '{element.Value}'");

            return value;
        }

        /// <summary>
        /// Reads an integer value from the XML element.
        /// </summary>
        /// <param name="root">The root XML element.</param>
        /// <param name="name">The name of the XML element to read.</param>
        /// <returns>The parsed integer value.</returns>
        /// <exception cref="ConfigurationException">Thrown if the element is missing or invalid.</exception>
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