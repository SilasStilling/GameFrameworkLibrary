using GameFrameworkLibrary.Models.ItemObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Models.Factories
{
    public interface IAttackItemFactory
    {
        Pistol CreatePistol(string name, int hitdamage, int range, string description);

        Rifle CreateRifle(string name, int hitdamage, int range, string description);
    }
}
