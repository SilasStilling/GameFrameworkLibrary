using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents an object with a name in the game framework.
    /// This interface provides a contract for any object that requires a unique or identifiable name.
    /// </summary>
    public interface IName
    {
        /// <summary>
        /// Gets the name of the object.
        /// </summary>
        string Name { get; }
    }
}