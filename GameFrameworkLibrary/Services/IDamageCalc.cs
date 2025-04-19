using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Creatures;

namespace GameFrameworkLibrary.Services
{
    public interface IDamageCalc
    {
        int CalculateDamage(ICombatStats attacker, ICombatStats defender);
    }

}
