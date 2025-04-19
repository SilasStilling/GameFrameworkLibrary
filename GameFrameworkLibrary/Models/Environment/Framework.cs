using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Extensions;
using GameFrameworkLibrary.Logging;
using GameFrameworkLibrary.Interfaces;

namespace GameFrameworkLibrary.Models.Environment
{
    public static class Framework
    {
        public static ServiceProvider Start() 
        {
            var services = new ServiceCollection();

            //cfg
            var (loggerAdapter, worldsettings) = InitializeLoggingAndConfiguration();

            // Services
            services.AddGameFrameworkLibrary();
            services.AddSingleton<ILogger>(loggerAdapter);
            services.AddSingleton(worldsettings);

            return services.BuildServiceProvider();
        }

        private static (ILogger loggerAdapter, WorldSettings worldSettings) InitializeLoggingAndConfiguration() 
        {
            var loader = new ConfigLoader();
            var (worldSettings, loggerSettings) = loader.Load("config.xml");

            var trace = new TraceSource("GameFrameworkLibrary")
            {
                Switch = { Level = loggerSettings.LogLevel }
            };

            if (loggerSettings.Listeners.Count > 0) 
            {
                foreach (var ListenerConfig in loggerSettings.Listeners)
                {
                    TraceListener listener = ListenerConfig.Type switch
                    {
                        "Console" => new ConsoleTraceListener(),
                        "File" when ListenerConfig.Settings.TryGetValue("Path", out var path) => new TextWriterTraceListener(path),
                        _ => throw new InvalidOperationException($"Unknown listener type '{ListenerConfig.Type}'")
                    };
                    listener.Filter = new EventTypeFilter(loggerSettings.LogLevel);
                    trace.Listeners.Add(listener);
                }
            }
            else
            {
                trace.Listeners.Add(new ConsoleTraceListener());
            }
            var loggerAdapter = new GameLoggerAdapter(trace);

            return (loggerAdapter, worldSettings);
        }
    }
}
