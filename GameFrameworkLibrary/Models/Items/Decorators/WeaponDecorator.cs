using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Decorators
{
    /// <summary>
    /// A base decorator class for enhancing or modifying the behavior of weapons.
    /// Implements the <see cref="IWeapon"/> interface and extends <see cref="DamageSourceDecorator"/>.
    /// </summary>
    public abstract class WeaponDecorator : DamageSourceDecorator, IWeapon
    {
        /// <summary>
        /// The inner weapon being decorated.
        /// </summary>
        protected readonly IWeapon _innerWeapon;

        /// <summary>
        /// Gets the range of the weapon, delegating to the inner weapon.
        /// </summary>
        public int Range => _innerWeapon.Range;

        /// <summary>
        /// Gets the type of the weapon (e.g., Knife, Pistol, SniperRifle), delegating to the inner weapon.
        /// </summary>
        public WeaponType WeaponType => _innerWeapon.WeaponType;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponDecorator"/> class.
        /// </summary>
        /// <param name="inner">The inner weapon to decorate.</param>
        public WeaponDecorator(IWeapon inner) : base(inner)
        {
            _innerWeapon = inner ?? throw new ArgumentNullException(nameof(inner));
        }
    }
}