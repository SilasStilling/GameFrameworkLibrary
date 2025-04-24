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
    public class CombatService : ICombatService
    {
        private readonly IDamageCalc _damageCalc;
        private readonly ILogger _logger;


        public CombatService(IDamageCalc damageCalc, ILogger logger)
        {
            _damageCalc = damageCalc;
            _logger = logger;
        }

        public void Attack(ICreature attacker, ICreature target) 
        {
         int damage = _damageCalc.CalculateDamage(attacker, target);
            _logger.Log(
                TraceEventType.Information,
                LogType.Combat,
                $"{attacker.Name} attacks {target.Name} for {damage} damage.");
            ReceiveDamage(target, damage);
        }

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

        /// <inheritdoc />
        public void AttackWithSource(ICreature attacker, ICreature target, IDamageSource source)
        {
            int damage = source.BaseDamage;

            _logger.Log(TraceEventType.Information, LogType.Combat,
                $"{attacker.Name} uses {source} against {target.Name}, dealing {damage} damage");

            ReceiveDamage(target, damage);
        }

    }
}
