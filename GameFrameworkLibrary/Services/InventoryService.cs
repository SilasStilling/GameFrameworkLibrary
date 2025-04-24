using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Logging;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Environment;
using System.ComponentModel;

namespace GameFrameworkLibrary.Services
{
    /// <summary>
    /// Manages the inventory system for creatures in the game framework.
    /// Handles adding, equipping, looting, using, and removing items.
    /// Implements the <see cref="IInventory"/> interface.
    /// </summary>
    public class InventoryService : IInventory
    {
        private readonly List<IDamageSource> _attackItems = new();
        private readonly List<IDefenceSource> _defenceItems = new();
        private readonly List<IUsable> _usables = new();
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="InventoryService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging inventory actions.</param>
        public InventoryService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Adds an item to the inventory and processes it based on its type.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(IItem item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));
            ProcessLoot(new[] { item });
        }

        /// <summary>
        /// Equips an attack item (e.g., weapon) to the inventory.
        /// </summary>
        /// <param name="attackItem">The attack item to equip.</param>
        public void EquipAttackItem(IDamageSource attackItem)
        {
            _attackItems.Add(attackItem);
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Equipped attack item: {((WorldObject)attackItem).Name}");
        }

        /// <summary>
        /// Equips a defense item (e.g., armor) to the inventory.
        /// </summary>
        /// <param name="defenseSource">The defense item to equip.</param>
        public void EquipDefenceItem(IDefenceSource defenseSource)
        {
            _defenceItems.Add(defenseSource);
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Equipped defence item: {((WorldObject)defenseSource).Name}");
        }

        /// <inheritdoc/>
        public IEnumerable<IDamageSource> GetAttackItems() => _attackItems.AsReadOnly();

        /// <inheritdoc/>
        public IEnumerable<IDefenceSource> GetDefenceItems() => _defenceItems.AsReadOnly();

        /// <inheritdoc/>
        public IEnumerable<IUsable> GetUsables() => _usables.AsReadOnly();

        /// <summary>
        /// Loots items from a lootable source and adds them to the inventory.
        /// Removes the source from the world if it is an environment object.
        /// </summary>
        /// <param name="looter">The creature performing the looting.</param>
        /// <param name="source">The lootable source.</param>
        /// <param name="world">The game world where the looting occurs.</param>
        public void Loot(ICreature looter, ILootable source, World world)
        {
            if (!source.IsLootable)
            {
                _logger.Log(TraceEventType.Information, LogType.Inventory,
                    $"{looter.Name} tried to loot {source.Name}, but it's not lootable right now.");
                return;
            }

            var items = source.GetLoot(looter);
            ProcessLoot(items);

            if (source is EnvironmentObject)
            {
                world.RemoveObject((EnvironmentObject)source);
                _logger.Log(TraceEventType.Information, LogType.Inventory,
                    $"{source.Name} removed after looting.");
            }
        }

        /// <summary>
        /// Uses a usable item on a creature.
        /// </summary>
        /// <param name="user">The creature using the item.</param>
        /// <param name="item">The usable item to use.</param>
        public void UseItem(ICreature user, IUsable item)
        {
            if (item is IUsable usable)
                usable.UseOn(user);
            else
                _logger.Log(TraceEventType.Warning, LogType.Inventory,
                    $"{item.Name} cannot be used by {user.Name}.");
        }

        /// <summary>
        /// Removes all items from the inventory and returns them.
        /// </summary>
        /// <param name="creature">The creature whose inventory is being cleared.</param>
        /// <returns>A collection of removed items.</returns>
        public IEnumerable<IItem> RemoveAllItems(ICreature creature)
        {
            var items = new List<IItem>();
            items.AddRange(_attackItems.Cast<IItem>());
            items.AddRange(_defenceItems.Cast<IItem>());

            _attackItems.Clear();
            _defenceItems.Clear();

            _logger.Log(TraceEventType.Information, LogType.Inventory,
                $"{creature.Name} dropped {items.Count} items on death.");

            return items;
        }

        /// <summary>
        /// Processes a collection of loot items and categorizes them into attack items, defense items, or usables.
        /// </summary>
        /// <param name="loot">The collection of loot items to process.</param>
        public void ProcessLoot(IEnumerable<IItem> loot)
        {
            foreach (var item in loot)
            {
                switch (item)
                {
                    case IDamageSource attackItem:
                        EquipAttackItem(attackItem);
                        break;

                    case IDefenceSource defenseItem:
                        EquipDefenceItem(defenseItem);
                        break;

                    case IUsable usableItem:
                        _usables.Add(usableItem);
                        _logger.Log(
                            TraceEventType.Information,
                            LogType.Inventory,
                            $"Stored usable item: {((WorldObject)usableItem).Name}");
                        break;

                    default:
                        _logger.Log(
                            TraceEventType.Warning,
                            LogType.Inventory,
                            $"Unsupported looted item type: {item.Name}");
                        break;
                }
            }
        }
    }
}