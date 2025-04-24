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
    /// <summary>
    /// Represents a lootable object in the game world that contains a single item.
    /// Inherits from <see cref="EnvironmentObject"/> and implements <see cref="ILootable"/>.
    /// </summary>
    public class LootableObject : EnvironmentObject, ILootable
    {
        /// <summary>
        /// The item contained within the lootable object.
        /// </summary>
        private readonly IItem _itemInside;

        /// <summary>
        /// The logger used to log loot-related actions.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="LootableObject"/> class.
        /// </summary>
        /// <param name="item">The item contained within the lootable object.</param>
        /// <param name="position">The position of the lootable object in the game world.</param>
        /// <param name="logger">The logger instance for logging actions.</param>
        /// <param name="isLootable">Indicates whether the object can be looted (default is true).</param>
        public LootableObject(IItem item, Position position, ILogger logger, bool isLootable = true)
            : base(item.Name, item.Description, position, isRemovable: true)
        {
            IsLootable = isLootable;
            _itemInside = item;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves the item contained within the lootable object.
        /// </summary>
        /// <param name="looter">The creature performing the looting.</param>
        /// <returns>A collection containing the single item inside the lootable object.</returns>
        public IEnumerable<IItem> GetLoot(ICreature looter)
        {
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"{looter.Name} looted '{_itemInside.Name}' from wrapper at {Position}");

            return new[] { _itemInside };
        }

        /// <summary>
        /// Returns a string representation of the lootable object, including its name and the item it contains.
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()} [Item: {_itemInside.Name}]";
        }
    }
}