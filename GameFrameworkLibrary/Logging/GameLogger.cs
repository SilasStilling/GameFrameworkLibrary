using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Logging
{
    public static class GameLogger
    {
        private static readonly TraceSource _ts = new("GameFramework", SourceLevels.All);

        static GameLogger() 
        {
            var consoleLogger = new ConsoleTraceListener();
            consoleLogger.Filter = new EventTypeFilter(SourceLevels.All);
            _ts.Listeners.Add(consoleLogger);
        }
        public static void Log(TraceEventType level, LogType logType, string message, int offset = 1)
        {
            int id = (int)logType + offset;
            _ts.TraceEvent(level, id, message);
            _ts.Flush();
        }
        public static TraceSource Trace => _ts;
    }
}
