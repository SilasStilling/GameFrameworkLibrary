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
    /// <summary>
    /// Represents an inventory system for managing items, equipment, and loot in the game framework.
    /// This interface defines methods for adding, equipping, processing, and removing items.
    /// </summary>
    public interface IInventory
    {
        /// <summary>
        /// Adds an item to the inventory.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void AddItem(IItem item);

        /// <summary>
        /// Equips an attack item (e.g., weapon) from the inventory.
        /// </summary>
        /// <param name="attackItem">The attack item to equip.</param>
        void EquipAttackItem(IDamageSource attackItem);

        /// <summary>
        /// Equips a defense item (e.g., armor) from the inventory.
        /// </summary>
        /// <param name="defenseItem">The defense item to equip.</param>
        void EquipDefenceItem(IDefenceSource defenseItem);

        /// <summary>
        /// Processes loot by adding items to the inventory.
        /// </summary>
        /// <param name="loot">The collection of loot items to process.</param>
        void ProcessLoot(IEnumerable<IItem> loot);

        /// <summary>
        /// Retrieves all attack items (e.g., weapons) currently in the inventory.
        /// </summary>
        /// <returns>An enumerable of attack items.</returns>
        IEnumerable<IDamageSource> GetAttackItems();

        /// <summary>
        /// Retrieves all defense items (e.g., armor) currently in the inventory.
        /// </summary>
        /// <returns>An enumerable of defense items.</returns>
        IEnumerable<IDefenceSource> GetDefenceItems();

        /// <summary>
        /// Loots items from a source and adds them to the inventory.
        /// </summary>
        /// <param name="looter">The creature performing the looting.</param>
        /// <param name="source">The lootable source.</param>
        /// <param name="world">The game world where the looting occurs.</param>
        void Loot(ICreature looter, ILootable source, World world);

        /// <summary>
        /// Removes all items from the inventory and returns them.
        /// </summary>
        /// <param name="creature">The creature whose inventory is being cleared.</param>
        /// <returns>An enumerable of removed items.</returns>
        IEnumerable<IItem> RemoveAllItems(ICreature creature);
    }
}