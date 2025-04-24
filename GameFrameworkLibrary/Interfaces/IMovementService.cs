using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    public interface IMovementService
    {
        Position Move(Position current, int dx, int dy, World world);
    }
}
