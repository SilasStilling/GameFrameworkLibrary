using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Creatures
{
    /// <summary>
    /// Represents the event arguments for a creature's health change event.
    /// </summary>
    public class HealthChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the creature's health value before the change.
        /// </summary>
        public int OldHp { get; }

        /// <summary>
        /// Gets the creature's health value after the change.
        /// </summary>
        public int NewHp { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthChangedEventArgs"/> class.
        /// </summary>
        /// <param name="oldHp">The health value before the change.</param>
        /// <param name="newHp">The health value after the change.</param>
        public HealthChangedEventArgs(int oldHp, int newHp)
        {
            OldHp = oldHp;
            NewHp = newHp;
        }
    }
}