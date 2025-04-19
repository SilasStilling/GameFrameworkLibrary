using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.ItemObjects
{
    /// <summary>
    /// Represents a pistol weapon in the game, inheriting attack-related properties and behaviors from the AttackItem class.
    /// </summary>
    public class Pistol : AttackItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pistol"/> class with the specified properties.
        /// </summary>
        /// <param name="name">The name of the pistol.</param>
        /// <param name="hitdamage">The damage dealt by the pistol.</param>
        /// <param name="range">The range of the pistol's attack.</param>
        /// <param name="description">An optional description of the pistol.</param>
        public Pistol(string name, int hitdamage, int range, string description)
            : base(name, description, hitdamage, range, WeaponType.Pistol)
        {
        }
    }
}
