using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameFrameworkLibrary.Services
{
    public interface IInventory
    {
        void EquipAttackItem(IDamageSource attackItem);

        void EquipDefenceItem(IDefenceSource defenceItem);

        void ProcessLoot(IEnumerable<IItem> loot);

        IEnumerable<IUsable> GetUsables();

        void Loot(ICreature looter, ILootable source, World world);

        void UseItem(ICreature user, IUsable item);

        IEnumerable<IDamageSource> GetAttackItems();

        IEnumerable<IDefenceSource> GetDefenceItems();
    }
}
