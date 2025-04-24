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
    /// <summary>
    /// Observes and handles death events for creatures in the game world.
    /// Responsible for spawning lootable corpses and removing dead creatures from the world.
    /// </summary>
    public class DeathObserver
    {
        private readonly World _world;
        private readonly ILogger _logger;
        private readonly IInventory _inventory;

        /// <summary>
        /// Initializes a new instance of the <see cref="DeathObserver"/> class.
        /// Subscribes to the death events of the provided creatures.
        /// </summary>
        /// <param name="creatures">The collection of creatures to observe.</param>
        /// <param name="world">The game world where the creatures exist.</param>
        /// <param name="logger">The logger instance for logging actions.</param>
        /// <param name="inventory">The inventory service for managing creature items.</param>
        public DeathObserver(IEnumerable<Creature> creatures, World world, ILogger logger, IInventory inventory)
        {
            _world = world;
            _logger = logger;
            _inventory = inventory;

            foreach (var c in creatures)
                c.OnDeath += HandleDeath;
        }

        /// <summary>
        /// Subscribes to the death event of a specific creature.
        /// </summary>
        /// <param name="creature">The creature to observe.</param>
        public void Subscribe(ICreature creature)
        {
            creature.OnDeath += HandleDeath;
        }

        /// <summary>
        /// Handles the death event of a creature.
        /// Spawns a lootable corpse, transfers the creature's items to the corpse, and removes the creature from the world.
        /// </summary>
        /// <param name="sender">The source of the event (not used).</param>
        /// <param name="e">The event arguments containing the dead creature.</param>
        private void HandleDeath(object? sender, DeathEventArgs e)
        {
            var dead = e.DeadCreature;

            // Unsubscribe from the creature's death event to avoid memory leaks.
            dead.OnDeath -= HandleDeath;

            // Log the spawning of the corpse.
            _logger.Log(TraceEventType.Information, LogType.Inventory,
                        $"Spawning corpse for {dead.Name}.");

            // Create a lootable corpse at the creature's position.
            var corpse = new Container(
                name: $"Corpse of {dead.Name}",
                description: "Lootable remains",
                position: dead.Position,
                logger: _logger,
                isLootable: true,
                isRemovable: true);

            // Transfer all items from the dead creature's inventory to the corpse.
            foreach (var item in _inventory.RemoveAllItems(dead))
                corpse.AddItem(item);

            // Add the corpse to the world and remove the dead creature.
            _world.AddObject(corpse);
            _world.RemoveCreature(dead);
        }
    }
}