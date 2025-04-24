using GameFrameworkLibrary.Models.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents an object in the game that can provide loot.
    /// This interface defines the contract for objects that can be looted by creatures.
    /// </summary>
    public interface ILootable : IWorldObject
    {
        /// <summary>
        /// Indicates whether the object can be looted.
        /// </summary>
        bool IsLootable { get; }

        /// <summary>
        /// Retrieves the loot associated with the object.
        /// </summary>
        /// <param name="looter">The creature performing the looting.</param>
        /// <returns>An enumerable collection of items derived from <see cref="IItem"/>.</returns>
        IEnumerable<IItem> GetLoot(ICreature looter);
    }
}