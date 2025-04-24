using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Interfaces
{
    public interface ICreature : ICombatStats, IHasPosition
    {
        string Name { get; }
        int HitPoints { get; }

        void Move(int deltaX, int deltaY, World world);

        void Attack(ICreature target);

        void AdjustHitPoints(int delta);
    }
}
