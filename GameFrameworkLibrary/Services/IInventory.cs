using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameFrameworkLibrary.Services
{
    public interface IInventory : ICombatStats
    {
        void EquipAttackItem(IDamageSource attackItem);

        void EquipDefenceItem(IDefenceSource defenceItem);

        void ProcessLoot(IEnumerable<WorldObject> loot);

        IEnumerable<IUsable> GetUsables();
    }
}
