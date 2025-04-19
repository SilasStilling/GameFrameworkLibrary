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
    public class GameLoggerAdapter : ILogger
    {
        private readonly TraceSource _trace;

        public GameLoggerAdapter(TraceSource trace)
        {
            _trace = trace;
        }
        public void Log(TraceEventType level, LogType logType, string message, int offset = 1)
        {
            int id = (int)logType + offset;
            _trace.TraceEvent(level, id, message);
            _trace.Flush();
        }
    }

}
