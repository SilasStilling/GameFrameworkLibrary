using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Logging;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Models.Environment;
using System.ComponentModel;


namespace GameFrameworkLibrary.Services
{
    public class InventoryService : IInventory
    {
        private readonly List<IDamageSource> _attackItems = new();
        private readonly List<IDefenceSource> _defenceItems = new();
        private readonly List<IUsable> _usables = new();
        private readonly ILogger _logger;

        public InventoryService(ILogger logger)
        {
            _logger = logger;
        }

        public void EquipAttackItem(IDamageSource attackItem)
        {
            _attackItems.Add(attackItem);
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,

                $"Equipped attack item: {((WorldObject)attackItem).Name}");
        }

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

        public void Loot(ICreature looter, ILootable source, World world)
        {
            if (!source.IsLootable)
            {
                _logger.Log(TraceEventType.Information, LogType.Inventory,
                    $"{looter.Name} tried to loot {source.Name}, but it's not lootable right now.");
                return;
            }

            var items = source.GetLoot();
            ProcessLoot(items);

            if (source is EnvironmentObject)
            {
                world.RemoveObject((EnvironmentObject)source);
                _logger.Log(TraceEventType.Information, LogType.Inventory,
                    $"{source.Name} removed after looting.");
            }
        }

        public void UseItem(ICreature user, IUsable item)
        {
            if (item is IUsable usable)
                usable.UseOn(user);
            else
                _logger.Log(TraceEventType.Warning, LogType.Inventory,
                    $"{item.Name} cannot be used by {user.Name}.");
        }

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
