using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a source of damage in the game framework.
    /// This interface defines the base damage value that a damage source can provide.
    /// </summary>
    public interface IDamageSource
    {
        /// <summary>
        /// Gets the base damage value of the damage source.
        /// </summary>
        int BaseDamage { get; }
    }
}