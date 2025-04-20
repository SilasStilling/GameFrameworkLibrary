using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Factories
{
    public interface ICreatureFactory
    {
        Creature Create(string name, string description, int hitpoints, Position position);
    }
}
