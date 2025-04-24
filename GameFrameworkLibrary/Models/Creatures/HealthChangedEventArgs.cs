using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Creatures
{
    public class HealthChangedEventArgs : EventArgs
    {
        public int OldHp { get; }
        public int NewHp { get; }

        public HealthChangedEventArgs(int oldHp, int newHp)
        {
            OldHp = oldHp;
            NewHp = newHp;
        }
    }
}
