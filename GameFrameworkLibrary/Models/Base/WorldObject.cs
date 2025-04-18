using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Base
{
    /// <summary>
    /// Represents a base class for objects within the game world, 
    /// providing common functionality and structure.
    /// </summary>
    public abstract class WorldObject
    {
        public string Name { get; set; }
        public string? Description { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="WorldObject"/> class with the specified name and description.
        /// </summary>
        /// <param name="name">The name of the world object.</param>
        /// <param name="description">The optional description of the world object.</param>
        protected WorldObject(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Returns a string representation of the world object, including its name and description.
        /// </summary>
        /// <returns>A string in the format "Name: Description".</returns>
        public override string ToString()
        {
            return $"{Name}: {Description}";
        }
    }
}