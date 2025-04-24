using GameFrameworkLibrary.Models.Combat;
using GameFrameworkLibrary.Models.Creatures;
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


        event EventHandler<DeathEventArgs>? OnDeath;
        event EventHandler<HealthChangedEventArgs>? HealthChanged;


        void Move(int deltaX, int deltaY, World world);

        void RegisterAttackAction(string key, IAttackAction action);

        void Attack(string actionKey, ICreature target);

        void AdjustHitPoints(int delta);

    }
}
