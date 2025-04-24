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
using GameFrameworkLibrary.Factories;

namespace GameFrameworkLibrary
{
    public static class Framework
    {
        public static ServiceProvider Start(string configFilePath, string traceSourceName)
        {
            var services = new ServiceCollection();

            // cfg
            var (loggerAdapter, worldsettings) = InitializeLoggingAndConfiguration(configFilePath, traceSourceName);

            services.AddSingleton(loggerAdapter);
            services.AddSingleton(worldsettings);

            
            services.AddGameFrameworkLibrary();

            services.AddSingleton<IFactory<IUsable>>(sp =>
            {
                var combat = sp.GetRequiredService<ICombatService>();

                var factory = new Factory<IUsable>();

                return factory;
            });

            services.AddSingleton<IFactory<IWeapon>>(new Factory<IWeapon>());
            services.AddSingleton<IFactory<IArmor>>(new Factory<IArmor>());
            services.AddSingleton<IFactory<IUsable>>(new Factory<IUsable>());

            return services.BuildServiceProvider();
        }


        private static (ILogger loggerAdapter, WorldSettings worldSettings) InitializeLoggingAndConfiguration(
            string configFilePath,
            string traceSourceName)
        {
            var loader = new ConfigLoader();
            var (worldSettings, loggerSettings) = loader.Load(configFilePath);

            var trace = new TraceSource(traceSourceName)
            {
                Switch = { Level = loggerSettings.LogLevel } // uses that global setting
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

                    listener.Filter = new EventTypeFilter(ListenerConfig.FilterLevel);
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
