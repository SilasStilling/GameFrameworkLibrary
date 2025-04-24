using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Combat;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Creatures
{
    public class DefaultCreature : Creature
    {
        public DefaultCreature(
            string name,
            string description,
            int hitpoints,
        Position startPosition,
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventory inventory)
                : base(name, description, hitpoints, startPosition, statsService, combatService, movementService, inventory)
        {
        }

        public void AddAttackAction(string key, IAttackAction action)
        {
            RegisterAttackAction(key, action);
        }

        protected override void PreAttack(IAttackAction action, ICreature target)
        {
  
        }

        protected override void DoAttack(IAttackAction action, ICreature target)
        {
            action.Execute(this, target);
        }

        protected override void PostAttack(IAttackAction action, ICreature target)
        {
        }
    }
}
