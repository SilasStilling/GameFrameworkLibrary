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
    /// <summary>
    /// Represents a default implementation of the <see cref="Creature"/> class.
    /// Provides basic functionality for attack actions and inherits core creature behavior.
    /// </summary>
    public class DefaultCreature : Creature
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DefaultCreature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">A description of the creature.</param>
        /// <param name="hitpoints">The initial and maximum hit points of the creature.</param>
        /// <param name="startPosition">The starting position of the creature in the game world.</param>
        /// <param name="statsService">The stats service to calculate damage and reduction.</param>
        /// <param name="combatService">The combat service to handle attack logic.</param>
        /// <param name="movementService">The movement service to handle positioning.</param>
        /// <param name="inventory">The inventory service to manage items.</param>
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

        /// <summary>
        /// Adds a new attack action to the creature's list of registered actions.
        /// </summary>
        /// <param name="key">The unique key for the attack action.</param>
        /// <param name="action">The attack action to register.</param>
        public void AddAttackAction(string key, IAttackAction action)
        {
            RegisterAttackAction(key, action);
        }

        /// <summary>
        /// Executes any pre-attack logic before the attack action is performed.
        /// This implementation is empty but can be overridden for custom behavior.
        /// </summary>
        /// <param name="action">The attack action being executed.</param>
        /// <param name="target">The target creature of the attack.</param>
        protected override void PreAttack(IAttackAction action, ICreature target)
        {
            // No pre-attack logic in the default implementation.
        }

        /// <summary>
        /// Executes the main attack logic.
        /// </summary>
        /// <param name="action">The attack action being executed.</param>
        /// <param name="target">The target creature of the attack.</param>
        protected override void DoAttack(IAttackAction action, ICreature target)
        {
            action.Execute(this, target);
        }

        /// <summary>
        /// Executes any post-attack logic after the attack action is performed.
        /// This implementation is empty but can be overridden for custom behavior.
        /// </summary>
        /// <param name="action">The attack action that was executed.</param>
        /// <param name="target">The target creature of the attack.</param>
        protected override void PostAttack(IAttackAction action, ICreature target)
        {
            // No post-attack logic in the default implementation.
        }
    }
}