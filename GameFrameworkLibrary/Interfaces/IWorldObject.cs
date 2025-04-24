using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a general object in the game world.
    /// This interface defines the basic properties that all world objects must have.
    /// </summary>
    public interface IWorldObject
    {
        /// <summary>
        /// Gets the name of the world object.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of the world object.
        /// </summary>
        string? Description { get; }
    }
}