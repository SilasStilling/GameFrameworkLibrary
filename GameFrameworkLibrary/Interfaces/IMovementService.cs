using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a service for handling movement logic in the game framework.
    /// This interface defines a method to calculate the new position of a creature after movement.
    /// </summary>
    public interface IMovementService
    {
        /// <summary>
        /// Moves a creature to a new position based on the specified deltas.
        /// </summary>
        /// <param name="mover">The creature that is moving.</param>
        /// <param name="current">The current position of the creature.</param>
        /// <param name="dx">The change in the X-coordinate.</param>
        /// <param name="dy">The change in the Y-coordinate.</param>
        /// <param name="world">The game world where the movement occurs.</param>
        /// <returns>The new position of the creature after movement.</returns>
        Position Move(ICreature mover, Position current, int dx, int dy, World world);
    }
}