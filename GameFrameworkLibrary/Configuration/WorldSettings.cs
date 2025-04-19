using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Configuration
{
    public class WorldSettings
    {
        public int WorldWidth { get; init; } = 50;
        public int WorldHeight { get; init; } = 50;
        public GameLevel GameLevel { get; init; } = GameLevel.Normal;
    }
}
