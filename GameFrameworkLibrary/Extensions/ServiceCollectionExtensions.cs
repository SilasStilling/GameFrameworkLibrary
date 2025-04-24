using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Services;
using Microsoft.Extensions.DependencyInjection;
using GameFrameworkLibrary.Factories;
using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Observers;

namespace GameFrameworkLibrary.Extensions
{
    /// <summary>
    /// Provides extension methods for registering game framework services with the dependency injection container.
    /// </summary>
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Registers all necessary services, factories, and observers required by the game framework into the provided IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to which the services will be added.</param>
        /// <returns>The updated IServiceCollection with the game framework services registered.</returns>
        public static IServiceCollection AddGameFrameworkLibrary(this IServiceCollection services)
        {
            // Register core game services
            services.AddSingleton<IStatsService, StatsService>(); // Handles character stats management
            services.AddSingleton<ICombatService, CombatService>(); // Manages combat logic
            services.AddSingleton<IMovementService, MovementService>(); // Handles movement logic
            services.AddSingleton<IDamageCalc, DamageCalc>(); // Calculates damage in combat
            services.AddTransient<IInventory, InventoryService>(); // Manages inventory systems

            // Register observers for game events
            services.AddSingleton<HealthObserver>(); // Observes health changes in creatures
            services.AddSingleton<DeathObserver>(); // Observes death events in creatures

            // Register factories
            services.AddSingleton<ICreatureFactory, CreatureFactory>(); // Creates creature instances

            // Register the game world
            services.AddSingleton<World>(); // Represents the game world and its state

            return services;
        }
    }
}