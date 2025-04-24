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
        private readonly IItem _itemInside;
        private readonly ILogger _logger;

        public LootableObject(IItem item, Position position, ILogger logger, bool isLootable = true)
            : base(item.Name, item.Description, position, isRemovable: true)
        {
            IsLootable = isLootable;
            _itemInside = item;
            _logger = logger;
        }

        public IEnumerable<IItem> GetLoot(ICreature looter)
        {
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"{looter.Name} looted '{_itemInside.Name}' from wrapper at {Position}");

            return new[] { _itemInside };
        }
        public override string ToString()
        {
            return $"{base.ToString()} [Item: {_itemInside.Name}]";
        }
    }
}
