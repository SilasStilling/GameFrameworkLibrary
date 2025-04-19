using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.ItemObjects
{
    public class Helmet : DefenceItem
    {
        public Helmet(string name, string? description, int damageReduction, int durability, EquipmentSlots slot)
            : base(name, description, damageReduction, durability, slot)
        {
        }
    }
}
