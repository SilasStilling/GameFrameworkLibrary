using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Base
{
    /// <summary>
    /// Represents a base class for items in the game world, inheriting common properties 
    /// and functionality from the WorldObject class.
    /// </summary>
    public abstract class ItemBase : WorldObject
    {
        protected ItemBase(string name, string? description)
            : base(name, description)
        {

        }
    }
}
