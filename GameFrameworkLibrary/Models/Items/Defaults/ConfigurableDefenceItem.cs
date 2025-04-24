using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Models.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Defaults
{
    public class ConfigurableDefenceItem : DefenceItem
    {
        public ConfigurableDefenceItem(
            string name,
            string description,
            int damageReduction,
            int durability,
            EquipmentSlots slot)
            : base(name, description, damageReduction, durability, slot)
        {
        }


    }
}
