using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    public interface ICombatService
    {
        void Attack(ICreature attacker, ICreature target);

        void ReceiveDamage(ICreature creature, int damage);

    }
}
