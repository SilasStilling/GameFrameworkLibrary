using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Configuration
{
    public class ListenerConfig
    {
        public required string Type { get; set; }
        public SourceLevels? FilterLevel { get; set; }
        public Dictionary<string, string> Settings { get; set; } = new Dictionary<string, string>();
    }
    public class LoggerSettings
    {
        public SourceLevels LogLevel { get; set; } = SourceLevels.All;
        public List<ListenerConfig> Listeners { get; set; } = new List<ListenerConfig>();
    }
}
