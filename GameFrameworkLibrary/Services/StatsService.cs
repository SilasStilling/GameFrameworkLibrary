using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Services
{
    public class StatsService : IStatsService
    {
        private readonly IInventory _inventory;

        public StatsService(IInventory inventory)
        {
            _inventory = inventory;
        }

        public int GetTotalBaseDamage()
          => _inventory.GetAttackItems().Sum(i => i.BaseDamage);

        public int GetTotalDamageReduction()
          => _inventory.GetDefenceItems().Sum(d => d.DamageReduction);

    }
}
