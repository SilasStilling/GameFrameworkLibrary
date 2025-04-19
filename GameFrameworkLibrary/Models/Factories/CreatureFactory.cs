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
        private readonly ILogger _logger;
        private readonly IInventory _inventory;
        private readonly IDamageCalc _damageCalc;

        public CreatureFactory(
             ILogger logger,
             IInventory inventory,
             IDamageCalc damageCalc)
        {
            _logger = logger;
            _inventory = inventory;
            _damageCalc = damageCalc;
        }

        /// <inheritdoc/>
        public Creature Create(string name, Position position, int hitpoints, string? description = null)
        {
            return new Creature(name, description, hitpoints, position, _inventory, _logger, _damageCalc);
        }
    }
}
