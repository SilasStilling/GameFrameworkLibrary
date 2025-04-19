using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.Factories;

Console.WriteLine("Start");

#region Initialize Framework
var gameFramework = Framework.Start();
var frameworkLog = gameFramework.GetRequiredService<ILogger>();
frameworkLog.Log(TraceEventType.Information, LogType.Game, "GameFramework started.");

// Factories 
var creatureFactory = gameFramework.GetRequiredService<ICreatureFactory>();
var attackItemFactory = gameFramework.GetRequiredService<IAttackItemFactory>();
var defenceItemFactory = gameFramework.GetRequiredService<IDefenceItemFactory>();
#endregion

#region Setup World and Creatures
var world = gameFramework.GetRequiredService<World>();
var player = creatureFactory.Create("John Doe", new Position(2, 2), 100, "The Main Player");
var enemy = creatureFactory.Create("evil person", new Position(4, 4), 50, "Very dumb enemy");
#endregion

#region Setup Items
var pistol = attackItemFactory.CreatePistol(
    name: "9mm Pistol",
    hitdamage: 29,
    range: 20,
    description: "A standard 9mm pistol.");

var rifle = attackItemFactory.CreateRifle(
    name: "M4 Carbine",
    hitdamage: 50,
    range: 100,
    description: "A standard M4 Carbine.");

//var helmet = defenceItemFactory.CreateHelmet(
//    name: "Combat Helmet",
//    damageReduction: 10,
//    description: "A standard combat helmet.");
//int durability = 100;

//var vest = defenceItemFactory.CreateChest(
//    name: "Kevlar Vest",
//    damageReduction: 20,
//    description: "A standard kevlar vest.");

var stone = new EnvironmentObject(
    name: "Stone",
    description: "A huge stone that blocks people from going this way.",
    new Position(8, 1));
#endregion

#region Test Attack
//player.Attack(enemy);


#endregion