using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Services
{
    /// <summary>
    /// Provides functionality to calculate combat-related stats for a creature based on its inventory.
    /// Implements the <see cref="IStatsService"/> interface.
    /// </summary>
    public class StatsService : IStatsService
    {
        private readonly IInventory _inventory;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatsService"/> class.
        /// </summary>
        /// <param name="inventory">The inventory instance used to retrieve equipped items.</param>
        public StatsService(IInventory inventory)
        {
            _inventory = inventory;
        }

        /// <summary>
        /// Calculates the total base damage of all equipped attack items in the inventory.
        /// </summary>
        /// <returns>The sum of the base damage values of all attack items.</returns>
        public int GetTotalBaseDamage()
          => _inventory.GetAttackItems().Sum(i => i.BaseDamage);

        /// <summary>
        /// Calculates the total damage reduction of all equipped defense items in the inventory.
        /// </summary>
        /// <returns>The sum of the damage reduction values of all defense items.</returns>
        public int GetTotalDamageReduction()
          => _inventory.GetDefenceItems().Sum(d => d.DamageReduction);
    }
}