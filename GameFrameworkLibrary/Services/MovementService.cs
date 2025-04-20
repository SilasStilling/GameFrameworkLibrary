using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Services
{
    public class MovementService : IMovementService
    {
        public Position Move(Position current, int dx, int dy, World world)
        {
            int newX = Math.Clamp(current.X + dx, 0, world.WorldWidth);
            int newY = Math.Clamp(current.Y + dy, 0, world.WorldHeight);
            return current with { X = newX, Y = newY };
        }
    }
}
