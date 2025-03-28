using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary
{
    public class Creature
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
        public void Loot(WorldObject worldObject)
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
