using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.ItemObjects;

namespace GameFrameworkLibrary.Models.Factories
{
    public class DefenceItemFactory : IDefenceItemFactory
    {
        public DefenceItem CreateHelmet(string name, string description, int damageReduction, int durability, EquipmentSlots slot = EquipmentSlots.head)
        {
            return new DefenceItem(name, description, damageReduction, durability, slot);
        }


    }
}
