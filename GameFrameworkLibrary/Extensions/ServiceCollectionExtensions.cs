using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using GameFrameworkLibrary.Models.Factories;

namespace GameFrameworkLibrary.Extensions
{
    internal static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddGameFrameworkLibrary(this IServiceCollection services)
        {
            // Registered services here
            services.AddSingleton<IStatsService, StatsService>();
            services.AddSingleton<ICombatService, CombatService>();
            services.AddSingleton<IMovementService, MovementService>();
            services.AddSingleton<IDamageCalc, DamageCalc>();
            services.AddTransient<IInventory, InventoryService>();


            services.AddSingleton<ICreatureFactory, CreatureFactory>();

            // World
            services.AddSingleton<World>();

            return services;
        }
    }
}
