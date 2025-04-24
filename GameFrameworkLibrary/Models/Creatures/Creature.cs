using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.ItemObjects;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using GameFrameworkLibrary.Services;
using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Models.Combat;

namespace GameFrameworkLibrary.Models.Creatures
{
    /// <summary>
    /// Represents a base class for creatures in the game world.
    /// Provides core functionality for combat, movement, inventory management, and health tracking.
    /// </summary>
    public abstract class Creature : WorldObject, ICreature
    {
        /// <summary>
        /// Gets or sets the current hit points of the creature.
        /// </summary>
        public int HitPoints { get; internal set; }

        /// <summary>
        /// Gets the maximum hit points of the creature.
        /// </summary>
        public int MaxHitPoints { get; }

        /// <summary>
        /// Gets or sets the current position of the creature in the game world.
        /// </summary>
        public Position Position { get; internal set; }

        /// <summary>
        /// Gets the inventory of the creature.
        /// </summary>
        public IInventory Inventory => _inventory;

        // Services and dependencies
        protected readonly ICombatService _combatService;
        protected readonly IMovementService _movementService;
        protected readonly IInventory _inventory;
        protected readonly IStatsService _statsService;

        // Dictionary to store registered attack actions
        protected readonly Dictionary<string, IAttackAction> _namedActions = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="Creature"/> class.
        /// </summary>
        /// <param name="name">The name of the creature.</param>
        /// <param name="description">An optional description of the creature.</param>
        /// <param name="hitpoints">The starting and maximum hit points of the creature.</param>
        /// <param name="startPosition">The initial position of the creature in the world.</param>
        /// <param name="statsService">The stats service to calculate damage and reduction.</param>
        /// <param name="combatService">The combat service to handle attack logic.</param>
        /// <param name="movementService">The movement service to handle positioning.</param>
        /// <param name="inventoryService">The inventory service to handle items.</param>
        public Creature(
            string name,
            string description,
            int hitpoints,
            Position startPosition,
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventory inventoryService)
                : base(name, description)
        {
            HitPoints = hitpoints;
            MaxHitPoints = hitpoints;
            Position = startPosition;
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventory = inventoryService;
        }

        #region Public Methods

        /// <summary>
        /// Registers an attack action under a unique key.
        /// CompositeAttackAction can itself wrap multiple actions.
        /// </summary>
        public void RegisterAttackAction(string key, IAttackAction action)
        {
            if (string.IsNullOrWhiteSpace(key))
                throw new ArgumentException("Action key must be non-empty", nameof(key));
            _namedActions[key] = action;
        }

        /// <summary>
        /// Executes an attack using a registered attack action.
        /// </summary>
        /// <param name="actionKey">The key of the attack action to execute.</param>
        /// <param name="target">The target creature to attack.</param>
        public void Attack(string actionKey, ICreature target)
        {
            if (!_namedActions.TryGetValue(actionKey, out var action))
                throw new InvalidOperationException($"No attack registered with key '{actionKey}'");

            AttackTemplate(action, target);
        }

        /// <summary>
        /// Adjusts the creature's hit points by the specified delta value.
        /// </summary>
        /// <param name="delta">The amount to adjust the hit points by (positive or negative).</param>
        public void AdjustHitPoints(int delta)
        {
            int oldHitPoints = HitPoints;
            HitPoints = Math.Max(0, Math.Min(HitPoints + delta, MaxHitPoints));

            HealthChanged?.Invoke(this, new HealthChangedEventArgs(oldHitPoints, HitPoints));

            if (oldHitPoints > 0 && HitPoints <= 0)
                OnDeath?.Invoke(this, new DeathEventArgs(this));
        }

        /// <summary>
        /// Moves the creature by the specified delta values within the game world.
        /// </summary>
        /// <param name="deltaX">The change in the X-coordinate.</param>
        /// <param name="deltaY">The change in the Y-coordinate.</param>
        /// <param name="world">The game world where the movement occurs.</param>
        public void Move(int deltaX, int deltaY, World world)
            => Position = _movementService.Move(this, Position, deltaX, deltaY, world);

        /// <summary>
        /// Loots items from a source and adds them to the creature's inventory.
        /// </summary>
        /// <param name="source">The lootable source.</param>
        /// <param name="world">The game world where the looting occurs.</param>
        public void Loot(ILootable source, World world) => _inventory.Loot(this, source, world);

        /// <summary>
        /// Gets the total base damage of the creature from equipped attack items.
        /// </summary>
        public int GetTotalBaseDamage() => _statsService.GetTotalBaseDamage();

        /// <summary>
        /// Gets the total damage reduction of the creature from equipped defense items.
        /// </summary>
        public int GetTotalDamageReduction() => _statsService.GetTotalDamageReduction();

        /// <summary>
        /// Equips a weapon for the creature.
        /// </summary>
        /// <param name="weapon">The weapon to equip.</param>
        public void EquipWeapon(IDamageSource weapon) => _inventory.EquipAttackItem(weapon);

        /// <summary>
        /// Equips armor for the creature.
        /// </summary>
        /// <param name="armor">The armor to equip.</param>
        public void EquipArmor(IDefenceSource armor) => _inventory.EquipDefenceItem(armor);

        /// <summary>
        /// Returns a string representation of the creature, including its name, position, and health.
        /// </summary>
        public override string ToString() =>
            $"{Name} at {Position} ({HitPoints}/{MaxHitPoints} HP)";

        #endregion

        #region Templates

        /// <summary>
        /// Template method for executing an attack action.
        /// </summary>
        protected void AttackTemplate(IAttackAction action, ICreature target)
        {
            PreAttack(action, target);
            DoAttack(action, target);
            PostAttack(action, target);
        }

        /// <summary>
        /// Hook for pre-attack logic (e.g., range checks, buffs).
        /// </summary>
        protected virtual void PreAttack(IAttackAction action, ICreature target) { }

        /// <summary>
        /// Abstract method for performing the actual attack logic.
        /// Must be implemented by derived classes.
        /// </summary>
        protected abstract void DoAttack(IAttackAction action, ICreature target);

        /// <summary>
        /// Hook for post-attack logic (e.g., applying effects, logging).
        /// </summary>
        protected virtual void PostAttack(IAttackAction action, ICreature target) { }

        #endregion

        /// <summary>
        /// Event triggered when the creature's health changes.
        /// </summary>
        public event EventHandler<HealthChangedEventArgs>? HealthChanged;

        /// <summary>
        /// Event triggered when the creature dies.
        /// </summary>
        public event EventHandler<DeathEventArgs>? OnDeath;
    }
}