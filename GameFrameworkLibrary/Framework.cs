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
    /// <summary>
    /// Provides the entry point for initializing and configuring the game framework.
    /// Sets up dependency injection, logging, and configuration for the game.
    /// </summary>
    public static class Framework
    {
        /// <summary>
        /// Starts the game framework by configuring services, logging, and world settings.
        /// </summary>
        /// <param name="configFilePath">The path to the configuration file.</param>
        /// <param name="traceSourceName">The name of the trace source for logging.</param>
        /// <returns>A configured <see cref="ServiceProvider"/> instance.</returns>
        public static ServiceProvider Start(string configFilePath, string traceSourceName)
        {
            var services = new ServiceCollection();

            // Initialize logging and configuration
            var (loggerAdapter, worldSettings) = InitializeLoggingAndConfiguration(configFilePath, traceSourceName);

            // Register core services
            services.AddSingleton(loggerAdapter);
            services.AddSingleton(worldSettings);

            // Register game framework services
            services.AddGameFrameworkLibrary();

            // Register factories for game objects
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

        /// <summary>
        /// Initializes logging and configuration for the game framework.
        /// </summary>
        /// <param name="configFilePath">The path to the configuration file.</param>
        /// <param name="traceSourceName">The name of the trace source for logging.</param>
        /// <returns>A tuple containing the logger adapter and world settings.</returns>
        private static (ILogger loggerAdapter, WorldSettings worldSettings) InitializeLoggingAndConfiguration(
            string configFilePath,
            string traceSourceName)
        {
            var loader = new ConfigLoader();
            var (worldSettings, loggerSettings) = loader.Load(configFilePath);

            var trace = new TraceSource(traceSourceName)
            {
                Switch = { Level = loggerSettings.LogLevel }
            };

            // Configure trace listeners
            if (loggerSettings.Listeners.Count > 0)
            {
                foreach (var listenerConfig in loggerSettings.Listeners)
                {
                    TraceListener listener = listenerConfig.Type switch
                    {
                        "Console" => new ConsoleTraceListener(),
                        "File" when listenerConfig.Settings.TryGetValue("Path", out var path) => new TextWriterTraceListener(path),
                        _ => throw new InvalidOperationException($"Unknown listener type '{listenerConfig.Type}'")
                    };

                    listener.Filter = new EventTypeFilter(listenerConfig.FilterLevel);
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