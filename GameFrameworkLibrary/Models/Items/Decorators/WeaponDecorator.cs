using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Decorators
{
    public abstract class WeaponDecorator : DamageSourceDecorator, IWeapon
    {
        protected readonly IWeapon _innerWeapon;

        public int Range => _innerWeapon.Range;

        public WeaponType WeaponType => _innerWeapon.WeaponType;


        public WeaponDecorator(IWeapon inner) : base(inner)
        {
            _innerWeapon = inner;
        }


    }
}
