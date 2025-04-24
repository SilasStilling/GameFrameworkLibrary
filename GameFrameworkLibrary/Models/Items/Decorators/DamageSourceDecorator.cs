using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Decorators
{
    /// <summary>
    /// Represents a base decorator for enhancing or modifying the behavior of an <see cref="IDamageSource"/>.
    /// Implements both <see cref="IDamageSource"/> and <see cref="IItem"/> to ensure compatibility with damage sources and items.
    /// </summary>
    public abstract class DamageSourceDecorator : IDamageSource, IItem
    {
        /// <summary>
        /// The inner <see cref="IDamageSource"/> being decorated.
        /// </summary>
        protected readonly IDamageSource _inner;

        /// <summary>
        /// Gets the base damage of the decorated damage source.
        /// By default, this delegates to the inner damage source.
        /// </summary>
        public virtual int BaseDamage => _inner.BaseDamage;

        /// <summary>
        /// Gets the name of the decorated item.
        /// Delegates to the inner item's name.
        /// </summary>
        public string Name => ((IItem)_inner).Name;

        /// <summary>
        /// Gets the description of the decorated item.
        /// Delegates to the inner item's description.
        /// </summary>
        public string Description => ((IItem)_inner).Description;

        /// <summary>
        /// Initializes a new instance of the <see cref="DamageSourceDecorator"/> class.
        /// </summary>
        /// <param name="inner">The inner <see cref="IDamageSource"/> to decorate.</param>
        protected DamageSourceDecorator(IDamageSource inner)
        {
            _inner = inner ?? throw new ArgumentNullException(nameof(inner));
        }
    }
}