using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Factories
{
    internal class CreatureFactory : ICreatureFactory
    {
        private readonly IStatsService _statsService;
        private readonly ICombatService _combatService;
        private readonly IMovementService _movementService;
        private readonly IInventory _inventory;

        public CreatureFactory(
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventory inventoryService)
        {
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventory = inventoryService;
        }

        /// <inheritdoc/>
        public Creature Create(string name, string description, int hitpoints, Position position)
        {
            return new Creature(
                name,
                description,
                hitpoints,
                position,
                _statsService,
                _combatService,
                _movementService,
                _inventory);
        }
    }
}
