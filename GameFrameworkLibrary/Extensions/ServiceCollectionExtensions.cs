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
            services.AddSingleton<IDamageCalc, DamageCalc>();
            services.AddTransient<IInventory, InventoryService>();

            services.AddSingleton<ICreatureFactory, CreatureFactory>();
            services.AddSingleton<IAttackItemFactory, AttackItemFactory>();

            services.AddSingleton<IDefenceItemFactory, DefenceItemFactory>();

            // World
            services.AddSingleton<World>();

            return services;
        }
    }
}
