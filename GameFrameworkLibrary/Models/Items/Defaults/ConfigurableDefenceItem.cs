using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Models.Items.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Defaults
{
    /// <summary>
    /// Represents a configurable defense item in the game.
    /// This class provides a concrete implementation of the <see cref="DefenceItem"/> base class.
    /// </summary>
    public class ConfigurableDefenceItem : DefenceItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurableDefenceItem"/> class.
        /// </summary>
        /// <param name="name">The name of the defense item.</param>
        /// <param name="description">A description of the defense item.</param>
        /// <param name="damageReduction">The damage reduction value provided by the defense item.</param>
        /// <param name="durability">The durability of the defense item.</param>
        /// <param name="slot">The equipment slot where the defense item can be equipped (e.g., head, torso).</param>
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