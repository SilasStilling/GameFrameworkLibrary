using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Combat
{
    /// <summary>
    /// Represents a composite attack action that can execute multiple child attack actions.
    /// This class implements the Composite design pattern, allowing multiple attack actions to be grouped and executed as one.
    /// </summary>
    public class CompositeAttackAction : IAttackAction
    {
        private readonly List<IAttackAction> _children = new();

        /// <summary>
        /// Adds a child attack action to the composite.
        /// </summary>
        /// <param name="action">The attack action to add.</param>
        public void Add(IAttackAction action) => _children.Add(action);

        /// <summary>
        /// Removes a child attack action from the composite.
        /// </summary>
        /// <param name="action">The attack action to remove.</param>
        public void Remove(IAttackAction action) => _children.Remove(action);

        /// <summary>
        /// Executes all child attack actions in the composite.
        /// </summary>
        /// <param name="attacker">The creature performing the attack.</param>
        /// <param name="target">The target creature being attacked.</param>
        public void Execute(ICreature attacker, ICreature target)
        {
            foreach (var action in _children)
                action.Execute(attacker, target);
        }
    }
}