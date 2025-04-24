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
    /// <summary>
    /// Represents a container object in the game world that can hold items and be looted by creatures.
    /// Inherits from <see cref="EnvironmentObject"/> and implements <see cref="ILootable"/>.
    /// </summary>
    public class Container : EnvironmentObject, ILootable
    {
        /// <summary>
        /// A collection of items stored in the container.
        /// </summary>
        private readonly List<IItem> _items = new();

        /// <summary>
        /// The logger used to log container-related actions.
        /// </summary>
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="Container"/> class.
        /// </summary>
        /// <param name="name">The name of the container.</param>
        /// <param name="description">A description of the container.</param>
        /// <param name="position">The position of the container in the game world.</param>
        /// <param name="logger">The logger instance for logging actions.</param>
        /// <param name="isLootable">Indicates whether the container can be looted (default is true).</param>
        /// <param name="isRemovable">Indicates whether the container can be removed from the game world (default is false).</param>
        public Container(string name, string description, Position position, ILogger logger, bool isLootable = true, bool isRemovable = false)
            : base(name, description, position, isRemovable)
        {
            IsLootable = isLootable;
            _logger = logger;
        }

        /// <summary>
        /// Adds an item to the container.
        /// </summary>
        /// <param name="item">The item to add.</param>
        public void AddItem(IItem item)
        {
            _items.Add(item);

            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"Item '{item.Name}' added to container '{Name}' at {Position}");
        }

        /// <summary>
        /// Retrieves and removes all items from the container.
        /// </summary>
        /// <param name="looter">The creature performing the looting.</param>
        /// <returns>A collection of items that were in the container.</returns>
        public IEnumerable<IItem> GetLoot(ICreature looter)
        {
            var loot = _items.ToList();
            _items.Clear();

            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"{looter.Name} looted {loot.Count} items from container '{Name}' at {Position}.");

            return loot;
        }

        /// <summary>
        /// Returns a string representation of the container, including its name, position, and items.
        /// </summary>
        public override string ToString()
        {
            var itemList = _items.Count > 0
                ? string.Join(", ", _items.Select(i => i.Name))
                : "None";

            return $"{base.ToString()} [Items: {_items.Count}] [{itemList}]";
        }

        /// <summary>
        /// Allows peeking at the items in the container without removing them.
        /// </summary>
        /// <returns>A read-only collection of items currently in the container.</returns>
        public IEnumerable<IItem> PeekLoot() => _items.AsReadOnly();
    }
}