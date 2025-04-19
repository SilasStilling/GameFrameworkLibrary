using GameFrameworkLibrary.Models.Creatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Defines an object that can be used on a creature.
    /// </summary>
    public interface IUsable
    {
        /// <summary>
        /// Applies this usable object's effect to the specified creature.
        /// </summary>
        /// <param name="target">The creature on which this object is used.</param>
        void UseOn(Creature target);
    }
}
