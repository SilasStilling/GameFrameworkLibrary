using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Base
{
    public abstract class AttackItem : ItemBase, IWeapon
    {
        public int BaseDamage { get; }
        public int Range { get; }
        public WeaponType WeaponType { get; }

        public AttackItem(string name, string description, int hitdamage, int range, WeaponType weaponType) 
            : base(name, description)
        {
            BaseDamage = hitdamage;
            Range = range;
            WeaponType = weaponType;
        }
        /// <summary>
        /// Returns a formatted string describing this weapon’s base information,
        /// including its damage, range, and type.
        /// </summary>
        /// <returns>A string representation of the weapon.</returns>
        public override string ToString() =>
            $"{base.ToString()} (Dmg: {BaseDamage}, Range: {Range}, Type: {WeaponType})";

    }
}
