using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Factories
{
    /// <summary>
    /// Defines a contract for a factory that creates instances of creatures in the game.
    /// </summary>
    public interface ICreatureFactory
    {
        /// <summary>
        /// Creates a new creature with the specified attributes.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">A description of the creature.</param>
        /// <param name="hitpoints">The initial hit points of the creature.</param>
        /// <param name="position">The starting position of the creature in the game world.</param>
        /// <returns>A new instance of <see cref="Creature"/>.</returns>
        Creature Create(string name, string description, int hitpoints, Position position);
    }
}