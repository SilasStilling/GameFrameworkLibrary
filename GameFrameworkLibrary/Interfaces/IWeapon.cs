using GameFrameworkLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    public interface IWeapon : IDamageSource, IItem
    {
        int Range { get; }
        WeaponType WeaponType { get; }
    }
}
