using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.ItemObjects;
using static System.Reflection.Metadata.BlobBuilder;

namespace GameFrameworkLibrary.Models.Factories
{
    public class DefenceItemFactory : IDefenceItemFactory
    {
        public DefenceItem CreateHelmet(string name, string description, int damageReduction, int durability, EquipmentSlots slot)
        {
            return new Helmet(name, description, damageReduction, durability, slot);
        }

        public DefenceItem CreateChest(string name, string description, int damageReduction, int durability, EquipmentSlots slot)
        {
            return new Chest(name, description, damageReduction, durability, slot);
        }

        public DefenceItem CreateLegs(string name, string description, int damageReduction, int durability, EquipmentSlots slot)
        {
            return new Legs(name, description, damageReduction, durability, slot);
        }

        public DefenceItem CreateBoots(string name, string description, int damageReduction, int durability, EquipmentSlots slot)
        {
            return new Boots(name, description, damageReduction, durability, slot);
        }
    }
}