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
        public bool IsLootable { get; internal set; }

        private readonly List<IItem> _items = new();
        private readonly ILogger _logger;

        public Container(string name, string description, Position position, ILogger logger, bool isLootable = true, bool isRemovable = false)
            : base(name, description, position, isRemovable)
        {
            IsLootable = isLootable;
            _logger = logger;
        }

        public void AddItem(IItem item)
        {
            _items.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Item '{item.Name}' added to container '{Name}' at {Position}");
        }

        public IEnumerable<IItem> GetLoot()
        {
            var loot = _items.ToList();
            _items.Clear();

            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Loot retrieved from container '{Name}' at {Position}. Items: {loot.Count}");

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
