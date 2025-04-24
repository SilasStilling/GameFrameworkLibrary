using GameFrameworkLibrary.Models.Combat;
using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a creature in the game world with combat capabilities, position, and health management.
    /// This interface defines the core behavior and properties of a creature.
    /// </summary>
    public interface ICreature : ICombatStats, IHasPosition
    {
        /// <summary>
        /// Gets the name of the creature.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the current hit points of the creature.
        /// </summary>
        int HitPoints { get; }

        /// <summary>
        /// Event triggered when the creature dies.
        /// </summary>
        event EventHandler<DeathEventArgs>? OnDeath;

        /// <summary>
        /// Event triggered when the creature's health changes.
        /// </summary>
        event EventHandler<HealthChangedEventArgs>? HealthChanged;

        /// <summary>
        /// Moves the creature by the specified delta values within the game world.
        /// </summary>
        /// <param name="deltaX">The change in the X-coordinate.</param>
        /// <param name="deltaY">The change in the Y-coordinate.</param>
        /// <param name="world">The game world where the movement occurs.</param>
        void Move(int deltaX, int deltaY, World world);

        /// <summary>
        /// Registers an attack action with a unique key for the creature.
        /// </summary>
        /// <param name="key">The unique key for the attack action.</param>
        /// <param name="action">The attack action to register.</param>
        void RegisterAttackAction(string key, IAttackAction action);

        /// <summary>
        /// Executes an attack using a registered attack action.
        /// </summary>
        /// <param name="actionKey">The key of the attack action to execute.</param>
        /// <param name="target">The target creature to attack.</param>
        void Attack(string actionKey, ICreature target);

        /// <summary>
        /// Adjusts the creature's hit points by the specified delta value.
        /// </summary>
        /// <param name="delta">The amount to adjust the hit points by (positive or negative).</param>
        void AdjustHitPoints(int delta);
    }
}