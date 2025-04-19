using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.ItemObjects
{
    public class Rifle : AttackItem
    {
        public Rifle(string name, int hitdamage, int range, string description)
    : base(name, description, hitdamage, range, WeaponType.AssaultRifle)
        {
        }
    }
}
