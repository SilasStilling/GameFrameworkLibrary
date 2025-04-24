using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Decorators
{
    /// <summary>
    /// A decorator for weapons that applies a temporary damage buff for a limited number of uses.
    /// </summary>
    public class TimedWeaponDecorator : WeaponDecorator
    {
        private int _remainingUses;
        private readonly Func<int, int> _modifier;

        /// <summary>
        /// Initializes a new instance of the <see cref="TimedWeaponDecorator"/> class.
        /// </summary>
        /// <param name="inner">The underlying weapon to decorate.</param>
        /// <param name="modifier">A function that modifies the weapon's base damage.</param>
        /// <param name="uses">The number of attacks this buff applies to before expiring.</param>
        public TimedWeaponDecorator(
            IWeapon inner,
            Func<int, int> modifier,
            int uses)
                : base(inner)
        {
            _modifier = modifier ?? throw new ArgumentNullException(nameof(modifier));
            _remainingUses = uses;
        }

        /// <summary>
        /// Gets the base damage of the weapon. Applies the damage modifier if the buff is still active.
        /// </summary>
        public override int BaseDamage
        {
            get
            {
                if (_remainingUses > 0)
                {
                    _remainingUses--;
                    return _modifier(_innerWeapon.BaseDamage);
                }

                return _innerWeapon.BaseDamage;
            }
        }
    }
}