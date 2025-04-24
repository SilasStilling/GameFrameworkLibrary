using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Environment
{
    /// <summary>
    /// Represents an immutable 2D position with X and Y coordinates.
    /// </summary>
    public readonly record struct Position(int X, int Y)
    {
        /// <summary>
        /// Returns a string representation of the position in the format "(X, Y)".
        /// </summary>
        public override string ToString() => $"({X}, {Y})";
    }
}
