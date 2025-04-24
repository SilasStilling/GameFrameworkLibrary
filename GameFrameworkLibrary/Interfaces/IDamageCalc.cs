using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Services;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Defines the contract for calculating damage in the game framework.
    /// This interface is responsible for determining the damage dealt by an attacker to a defender
    /// based on their combat stats.
    /// </summary>
    public interface IDamageCalc
    {
        /// <summary>
        /// Calculates the damage dealt by an attacker to a defender.
        /// </summary>
        /// <param name="attacker">The combat stats of the attacking entity.</param>
        /// <param name="defender">The combat stats of the defending entity.</param>
        /// <returns>The calculated damage value.</returns>
        int CalculateDamage(ICombatStats attacker, ICombatStats defender);
    }
}