using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.ItemObjects;
using System.Diagnostics;
using System.ComponentModel;
using System.Runtime.Intrinsics.Arm;

namespace GameFrameworkLibrary.Models.Creatures
{
    public class Creature : Base.WorldObject, IHasPosition
    {
        public Position Position { get; private set; }
        public int HitPoints { get; private set; }

        private readonly List<AttackItem> _attackItems = new();
        private readonly List<DefenceItem> _defenceItems = new();
        private readonly ILogger _logger;
        private readonly List<IUsable> _usables = new();

        public Creature(string name, string? description, int hitpoints, Position startPosition, ILogger logger)
            : base(name, description)
        {
            HitPoints = hitpoints;
            Position = startPosition;
            _logger = logger;
        }
        public override string ToString() =>
            $"(HP: {HitPoints}, Position: {Position})";


        public void Attack(Creature target)
        {
            int damage = TotalDamage();

            _logger.Log(
                TraceEventType.Information,
                LogType.Combat,
                $"{Name} attacks {target.Name} for {damage} damage."
            );
            target.ReceiveDamage(damage);
        }

        public void ReceiveDamage(int hitdamage)
        {
            int damageReduction = _defenceItems.Sum(i => i.DamageReduction);
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
            EquipLoot(loot);
            if (container is LootableObject)
            {
                world.RemoveObject(container);
                _logger.Log(
                    TraceEventType.Information,
                    LogType.Inventory,
                    $"{Name} looted {container.Name} and removed it from the world");
            }
        }
        private int TotalDamage()
        {
            return _attackItems.Count != 0
            ? _attackItems.Sum(i => i.HitDamage)
            : 10;
        }
        private void AddUsable(IUsable usable)
        {
            _usables.Add(usable);
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"{Name} added usable item to backpack: {((ItemBase)usable).Name}");
        }

        private void EquipDefenceItem(DefenceItem item)
        {
            if (item is not DefenceItem defenceItem)
            {
                _logger.Log(
                    TraceEventType.Warning,
                    LogType.Inventory,
                    $"{Name} tried to equip {item.Name} but it is not a defence item");
                return;
            }
            _defenceItems.Add(defenceItem);
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"{Name} equipped {defenceItem.Name}");
        }
        private void EquipAttackItem(AttackItem item)
        {
            if (item is not AttackItem attackItem)
            {
                _logger.Log(
                    TraceEventType.Warning,
                    LogType.Inventory,
                    $"{Name} tried to equip {item.Name} but it is not an attack item");
                return;
            }
            _attackItems.Add(attackItem);
            _logger.Log(
                TraceEventType.Information,
                LogType.Inventory,
                $"{Name} equipped {attackItem.Name}");
        }
        private void EquipSingleItem(ItemBase item)
        {
            switch (item)
            {
                case AttackItem attackItem:
                    EquipAttackItem(attackItem);
                    break;

                case DefenceItem defenceItem:
                    EquipDefenceItem(defenceItem);
                    break;

                case IUsable usable:
                    AddUsable(usable);
                    break;

                default:
                    _logger.Log(
                        TraceEventType.Warning,
                        LogType.Inventory,
                        $"{Name} ignored item '{item.Name}' – unsupported item type.");
                    break;
            }
        }
        private void EquipLoot(IEnumerable<ItemBase> loot)
        {
            foreach (var item in loot)
            {
                EquipSingleItem(item);
            }
        }
    }
}
