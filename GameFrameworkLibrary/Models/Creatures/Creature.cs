using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.ItemObjects;
using System.Diagnostics;
using System.ComponentModel;

namespace GameFrameworkLibrary.Models.Creatures
{
    public class Creature : Base.WorldObject, IHasPosition
    {
        public Position Position { get; private set; }
        public int HitPoints { get; private set; }

        private readonly List<AttackItem> _attackItems = new();
        private readonly List<DefenceItem> _defenceItems = new();
        private readonly ILogger _logger;

        public Creature(string name, string? description, int hitpoints, Position startPosition, ILogger logger)
            : base(name, description)
        {
            HitPoints = hitpoints;
            Position = startPosition;
            _logger = logger;
        }
        public override string ToString() =>
            $"(HP: {HitPoints}, Position: {Position})";


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
            //EquipLoot(loot);
            if (container is LootableObject) 
            {
                world.RemoveObject(container);
                _logger.Log(
                    TraceEventType.Information,
                    LogType.Inventory,
                    $"{Name} looted {container.Name} and removed it from the world");
            }
        }
    }
}
