using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.ItemObjects
{
    public class LootableObject : EnvironmentObject, ILootable
    {
        private readonly ItemBase _itemInside;
        private readonly ILogger _logger;

        public LootableObject(ItemBase item, Position position, ILogger logger)
            : base(item.Name, item.Description, position, isLootable: true, isRemovable: true)
        {
            _itemInside = item;
            _logger = logger;
        }

        public IEnumerable<ItemBase> GetLoot() 
        {
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Looted {_itemInside.Name} from {Name} at {Position}");
            return new List<ItemBase> { _itemInside };
        }
        public override string ToString()
        {
            return $"{base.ToString()} [Item: {_itemInside.Name}]";
        }
    }
}
