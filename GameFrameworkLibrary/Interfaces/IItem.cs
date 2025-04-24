using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a general item in the game framework.
    /// This interface defines the basic properties that all items must have.
    /// </summary>
    public interface IItem : IName
    {
        /// <summary>
        /// Gets the description of the item.
        /// </summary>
        string? Description { get; }
    }
}