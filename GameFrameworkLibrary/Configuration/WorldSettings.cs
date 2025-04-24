using GameFrameworkLibrary.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Configuration
{
    /// <summary>
    /// Represents the configuration settings for the game world.
    /// </summary>
    public class WorldSettings
    {
        /// <summary>
        /// The width of the game world in units.
        /// Defaults to 50.
        /// </summary>
        public int WorldWidth { get; init; } = 50;

        /// <summary>
        /// The height of the game world in units.
        /// Defaults to 50.
        /// </summary>
        public int WorldHeight { get; init; } = 50;

        /// <summary>
        /// The difficulty level of the game world.
        /// Defaults to <see cref="GameLevel.Normal"/>.
        /// </summary>
        public GameLevel GameLevel { get; init; } = GameLevel.Normal;
    }
}