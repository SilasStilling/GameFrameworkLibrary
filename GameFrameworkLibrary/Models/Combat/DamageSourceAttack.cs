using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Combat
{
    public class DamageSourceAttack : IAttackAction
    {
        private readonly IDamageSource _source;
        private readonly ICombatService _combat;

        public DamageSourceAttack(IDamageSource source, ICombatService combat)
        {
            _source = source;
            _combat = combat;
        }

        public void Execute(ICreature attacker, ICreature target)
        {
            _combat.AttackWithSource(attacker, target, _source);
        }
    }
}
