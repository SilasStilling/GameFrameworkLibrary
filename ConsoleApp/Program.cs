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
using GameFrameworkLibrary.Models.Items.Defaults;
using GameFrameworkLibrary.Models.Items.Base;
using GameFrameworkLibrary.Models.Combat;


#region Setup
Console.WriteLine("Setting up services");
var framework = Framework.Start("config.xml", "GameFramework");

var logger = framework.GetRequiredService<ILogger>();
var combatService = framework.GetRequiredService<ICombatService>();
var creatureFactory = framework.GetRequiredService<ICreatureFactory>();

// Factories
var armorFactory = framework.GetRequiredService<IFactory<IArmor>>();
var weaponFactory = framework.GetRequiredService<IFactory<IWeapon>>();
 var consumableFactory = framework.GetRequiredService<IFactory<IUsable>>();

// World
var world = framework.GetRequiredService<World>();
#endregion

#region Create Items
Console.WriteLine("Creating weapons");

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
    description: "A bulletproof helmet that protects you from basic firearms",
    damageReduction: 20,
    durability: 100,
    slot: EquipmentSlots.head));

armorFactory.Register("Kevler Vest", () => new ConfigurableDefenceItem(
    name: "Kevler Vest",
    description: "A bulletproof vest that protects you from basic firearms",
    damageReduction: 20,
    durability: 100,
    slot: EquipmentSlots.torso));

#endregion

#region Creatures
var player = creatureFactory.Create("Player", "The Main Character", 100, new Position(1, 1));

var enemy = creatureFactory.Create("Enemy", "The Evil Enemy", 50, new Position(2, 2));
#endregion

Console.WriteLine("_________________________________________");

#region Loot
var container = new Container(
    name: "Large Weapon container",
    description: "A Weapon container that could contain any weapon",
    position: new Position(2, 2),
    logger: logger);

var weapon = weaponFactory.Create("9mm Pistol");
var weapon2 = weaponFactory.Create("AR-15");
var amor = armorFactory.Create("Kevler Helmet");

container.AddItem(weapon);
container.AddItem(weapon2);
container.AddItem(amor);
Console.WriteLine("_________________________________________");


enemy.Inventory.AddItem(amor);
enemy.Inventory.AddItem(weapon);
enemy.EquipArmor(amor);
enemy.EquipWeapon(weapon);
Console.WriteLine("Enemy equipped armor and weapon");

Console.WriteLine("_________________________________________");
Console.WriteLine($"\n{container}");

Console.WriteLine("_________________________________________");
// Hero loots chest
player.Loot(container, world);
Console.WriteLine("\nRemaining in chest: " + container.PeekLoot().Count() + " items");


#endregion

#region Objects
var stone = new EnvironmentObject(
    name: "Stone",
    description: "A stone that could be used to throw at enemies",
    position: new Position(5, 3));

world.AddObject(stone);
world.AddCreature(player);
world.AddCreature(enemy);
world.AddObject(container);

#endregion

#region Movement
player.Move(2, 2, world);
enemy.Move(3, 3, world);
#endregion

#region Combat
IAttackAction pistolAttack = new DamageSourceAttack(weapon, combatService);
IAttackAction m4Attack = new DamageSourceAttack(weapon2, combatService);

player.RegisterAttackAction("pistol", pistolAttack);
player.RegisterAttackAction("m4", m4Attack);

Console.WriteLine("pistol Attack");
player.Attack("pistol", enemy);
Console.ReadKey();

Console.WriteLine("m4 Attack");
player.Attack("m4", enemy);
Console.ReadKey();

Console.WriteLine("m4 Attack");
player.Attack("m4", enemy);
Console.ReadKey();

Console.WriteLine("pistol Attack");
player.Attack("pistol", enemy);
Console.ReadKey();
#endregion