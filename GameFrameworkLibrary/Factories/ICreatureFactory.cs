using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Models.Environment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Factories
{
    public interface ICreatureFactory
    {
        Creature Create(string name, string description, int hitpoints, Position position);
    }
}
