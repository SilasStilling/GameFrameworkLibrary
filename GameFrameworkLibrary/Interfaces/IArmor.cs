using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Base;

namespace GameFrameworkLibrary.Interfaces
{
    public interface IArmor : IDefenceSource, IItem
    {
        EquipmentSlots EquipmentSlot { get; }
    }
}
