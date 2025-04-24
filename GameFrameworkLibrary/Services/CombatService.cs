using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Logging;
using System.Diagnostics;
using GameFrameworkLibrary.Models.Base;

namespace GameFrameworkLibrary.Services
{
    /// <summary>
    /// Provides combat-related operations in the game framework.
    /// Handles attacking, receiving damage, and attacks with specific damage sources.
    /// </summary>
    public class CombatService : ICombatService
    {
        private readonly IDamageCalc _damageCalc;
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="CombatService"/> class.
        /// </summary>
        /// <param name="damageCalc">The damage calculation service.</param>
        /// <param name="logger">The logger instance for logging combat events.</param>
        public CombatService(IDamageCalc damageCalc, ILogger logger)
        {
            _damageCalc = damageCalc;
            _logger = logger;
        }

        /// <summary>
        /// Executes an attack from one creature to another.
        /// Calculates damage using the damage calculator and applies it to the target.
        /// </summary>
        /// <param name="attacker">The creature initiating the attack.</param>
        /// <param name="target">The creature being attacked.</param>
        public void Attack(ICreature attacker, ICreature target)
        {
            int damage = _damageCalc.CalculateDamage(attacker, target);
            _logger.Log(
                TraceEventType.Information,
                LogType.Combat,
                $"{attacker.Name} attacks {target.Name} for {damage} damage.");
            ReceiveDamage(target, damage);
        }

        /// <summary>
        /// Applies damage to a creature, considering its damage reduction.
        /// Logs the damage received and the remaining health of the creature.
        /// </summary>
        /// <param name="creature">The creature receiving the damage.</param>
        /// <param name="hitdamage">The raw damage to apply.</param>
        public void ReceiveDamage(ICreature creature, int hitdamage)
        {
            int reduction = creature.GetTotalDamageReduction();
            int actual = Math.Max(0, hitdamage - reduction);

            creature.AdjustHitPoints(-actual);

            _logger.Log(
                TraceEventType.Information,
                LogType.Combat,
                $"{creature.Name} receives {actual} damage after reduction. Remaining HP: {creature.HitPoints}.");

            if (creature.HitPoints <= 0)
            {
                _logger.Log(
                    TraceEventType.Information,
                    LogType.Combat,
                    $"{creature.Name} has been defeated.");
            }
        }

        /// <summary>
        /// Executes an attack from one creature to another using a specific damage source.
        /// Applies the base damage of the source directly to the target.
        /// </summary>
        /// <param name="attacker">The creature initiating the attack.</param>
        /// <param name="target">The creature being attacked.</param>
        /// <param name="source">The source of the damage (e.g., weapon, spell).</param>
        public void AttackWithSource(ICreature attacker, ICreature target, IDamageSource source)
        {
            int damage = source.BaseDamage;

            _logger.Log(TraceEventType.Information, LogType.Combat,
                $"{attacker.Name} uses {source} against {target.Name}, dealing {damage} damage");

            ReceiveDamage(target, damage);
        }
    }
}