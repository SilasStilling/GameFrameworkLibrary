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

namespace GameFrameworkLibrary.Configuration
{
    public class ConfigLoader 
    {
        private readonly XmlDocument _document = new();
        public (WorldSettings worldSettings, LoggerSettings loggingConfig) Load(string xmlFile)
        {
            // 1) Validate existence and load XML document
            if (!File.Exists(xmlFile))
                throw new FileNotFoundException($"Config file not found: {xmlFile}");

            // 2) Load the XML document
            _document.Load(xmlFile);

            // 3) Grab the root <Configuration> element
            var root = _document.DocumentElement ?? throw new ConfigurationException("Invalid configuration file format");

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
            var loggingElement = root.SelectSingleNode("Logging");
            if (loggingElement != null)
                ParseLogging(loggingElement, loggerSettings);

            return (worldSettings, loggerSettings);
        }
        /// <summary>
        /// Reads an integer child element and throws if missing or invalid.
        /// </summary>
        private static int ReadInt(XmlElement root, string name)
        {
            var node = root[name] ?? throw new ConfigurationException($"Missing <{name}> in config");

            if (!int.TryParse(node.InnerText, out int val))
                throw new ConfigurationException($"Invalid integer for <{name}>: '{node.InnerText}'");

            return val;
        }
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

        /// <summary>
        /// Parses the <Logging> section, filling in cfg.LogLevel and cfg.Listeners.
        /// </summary>
        private static void ParseLogging(XmlNode loggingElement, LoggerSettings config)
        {
            // 1) Read the <SourceLevel> element into cfg.LogLevel (if present)
            var globalSrcLvlNode = loggingElement.SelectSingleNode("GlobalSourceLevel");
            if (globalSrcLvlNode != null && Enum.TryParse(globalSrcLvlNode.InnerText, out SourceLevels globalLvl))
                config.LogLevel = globalLvl;

            // 2) Find all <Listener> entries under <Listeners>
            var listenerElements = loggingElement.SelectNodes("Listeners/Listener");
            if (listenerElements == null) return;

            // 3) For each Listener node, capture type and any custom settings
            foreach (XmlNode ln in listenerElements)
            {
                // a) Must have a type attribute
                var typeAttr = ln.Attributes?["type"]?.Value;
                if (string.IsNullOrWhiteSpace(typeAttr)) continue;

                // b) Create a new ListenerConfig and populate its settings dictionary
                var listener = new ListenerConfig { Type = typeAttr };

                // c) Read any <FilterLevel> override
                var filterNode = ln.SelectSingleNode("FilterLevel");
                if (filterNode != null
                    && Enum.TryParse(filterNode.InnerText, out SourceLevels filterLvl))
                {
                    listener.FilterLevel = filterLvl;
                }

                // d) Read the rest of the child settigns
                foreach (XmlNode child in ln.ChildNodes)
                {
                    if (child.Name == "FilterLevel")
                        continue;

                    listener.Settings[child.Name] = child.InnerText;
                }

                // e) Add to the master list on GameConfig
                config.Listeners.Add(listener);
            }
        }

        /// <summary>
        /// Reads an enum child element of type T and throws if missing or invalid.
        /// </summary>
        private static T ReadEnum<T>(XmlElement root, string name) where T : struct
        {
            var node = root[name] ?? throw new ConfigurationException($"Missing <{name}> in config");

            if (!Enum.TryParse(node.InnerText, out T val))
                throw new ConfigurationException($"Invalid enum value for <{name}>: '{node.InnerText}'");

            return val;
        }
    }
}
