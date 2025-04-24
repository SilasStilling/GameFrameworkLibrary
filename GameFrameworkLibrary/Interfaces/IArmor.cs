using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Enums;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents an armor item in the game.
    /// Combines defensive capabilities and item properties, 
    /// and specifies the equipment slot where the armor can be equipped.
    /// </summary>
    public interface IArmor : IDefenceSource, IItem
    {
        /// <summary>
        /// Gets the equipment slot where this armor can be equipped (e.g., head, torso, legs, feet).
        /// </summary>
        EquipmentSlots EquipmentSlot { get; }
    }
}