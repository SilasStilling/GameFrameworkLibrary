using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Creatures
{
    public class DeathEventArgs : EventArgs
    {
        public ICreature DeadCreature { get; }
        public DeathEventArgs(ICreature dead) => DeadCreature = dead;
    }
}
