using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Creatures
{
    /// <summary>
    /// Represents the event arguments for a creature's death event.
    /// </summary>
    public class DeathEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the creature that has died.
        /// </summary>
        public ICreature DeadCreature { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathEventArgs"/> class.
        /// </summary>
        /// <param name="dead">The creature that has died.</param>
        public DeathEventArgs(ICreature dead) => DeadCreature = dead;
    }
}