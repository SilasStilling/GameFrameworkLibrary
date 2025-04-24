using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Enums
{
    /// <summary>
    /// Represents the different equipment slots available for a character or creature.
    /// These slots define where specific items (e.g., armor, gear) can be equipped.
    /// </summary>
    public enum EquipmentSlots
    {
        /// <summary>
        /// The slot for headgear or helmets.
        /// </summary>
        head,

        /// <summary>
        /// The slot for torso armor or clothing.
        /// </summary>
        torso,

        /// <summary>
        /// The slot for leg armor or pants.
        /// </summary>
        legs,

        /// <summary>
        /// The slot for footwear or boots.
        /// </summary>
        feet
    }
}