using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.ItemObjects;

namespace GameFrameworkLibrary.Models.Factories
{
    internal class AttackItemFactory : IAttackItemFactory
    {

        public Pistol CreatePistol(string name, int hitdamage, int range, string description)
        {
            return new Pistol(name, hitdamage, range, description);
        }

        public Rifle CreateRifle(string name, int hitdamage, int range, string description)
        {
            return new Rifle(name, hitdamage, range, description);
        }
    }
}

