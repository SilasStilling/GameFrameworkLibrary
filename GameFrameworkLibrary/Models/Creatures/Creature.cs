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
    public class Creature : Base.WorldObject, IHasPosition, ICombatStats
    {
        public Position Position { get; private set; }
        public int HitPoints { get; private set; }

        private readonly IDamageCalc _damageCalc;
        private readonly IInventory _inventory;

        private readonly ILogger _logger;

        public Creature(string name, string? description, int hitpoints, Position startPosition, IInventory inventory, ILogger logger, IDamageCalc damageCalc) 
            : base(name, description)
        {
            HitPoints = hitpoints;
            Position = startPosition;
            _inventory = inventory;
            _damageCalc = damageCalc;
            _logger = logger;
        }
        public override string ToString() =>
            $"(HP: {HitPoints}, Position: {Position})";

        public IEnumerable<IUsable> GetUsables() => _inventory.GetUsables();


        public void Attack(Creature target)
        {
            int damage = _damageCalc.CalculateDamage(this, target);

            _logger.Log(
                TraceEventType.Information,
                LogType.Combat,
                $"{Name} attacks {target.Name} for {damage} damage."
            );
            target.ReceiveDamage(damage);
        }

        public void ReceiveDamage(int hitdamage)
        {
            int damageReduction = GetTotalDamageReduction();
            int trueDamage = Math.Max(0, hitdamage - damageReduction);
            HitPoints -= trueDamage;

            _logger.Log(
                TraceEventType.Information,
                LogType.Combat,
                $"{Name} received {trueDamage} damage. Remaining HP: {HitPoints}");

            if (HitPoints <= 0)
            {
                _logger.Log(
                    TraceEventType.Critical,
                    LogType.Combat,
                    $"{Name} has been defeated.");
            }
        }
        public void Move(int dx, int dy, World world)
        {
            var from = Position;
            Position = Position with
            {
                X = Math.Clamp(Position.X + dx, 0, world.WorldWidth),
                Y = Math.Clamp(Position.Y + dy, 0, world.WorldHeight)
            };
            _logger.Log(
                TraceEventType.Information,
                LogType.World,
                $"{Name} attempted move from {from} to ({from.X + dx},{from.Y + dy}), clamped to {Position}");
        }


        public void UseItem(WorldObject item)
        {
            if (item is IUsable usable)
            {
                usable.UseOn(this);
            }
            else
            {
                _logger.Log(
                    TraceEventType.Warning,
                    LogType.Inventory,
                    $"{Name} tried to use {item.Name} but it is not usable");
            }
        }


        public void Loot(ILootable source, World world)
        {
            if (source is not EnvironmentObject container || source is not (ItemObjects.Container or LootableObject))
            {
                _logger.Log(
                    TraceEventType.Warning,
                    LogType.Inventory,
                   $"{Name} cannot loot this source");
                return;
            }
            if (!container.IsLootable)
            {
                _logger.Log(
                    TraceEventType.Warning,
                    LogType.Inventory,
                    $"{Name} tried to loot {container.Name} but it is not lootable ");
                return;
            }

            var loot = source.GetLoot();
            _inventory.ProcessLoot(loot);

            if (container is LootableObject) 
            {
            world.RemoveObject(container);
                _logger.Log(
                    TraceEventType.Information,
                    LogType.Inventory,
                    $"{Name} looted {container.Name} and removed it from the world");
            }
        }

        ///<inheritdoc/>
        public int GetTotalBaseDamage() => _inventory.GetTotalBaseDamage();

        ///<inheritdoc/>
        public int GetTotalDamageReduction() => _inventory.GetTotalDamageReduction();

        public void EquipAttackItem(IDamageSource attackItem) => _inventory.EquipAttackItem(attackItem);

        public void EquipDefenceItem(IDefenceSource defenceItem) => _inventory.EquipDefenceItem(defenceItem);
    }
}
