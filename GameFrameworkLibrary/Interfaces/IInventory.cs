using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameFrameworkLibrary.Interfaces
{
    public interface IInventory
    {
        void AddItem(IItem item);

        void EquipAttackItem(IDamageSource attackItem);

        void EquipDefenseItem(IDefenceSource defenseItem);

        void ProcessLoot(IEnumerable<IItem> loot);

        IEnumerable<IDamageSource> GetAttackItems();

        IEnumerable<IDefenceSource> GetDefenseItems();

        void Loot(ICreature looter, ILootable source, World world);
        IEnumerable<IItem> RemoveAllItems(ICreature creature);
    }
}
