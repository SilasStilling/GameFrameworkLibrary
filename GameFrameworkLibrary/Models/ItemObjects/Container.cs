using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace GameFrameworkLibrary.Models.ItemObjects
{
    public class Container : EnvironmentObject, ILootable
    {
        private readonly ILogger _logger;
        private readonly List<ItemBase> _items = new();

        public Container(string name, string? description, Position position, ILogger logger, bool isLootable = true, bool isRemovable = false)
            : base(name, description, position, isLootable, isRemovable)
        {
            _logger = logger;
        }
        public void AddItem(ItemBase item)
        {
            _items.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Item {item.Name} has been added to {Name} at {Position}");
        }

        public IEnumerable<ItemBase> GetLoot()
        {
            var loot = _items.ToList();
            _items.Clear();

            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Looted {loot.Count} items from {Name} at {Position}");
            return loot;
        }
        public override string ToString()
        {
            var itemList = _items.Count > 0
                ? string.Join(", ", _items.Select(i => i.Name))
                : "None";

            return $"{base.ToString()} [Items: {_items.Count}] [{itemList}]";
        }
    }
}
