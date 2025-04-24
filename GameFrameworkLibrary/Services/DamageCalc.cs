using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Services
{
    /// <summary>
    /// Provides functionality to calculate damage dealt by an attacker to a defender in the game framework.
    /// Implements the <see cref="IDamageCalc"/> interface.
    /// </summary>
    public class DamageCalc : IDamageCalc
    {
        /// <summary>
        /// Calculates the damage dealt by an attacker to a defender.
        /// The damage is determined by subtracting the defender's total damage reduction from the attacker's total base damage.
        /// </summary>
        /// <param name="attacker">The combat stats of the attacking entity.</param>
        /// <param name="defender">The combat stats of the defending entity.</param>
        /// <returns>The calculated damage value, ensuring it is not less than zero.</returns>
        public int CalculateDamage(ICombatStats attacker, ICombatStats defender)
        {
            int attack = attacker.GetTotalBaseDamage();
            int defence = defender.GetTotalDamageReduction();
            return Math.Max(0, attack - defence);
        }
    }
}