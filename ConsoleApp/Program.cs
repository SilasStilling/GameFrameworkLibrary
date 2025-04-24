using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.ItemObjects;
using GameFrameworkLibrary.Enums;
using GameFrameworkLibrary.Factories;
using GameFrameworkLibrary;

Console.WriteLine("Start");

#region Initialize Framework
var gameFramework = Framework.Start("config.xml", "2DGameFramework");

// Services
var logger              = gameFramework.GetRequiredService<ILogger>();
var creatureFactory     = gameFramework.GetRequiredService<ICreatureFactory>();

// Generic factories
var armorFactory        = gameFramework.GetRequiredService<IFactory<IArmor>>();
var weaponFactory       = gameFramework.GetRequiredService<IFactory<IWeapon>>();
var consumableFactory   = gameFramework.GetRequiredService<IFactory<IUsable>>();

logger.Log(TraceEventType.Information, LogType.Game, "Demo starting...");

#endregion

#region Setup Items
// Weapons
weaponFactory.Register("9mm Pistol", () => new ConfigurableAttackItem(
    name: "9mm Pistol",
    description: "A standard 9mm Pistol",
    hitdamage: 39,
    range: 21,
    weaponType: WeaponType.Pistol));

weaponFactory.Register("AR-15", () => new ConfigurableAttackItem(
    name: "AR-15",
    description: "A standard AR-15 riffle",
    hitdamage: 50,
    range: 200,
    weaponType: WeaponType.AssaultRifle));

weaponFactory.Register("M4A1", () => new ConfigurableAttackItem(
    name: "M4A1",
    description: "A standard M4A1 riffle",
    hitdamage: 50,
    range: 200,
    weaponType: WeaponType.AssaultRifle));

// Armor

armorFactory.Register("Kevler Helmet", () => new ConfigurableDefenceItem(
    name: "Kevler Helmet",
    description: "A bulletproof  helmet that protects you from basic firearms",
    damageReduction: 20,
    durability: 100,
    slot: EquipmentSlots.head));

armorFactory.Register("Kevler Vest", () => new ConfigurableDefenceItem(
    name: "Kevler Vest",
    description: "A bulletproof  vest that protects you from basic firearms",
    damageReduction: 20,
    durability: 100,
    slot: EquipmentSlots.torso));

#endregion

#region Setup World and Creatures
var world = gameFramework.GetRequiredService<World>();

var player = creatureFactory.Create("Player", "The Main Character", 100, new Position(1, 1));

var enemy = creatureFactory.Create("Enemy", "The Main Enemy", 25, new Position(2, 2));

var mmPistol = weaponFactory.Create("9mm Pistol");
var m4A1 = weaponFactory.Create("M4A1");
var kevlerHelmet = armorFactory.Create("Kevler Helmet");
var kevlerVest = armorFactory.Create("Kevler Vest");

world.AddCreature(player);
world.AddCreature(enemy);

world.AddObject(new LootableObject(m4A1, new Position(2, 3), logger));

#endregion


#region Test World

player.EquipAttackItem(mmPistol);

player.EquipDefenceItem(kevlerHelmet);
player.EquipDefenceItem(kevlerVest);
//player.Move(new Position(2, 2));

player.Attack(enemy);

Console.WriteLine(world);
#endregion