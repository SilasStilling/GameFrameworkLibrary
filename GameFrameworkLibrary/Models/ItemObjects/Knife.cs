using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.ItemObjects
{
    /// <summary>
    /// Represents a knife weapon in the game, inheriting attack-related properties and behaviors from the AttackItem class.
    /// </summary>
    public class Knife : AttackItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Knife"/> class with the specified properties.
        /// </summary>
        /// <param name="name">The name of the knife.</param>
        /// <param name="hitdamage">The damage dealt by the knife.</param>
        /// <param name="range">The range of the knife's attack.</param>
        /// <param name="description">An optional description of the knife.</param>
        public Knife(string name, int hitdamage, int range, string? description)
            : base(name, description, hitdamage, range, WeaponType.Knife)
        {
        }
    }
}
