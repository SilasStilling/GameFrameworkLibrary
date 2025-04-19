using GameFrameworkLibrary.Models;
using GameFrameworkLibrary.Configuration;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Logging;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics;
using GameFrameworkLibrary.Models.Environment;

Console.WriteLine("Start");

#region Initialize Framework
var gameFramework = GameFramework.Start();
var frameworkLog = gameFramework.GetRequiredService<ILogger>();
frameworkLog.Log(TraceEventType.Information, LogType.Game, "GameFramework started.");

// Factories 
var creatureFactory = gameFramework.GetRequiredService<ICreatureFactory>();
var attackItemFactory = gameFramework.GetRequiredService<IAttackItemFactory>();
var defenceItemFactory = gameFramework.GetRequiredService<IDefenceItemFactory>();
#endregion

#region Setup World and Creatures
var world = gameFramework.GetRequiredService<World>();
var player = creatureFactory.Create("John Doe", new Position(2, 3), 100);
var enemy = creatureFactory.Create("Abe", new Position(5, 7), 50);
#endregion

#region Setup Items
var pistol = attackItemFactory.Create(
    name: "9mm Pistol",
    hitdamage: 29,
    range: 20,
    description: "A standard 9mm pistol.");

var rifle = attackItemFactory.Create(
    name: "M4 Carbine",
    hitdamage: 50,
    range: 100,
    description: "A standard M4 Carbine.");

var helmet = defenceItemFactory.Create(
    name: "Combat Helmet",
    damageReduction: 10,
    description: "A standard combat helmet.");
var vest = defenceItemFactory.Create(
    name: "Kevlar Vest",
    damageReduction: 20,
    description: "A standard kevlar vest.");

var stone = new EnvironmentObject(
    name: "Stone",
    description: "A huge stone that blocks people from going this way.",
    new Position(8, 1));
#endregion

#region Test Attack
player.Attack(enemy);


#endregion