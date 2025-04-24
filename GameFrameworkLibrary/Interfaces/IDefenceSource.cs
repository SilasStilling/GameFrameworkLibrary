using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    /// <summary>
    /// Represents a source of defense in the game framework.
    /// This interface defines the damage reduction value that a defense source can provide.
    /// </summary>
    public interface IDefenceSource
    {
        /// <summary>
        /// Gets the damage reduction value provided by the defense source.
        /// </summary>
        int DamageReduction { get; }
    }
}