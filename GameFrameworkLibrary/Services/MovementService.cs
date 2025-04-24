using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Services
{
    /// <summary>
    /// Provides movement logic for creatures in the game world.
    /// Ensures creatures move within the bounds of the game world and logs movement actions.
    /// Implements the <see cref="IMovementService"/> interface.
    /// </summary>
    public class MovementService : IMovementService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="MovementService"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging movement actions.</param>
        public MovementService(ILogger logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Moves a creature to a new position based on the specified deltas.
        /// Ensures the new position is clamped within the bounds of the game world.
        /// </summary>
        /// <param name="mover">The creature that is moving.</param>
        /// <param name="current">The current position of the creature.</param>
        /// <param name="dx">The change in the X-coordinate.</param>
        /// <param name="dy">The change in the Y-coordinate.</param>
        /// <param name="world">The game world where the movement occurs.</param>
        /// <returns>The new position of the creature after movement.</returns>
        public Position Move(ICreature mover, Position current, int dx, int dy, World world)
        {
            // Capture origin and intended positions
            var origin = current;
            var intended = new Position(current.X + dx, current.Y + dy);

            // Clamp into world bounds
            var clamped = new Position(
                Math.Clamp(intended.X, 0, world.WorldWidth),
                Math.Clamp(intended.Y, 0, world.WorldHeight)
            );

            // Determine log level based on whether clamping occurred
            var level = (intended != clamped)
                ? TraceEventType.Warning
                : TraceEventType.Information;

            // Log actual movement
            _logger.Log(
              level,
              LogType.World,
              $"[{mover.Name}] moved from {origin} to {clamped}" +
                (level == TraceEventType.Warning ? " (out‐of‐bounds clamped)" : "")
            );

            return clamped;
        }
    }
}