using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Factories
{
    public interface IDefenceItemFactory
    {
        DefenceItem CreateHelmet(string name, string description, int damageReduction, int durability, EquipmentSlots slot);

        DefenceItem CreateChest(string name, string description, int damageReduction, int durability, EquipmentSlots slot);

        DefenceItem CreateLegs(string name, string description, int damageReduction, int durability, EquipmentSlots slot);

        DefenceItem CreateBoots(string name, string description, int damageReduction, int durability, EquipmentSlots slot);
    }
}
