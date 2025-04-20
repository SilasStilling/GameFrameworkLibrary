using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    public interface IWorldObject
    {
        string Name { get; }

        string? Description { get; }
    }
}
