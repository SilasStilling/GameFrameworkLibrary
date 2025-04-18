using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Base;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Interface for objects that have a position in the game world.
    /// </summary>
    public interface IHasPosition
    {
        Position Position { get; }
    }
}
