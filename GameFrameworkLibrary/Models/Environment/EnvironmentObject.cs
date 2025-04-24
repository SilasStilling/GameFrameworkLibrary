using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Environment
{
    /// <summary>
    /// Represents an object in the game world with a position, lootable status, and removable status.
    /// Inherits common properties from the base WorldObject class.
    /// </summary>
    public class EnvironmentObject : WorldObject, IHasPosition
    {
        /// <summary>
        /// Gets or sets the position of the object in the game world.
        /// </summary>
        public Position Position { get; internal set; }
        /// <summary>
        /// Indicates whether the object can be looted.
        /// </summary>
        public bool IsLootable { get; internal set; }
        /// <summary>
        /// Indicates whether the object can be removed from the game world.
        /// </summary>
        public bool IsRemovable { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnvironmentObject"/> class with the specified properties.
        /// </summary>
        /// <param name="name">The name of the object.</param>
        /// <param name="description">The optional description of the object.</param>
        /// <param name="position">The position of the object in the game world.</param>
        /// <param name="isLootable">Indicates if the object is lootable (default is false).</param>
        /// <param name="isRemovable">Indicates if the object is removable (default is false).</param>
        public EnvironmentObject(string name, string? description, Position position, bool isLootable = false, bool isRemovable = false)
    : base(name, description)
        {
            Position = position;
            IsLootable = isLootable;
            IsRemovable = isRemovable;
        }

        /// <summary>
        /// Returns a string representation of the object, including its position, lootable status, and removable status.
        /// </summary>
        public override string ToString()
        {
            return $"{base.ToString()} (Position: {Position}, Lootable: {IsLootable}, Removable: {IsRemovable})";
        }
    }
}
