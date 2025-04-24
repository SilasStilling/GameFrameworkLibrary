using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Environment;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents an object that has a position in the game world.
    /// This interface provides a contract for accessing the position of an object.
    /// </summary>
    public interface IHasPosition
    {
        /// <summary>
        /// Gets the position of the object in the game world.
        /// </summary>
        Position Position { get; }
    }
}