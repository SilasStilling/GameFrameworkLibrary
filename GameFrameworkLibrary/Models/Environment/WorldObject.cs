using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Environment
{
    public class WorldObject : Base.WorldObject, IHasPosition
    {
        public Position Position { get; internal set; }
        public bool IsLootable { get; internal set; }
        public bool IsRemovable { get; internal set; }

        public WorldObject(string name, string? description, Position position, bool isLootable = false, bool isRemovable = false)
    : base(name, description)
        {
            Position = position;
            IsLootable = isLootable;
            IsRemovable = isRemovable;
        }
        public override string ToString()
        {
            return $"{base.ToString()} (Position: {Position}, Lootable: {IsLootable}, Removable: {IsRemovable})";
        }
    }
}
