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
    public abstract class Creature : WorldObject, ICreature
    {
        public int HitPoints { get; internal set; }
        public int MaxHitPoints { get; }
        public Position Position { get; internal set; }
        public IInventory Inventory => _inventory;

        protected readonly ICombatService _combatService;
        protected readonly IMovementService _movementService;
        protected readonly IInventory _inventory;
        protected readonly IStatsService _statsService;

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

        /// <inheritdoc />
        public void Attack(string actionKey, ICreature target)
        {
            if (!_namedActions.TryGetValue(actionKey, out var action))
                throw new InvalidOperationException($"No attack registered with key '{actionKey}'");

            AttackTemplate(action, target);
        }

        /// <inheritdoc />
        public void AdjustHitPoints(int delta)
        {
            int oldHitPoints = HitPoints;
            HitPoints = Math.Max(0, Math.Min(HitPoints + delta, MaxHitPoints));

            HealthChanged?.Invoke(this, new HealthChangedEventArgs(oldHitPoints, HitPoints));

            if (oldHitPoints > 0 && HitPoints <= 0)
                OnDeath?.Invoke(this, new DeathEventArgs(this));
        }

        /// <inheritdoc />
        public void Move(int deltaX, int deltaY, World world)
            => Position = _movementService.Move(this, Position, deltaX, deltaY, world);


        /// <inheritdoc />
        public void Loot(ILootable source, World world) => _inventory.Loot(this, source, world);


        ///<inheritdoc/>
        public int GetTotalBaseDamage() => _statsService.GetTotalBaseDamage();

        ///<inheritdoc/>
        public int GetTotalDamageReduction() => _statsService.GetTotalDamageReduction();

        /// <inheritdoc />
        public void EquipWeapon(IDamageSource weapon) => _inventory.EquipAttackItem(weapon);

        /// <inheritdoc />
        public void EquipArmor(IDefenceSource armor) => _inventory.EquipDefenceItem(armor);

        public override string ToString() =>
            $"{Name} at {Position} ({HitPoints}/{MaxHitPoints} HP)";
        #endregion

        #region Templates
        protected void AttackTemplate(IAttackAction action, ICreature target)
        {
            PreAttack(action, target);
            DoAttack(action, target);
            PostAttack(action, target);
        }

        protected virtual void PreAttack(IAttackAction action, ICreature target)
        {
            // e.g. throw if out of range, consume stamina, apply “first‑strike” buff
        }

        protected abstract void DoAttack(IAttackAction action, ICreature target);

        protected virtual void PostAttack(IAttackAction action, ICreature target)
        {
            // e.g. trigger OnHit observers, apply bleed effect, log damage summary
        }
        #endregion
        public event EventHandler<HealthChangedEventArgs>? HealthChanged;

        public event EventHandler<DeathEventArgs>? OnDeath;
    }

}
