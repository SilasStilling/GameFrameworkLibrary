using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Combat
{
    /// <summary>
    /// Defines a contract for attack actions in the game framework.
    /// This interface provides a method to execute an attack from one creature to another.
    /// </summary>
    public interface IAttackAction
    {
        /// <summary>
        /// Executes the attack action.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="target">The target creature being attacked.</param>
        void Execute(ICreature attacker, ICreature target);
    }
}