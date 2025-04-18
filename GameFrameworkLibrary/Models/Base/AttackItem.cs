using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Base
{
    public class AttackItem : ItemBase
    {
        public int HitDamage { get; set; }
        public int Range { get; set; }

        public WeaponType WeaponType { get; }
        public AttackItem(string name, string? description, int hitDamage, int range, WeaponType weaponType) 
            : base(name, description)
        {
            HitDamage = hitDamage;
            Range = range;
            WeaponType = weaponType;
        }
        /// <summary>
        /// Returns a formatted string describing this weapon’s base information,
        /// including its damage, range, and type.
        /// </summary>
        /// <returns>A string representation of the weapon.</returns>
        public override string ToString() =>
            $"{base.ToString()} (Dmg: {HitDamage}, Range: {Range}, Type: {WeaponType})";

    }
}
