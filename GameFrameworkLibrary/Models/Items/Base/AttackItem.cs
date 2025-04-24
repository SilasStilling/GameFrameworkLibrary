using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Base
{
    /// <summary>
    /// Represents a base class for all attack-oriented items (weapons) in the game.
    /// Provides common properties and functionality for weapons, such as damage, range, and type.
    /// </summary>
    public abstract class AttackItem : ItemBase, IWeapon
    {
        /// <summary>
        /// Gets the base damage dealt by the weapon.
        /// </summary>
        public int BaseDamage { get; }

        /// <summary>
        /// Gets the range of the weapon, representing how far it can attack.
        /// </summary>
        public int Range { get; }

        /// <summary>
        /// Gets the type of the weapon (e.g., Knife, Pistol, SniperRifle).
        /// </summary>
        public WeaponType WeaponType { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AttackItem"/> class.
        /// </summary>
        /// <param name="name">The name of the weapon.</param>
        /// <param name="description">A description of the weapon.</param>
        /// <param name="hitdamage">The base damage dealt by the weapon.</param>
        /// <param name="range">The range of the weapon.</param>
        /// <param name="weaponType">The type of the weapon (e.g., Knife, Pistol).</param>
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