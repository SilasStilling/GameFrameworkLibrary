using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Models.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Defaults
{
    public class ConfigurableAttackItem : AttackItem
    {
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
