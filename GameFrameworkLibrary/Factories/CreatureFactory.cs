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

        /// <inheritdoc/>
        public Creature Create(string name, string description, int hitpoints, Position position)
        {
            var creature = new DefaultCreature(
                name,
                description,
                hitpoints,
                position,
                _statsService,
                _combatService,
                _movementService,
                _inventory);

            _deathObserver.Subscribe(creature);

            return creature;
        }
    }
}
