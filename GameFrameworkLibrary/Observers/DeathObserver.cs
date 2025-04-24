using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using GameFrameworkLibrary.Models.Creatures;
using GameFrameworkLibrary.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameFrameworkLibrary.Models.Environment;
using GameFrameworkLibrary.Models.ItemObjects;

namespace GameFrameworkLibrary.Observers
{
    public class DeathObserver
    {
        private readonly World _world;
        private readonly ILogger _logger;
        private readonly IInventory _inventory;

        public DeathObserver(IEnumerable<Creature> creatures, World world, ILogger logger, IInventory inventory)
        {
            _world = world;
            _logger = logger;
            _inventory = inventory;

            foreach (var c in creatures)
                c.OnDeath += HandleDeath;
        }

        /// <summary>Subscribe a single creature’s HealthChanged event.</summary>
        public void Subscribe(ICreature creature)
        {
            creature.OnDeath += HandleDeath;
        }

        private void HandleDeath(object? sender, DeathEventArgs e)
        {
            var dead = e.DeadCreature;

            // 1) Unsubscribe to observer
            dead.OnDeath -= HandleDeath;

            _logger.Log(TraceEventType.Information, LogType.Inventory,
                        $"Spawning corpse for {dead.Name}.");

            // 2) Create corpse
            var corpse = new Container(
                name: $"Corpse of {dead.Name}",
                description: "Lootable remains",
                position: dead.Position,
                logger: _logger,
                isLootable: true,
                isRemovable: true);

            // 3) Transfer items
            foreach (var item in _inventory.RemoveAllItems(dead))
                corpse.AddItem(item);

            // 4) Insert into world
            _world.AddObject(corpse);

            // 5) Remove creature
            _world.RemoveCreature(dead);
        }
    }

}
