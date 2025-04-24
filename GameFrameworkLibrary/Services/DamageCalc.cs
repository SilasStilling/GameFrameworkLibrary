using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Services
{
    public class DamageCalc : IDamageCalc
    {
        public int CalculateDamage(ICombatStats attacker, ICombatStats defender)
        {
            int attack = attacker.GetTotalBaseDamage();
            int defence = defender.GetTotalDamageReduction();
            return Math.Max(0, attack - defence);
        }
    }
}