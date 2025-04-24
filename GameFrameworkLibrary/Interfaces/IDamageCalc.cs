using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Services;

namespace GameFrameworkLibrary.Interfaces
{
    public interface IDamageCalc
    {
        int CalculateDamage(ICombatStats attacker, ICombatStats defender);
    }

}
