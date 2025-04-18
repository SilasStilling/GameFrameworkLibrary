using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Configuration
{
    /// <summary>
    /// Represents the configuration settings for the game, including world dimensions, 
    /// game difficulty level, logging level, and logging listeners.
    /// </summary>
    public class GameConfig
    {
        /// <summary>
        /// Gets or sets the width of the game world. Default is 50.
        /// </summary>
        public int WorldWidth { get; set; } = 50;

        /// <summary>
        /// Gets or sets the height of the game world. Default is 50.
        /// </summary>
        public int WorldHeight { get; set; } = 50;

        /// <summary>
        /// Gets or sets the difficulty level of the game. Default is GameLevel.Normal.
        /// </summary>
        public GameLevel GameLevel { get; set; } = GameLevel.Normal;

        /// <summary>
        /// Gets or sets the logging level for the game. Default is SourceLevels.All.
        /// </summary>
        public SourceLevels LogLevel { get; set; } = SourceLevels.All;

        /// <summary>
        /// Gets or sets the list of listener configurations for logging or other purposes.
        /// </summary>
        public List<ListenerConfig> Listeners { get; set; } = new List<ListenerConfig>();
    }
}