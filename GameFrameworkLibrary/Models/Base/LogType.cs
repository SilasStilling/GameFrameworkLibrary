using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Base
{
    public enum LogType
    {
        Game = 100,
        World = 200,
        Combat = 300,
        Inventory = 400,
        Configuration = 500,
        Error = 900
    }
}
