using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Observers;
using GameFrameworkLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Factories
{
    /// <summary>
    /// Factory class responsible for creating instances of creatures in the game.
    /// </summary>
    internal class CreatureFactory : ICreatureFactory
    {
        private readonly IStatsService _statsService;
        private readonly ICombatService _combatService;
        private readonly IMovementService _movementService;
        private readonly IInventory _inventory;

        private readonly HealthObserver _healthObserver;
        private readonly DeathObserver _deathObserver;

        /// <summary>
        /// Initializes a new instance of the <see cref="CreatureFactory"/> class.
        /// </summary>
        /// <param name="statsService">Service for managing creature stats.</param>
        /// <param name="combatService">Service for handling combat logic.</param>
        /// <param name="movementService">Service for managing movement logic.</param>
        /// <param name="inventory">Service for managing creature inventory.</param>
        /// <param name="healthObserver">Observer for monitoring health changes.</param>
        /// <param name="deathObserver">Observer for monitoring death events.</param>
        public CreatureFactory(
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventory inventory,
            HealthObserver healthObserver,
            DeathObserver deathObserver)
        {
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventory = inventory;
            _healthObserver = healthObserver;
            _deathObserver = deathObserver;
        }

        /// <summary>
        /// Creates a new creature with the specified attributes and subscribes it to the death observer.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">A description of the creature.</param>
        /// <param name="hitpoints">The initial hit points of the creature.</param>
        /// <param name="position">The starting position of the creature in the game world.</param>
        /// <returns>A new instance of <see cref="Creature"/>.</returns>
        public Creature Create(string name, string description, int hitpoints, Position position)
        {
            // Create a new DefaultCreature instance with the provided attributes and services.
            var creature = new DefaultCreature(
                name,
                description,
                hitpoints,
                position,
                _statsService,
                _combatService,
                _movementService,
                _inventory);

            // Subscribe the creature to the death observer to handle death-related events.
            _deathObserver.Subscribe(creature);

            return creature;
        }
    }
}