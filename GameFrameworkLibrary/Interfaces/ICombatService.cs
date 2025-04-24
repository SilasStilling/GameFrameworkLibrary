using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Defines the contract for combat-related operations in the game framework.
    /// This service handles attacking, receiving damage, and attacks with specific damage sources.
    /// </summary>
    public interface ICombatService
    {
        /// <summary>
        /// Executes an attack from one creature to another.
        /// </summary>
        /// <param name="attacker">The creature initiating the attack.</param>
        /// <param name="target">The creature being attacked.</param>
        void Attack(ICreature attacker, ICreature target);

        /// <summary>
        /// Applies damage to a creature.
        /// </summary>
        /// <param name="creature">The creature receiving the damage.</param>
        /// <param name="damage">The amount of damage to apply.</param>
        void ReceiveDamage(ICreature creature, int damage);

        /// <summary>
        /// Executes an attack from one creature to another using a specific damage source.
        /// </summary>
        /// <param name="attacker">The creature initiating the attack.</param>
        /// <param name="target">The creature being attacked.</param>
        /// <param name="source">The source of the damage (e.g., weapon, spell).</param>
        void AttackWithSource(ICreature attacker, ICreature target, IDamageSource source);
    }
}