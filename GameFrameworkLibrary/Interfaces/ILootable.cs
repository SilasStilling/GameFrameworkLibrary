using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Defines a contract for objects that can provide loot in the game.
    /// Implementing classes must provide a method to retrieve a collection of items.
    /// </summary>
    public interface ILootable
    {

        /// <summary>
        /// Retrieves the loot associated with the object.
        /// </summary>
        /// <returns>An enumerable collection of items derived from <see cref="ItemBase"/>.</returns>
        IEnumerable<ItemBase> GetLoot();
    }
}
