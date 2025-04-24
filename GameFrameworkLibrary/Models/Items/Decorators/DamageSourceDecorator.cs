using GameFrameworkLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Items.Decorators
{
    public abstract class DamageSourceDecorator : IDamageSource, IItem
    {
        protected readonly IDamageSource _inner;

        public DamageType DamageType => _inner.DamageType;
        public virtual int BaseDamage => _inner.BaseDamage;
        public string Name => ((IItem)_inner).Name;
        public string Description => ((IItem)_inner).Description;

        protected DamageSourceDecorator(IDamageSource inner)
        {
            _inner = inner;
        }
    }
}
