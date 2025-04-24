using GameFrameworkLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a weapon in the game framework.
    /// This interface combines the properties of a damage source and an item, 
    /// while adding weapon-specific attributes such as range and type.
    /// </summary>
    public interface IWeapon : IDamageSource, IItem
    {
        /// <summary>
        /// Gets the range of the weapon, representing how far it can attack.
        /// </summary>
        int Range { get; }

        /// <summary>
        /// Gets the type of the weapon (e.g., Knife, Pistol, SniperRifle).
        /// </summary>
        WeaponType WeaponType { get; }
    }
}