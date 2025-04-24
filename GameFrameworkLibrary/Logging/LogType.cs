using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Base
{
    /// <summary>
    /// Represents the different categories of logs in the game framework.
    /// This enum is used to classify log messages for better organization and filtering.
    /// </summary>
    public enum LogType
    {
        /// <summary>
        /// General game-related logs.
        /// </summary>
        Game = 100,

        /// <summary>
        /// Logs related to the game world, such as environment or world state changes.
        /// </summary>
        World = 200,

        /// <summary>
        /// Logs related to combat events, such as attacks or damage calculations.
        /// </summary>
        Combat = 300,

        /// <summary>
        /// Logs related to inventory actions, such as adding or removing items.
        /// </summary>
        Inventory = 400,

        /// <summary>
        /// Logs related to configuration changes or settings.
        /// </summary>
        Configuration = 500,

        /// <summary>
        /// Logs for errors or critical issues in the game framework.
        /// </summary>
        Error = 900
    }
}