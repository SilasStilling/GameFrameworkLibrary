using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Combat
{
    /// <summary>
    /// Represents an attack action that uses a specific damage source.
    /// This class delegates the attack logic to the <see cref="ICombatService"/>.
    /// </summary>
    public class DamageSourceAttack : IAttackAction
    {
        private readonly IDamageSource _source;
        private readonly ICombatService _combat;

        /// <summary>
        /// Initializes a new instance of the <see cref="DamageSourceAttack"/> class.
        /// </summary>
        /// <param name="source">The damage source used for the attack (e.g., weapon, spell).</param>
        /// <param name="combat">The combat service responsible for executing the attack.</param>
        public DamageSourceAttack(IDamageSource source, ICombatService combat)
        {
            _source = source;
            _combat = combat;
        }

        /// <summary>
        /// Executes the attack using the specified damage source.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="target">The target creature being attacked.</param>
        public void Execute(ICreature attacker, ICreature target)
        {
            _combat.AttackWithSource(attacker, target, _source);
        }
    }
}