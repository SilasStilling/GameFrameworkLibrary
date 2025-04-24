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
    public class MovementService : IMovementService
    {
        private readonly ILogger _logger;

        public MovementService(ILogger logger)
        {
            _logger = logger;
        }

        ///<inheritdoc/>
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
