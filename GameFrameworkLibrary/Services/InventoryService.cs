using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Logging;
using GameFrameworkLibrary.Models.Base;


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

        public int GetTotalBaseDamage() => _attackItems.Sum(item => item.BaseDamage);

        public int GetTotalDamageReduction() => _defenceItems.Sum(item => item.DamageReduction);

        public IEnumerable<IUsable> GetUsables() => _usables.AsReadOnly();

        public void ProcessLoot(IEnumerable<WorldObject> loot)
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
