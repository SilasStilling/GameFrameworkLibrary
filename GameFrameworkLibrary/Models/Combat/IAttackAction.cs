using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Combat
{
    public interface IAttackAction
    {
        void Execute(ICreature attacker, ICreature target);
    }
}
