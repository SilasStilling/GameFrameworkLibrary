using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Base
{
    public abstract class DefenceItem : ItemBase, IDefenceSource
    {
        public int DamageReduction { get; }
        public int Durability { get; }
        public EquipmentSlots Slot { get; }

        public DefenceItem(string name, string? description, int damageReduction, int durability, EquipmentSlots slot)
            : base(name, description)
        {
            DamageReduction = damageReduction;
            Durability = durability;
            Slot = slot;
        }

        public override string ToString()
        {
            return $"{base.ToString()} (DR: {DamageReduction}, Durability: {Durability}, Slot: {Slot})";
        }
    }
}
