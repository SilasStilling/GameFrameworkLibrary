using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.ItemObjects;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;
using GameFrameworkLibrary.Services;
using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Models.Creatures;

namespace GameFrameworkLibrary.Models.Creatures
{
    public class Creature : Base.WorldObject, ICreature
    {
        public Position Position { get; private set; }
        public int HitPoints { get; private set; }

        private readonly ICombatService _combatService;
        private readonly IMovementService _movementService;
        private readonly IInventory _inventory;
        private readonly IStatsService _statsService;


        public Creature(
            string name,
            string description,
            int hitpoints,
            Position startPosition,
            IStatsService statsService,
            ICombatService combatService,
            IMovementService movementService,
            IInventory inventory)
            : base(name, description)
        {
            HitPoints = hitpoints;
            Position = startPosition;
            _statsService = statsService;
            _combatService = combatService;
            _movementService = movementService;
            _inventory = inventory;

        }

        /// <inheritdoc />
        public void Attack(ICreature target) => _combatService.Attack(this, target);

        /// <inheritdoc />
        public void AdjustHitPoints(int delta)
        {
            HitPoints = Math.Max(0, Math.Min(HitPoints + delta, int.MaxValue));
        }

        /// <inheritdoc />
        public void Move(int deltaX, int deltaY, World world)
            => Position = _movementService.Move(Position, deltaX, deltaY, world);

        /// <inheritdoc />
        public IEnumerable<IUsable> GetUsables() => _inventory.GetUsables();

        /// <inheritdoc />
        public void Loot(ILootable source, World world) => _inventory.Loot(this, source, world);

        /// <inheritdoc />
        public void UseItem(IUsable item) => _inventory.UseItem(this, item);

        ///<inheritdoc/>
        public int GetTotalBaseDamage() => _statsService.GetTotalBaseDamage();

        ///<inheritdoc/>
        public int GetTotalDamageReduction() => _statsService.GetTotalBaseDamage();

        ///<inheritdoc/>
        public void EquipAttackItem(IDamageSource attackItem) => _inventory.EquipAttackItem(attackItem);

        ///<inheritdoc/>
        public void EquipDefenceItem(IDefenceSource defenceItem) => _inventory.EquipDefenceItem(defenceItem);

        public override string ToString() =>
            $"name {Name} position {Position} | ({HitPoints} HP)"; 
    }
}
