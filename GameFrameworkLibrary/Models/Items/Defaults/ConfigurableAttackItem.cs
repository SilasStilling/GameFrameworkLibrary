using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Models.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Defaults
{
    /// <summary>
    /// Represents a configurable attack item in the game.
    /// This class provides a concrete implementation of the <see cref="AttackItem"/> base class.
    /// </summary>
    public class ConfigurableAttackItem : AttackItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableAttackItem"/> class.
        /// </summary>
        /// <param name="name">The name of the attack item.</param>
        /// <param name="description">A description of the attack item.</param>
        /// <param name="hitdamage">The base damage dealt by the attack item.</param>
        /// <param name="range">The range of the attack item.</param>
        /// <param name="weaponType">The type of the weapon (e.g., Knife, Pistol).</param>
        public ConfigurableAttackItem(
            string name,
            string description,
            int hitdamage,
            int range,
            WeaponType weaponType)
            : base(name, description, hitdamage, range, weaponType)
        {
        }
    }
}