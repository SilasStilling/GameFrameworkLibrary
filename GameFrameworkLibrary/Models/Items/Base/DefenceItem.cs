using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Base
{
    /// <summary>
    /// Represents a base class for all defense-oriented items (armor) in the game.
    /// Provides common properties and functionality for armor, such as damage reduction, durability, and equipment slot.
    /// </summary>
    public abstract class DefenceItem : ItemBase, IArmor
    {
        /// <summary>
        /// Gets the equipment slot where this armor can be equipped (e.g., head, torso, legs, feet).
        /// </summary>
        public EquipmentSlots Slot { get; }

        /// <summary>
        /// Gets the damage reduction value provided by the armor.
        /// </summary>
        public int DamageReduction { get; }

        /// <summary>
        /// Gets the durability of the armor, representing how long it can last before breaking.
        /// </summary>
        public int Durability { get; }

        /// <summary>
        /// Explicit implementation of the <see cref="IArmor.EquipmentSlot"/> property.
        /// Maps to the <see cref="Slot"/> property.
        /// </summary>
        EquipmentSlots IArmor.EquipmentSlot => Slot;

        /// <summary>
        /// Initializes a new instance of the <see cref="DefenceItem"/> class.
        /// </summary>
        /// <param name="name">The name of the armor.</param>
        /// <param name="description">A description of the armor.</param>
        /// <param name="damageReduction">The damage reduction value provided by the armor.</param>
        /// <param name="durability">The durability of the armor.</param>
        /// <param name="slot">The equipment slot where the armor can be equipped.</param>
        public DefenceItem(string name, string? description, int damageReduction, int durability, EquipmentSlots slot)
            : base(name, description)
        {
            DamageReduction = damageReduction;
            Durability = durability;
            Slot = slot;
        }

        /// <summary>
        /// Returns a formatted string describing this armor’s base information,
        /// including its damage reduction, durability, and equipment slot.
        /// </summary>
        /// <returns>A string representation of the armor.</returns>
        public override string ToString()
        {
            return $"{base.ToString()} (DR: {DamageReduction}, Durability: {Durability}, Slot: {Slot})";
        }
    }
}