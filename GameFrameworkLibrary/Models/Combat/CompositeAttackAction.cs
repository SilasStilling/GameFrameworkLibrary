using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Combat
{
    public class CompositeAttackAction : IAttackAction
    {
        private readonly List<IAttackAction> _children = new();

        public void Add(IAttackAction action) => _children.Add(action);
        public void Remove(IAttackAction action) => _children.Remove(action);

        public void Execute(ICreature attacker, ICreature target)
        {
            foreach (var action in _children)
                action.Execute(attacker, target);
        }
    }
}
