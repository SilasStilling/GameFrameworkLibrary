using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Environment;

namespace GameFrameworkLibrary
{
    public class Creature : Models.Base.WorldObject, IHasPosition
    {
        public Position Position { get; private set; }
        public int HitPoints { get; private set; }

        private readonly List<AttackItem> _attackItems = new();
        private readonly List<DefenceItem> _defenceItems = new();
        private readonly ILogger _logger;

        public Creature(string name, int health, int damage)
        {
            Name = name;
            Health = health;
            Damage = damage;
        }

        public Creature() { }
    {
        public string? Name { get; set; }
        public int Health { get; set; }
        public int Damage { get; set; }

        public override string ToString()
        {
            return Name + " has " + Health + " health and does " + Damage + " damage.";
        }

        public void ReceiveDamage(int damage)
        {
            Health -= damage;
        }
        public void Loot(Models.Base.WorldObject worldObject)
        {
            if (worldObject.Lootable)
            {
                Console.WriteLine("Looting " + worldObject.Name);
            }
            else
            {
                Console.WriteLine("Cannot loot " + worldObject.Name);
            }
        }
    }
}
