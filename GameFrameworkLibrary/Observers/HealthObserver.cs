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

namespace GameFrameworkLibrary.Observers
{
    /// <summary>
    /// Observes and handles health changes for creatures in the game world.
    /// Logs health changes and tracks creatures with specific health thresholds.
    /// </summary>
    public class HealthObserver
    {
        private readonly ILogger _logger;
        private readonly IInventory _inventory;
        private readonly Dictionary<ICreature, double> _thresholds = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="HealthObserver"/> class.
        /// </summary>
        /// <param name="logger">The logger instance for logging health changes.</param>
        /// <param name="inventory">The inventory service for managing creature items (not currently used).</param>
        public HealthObserver(
            ILogger logger,
            IInventory inventory)
        {
            _logger = logger;
            _inventory = inventory;
        }

        /// <summary>
        /// Subscribes to the health changes of a specific creature and tracks a health threshold.
        /// </summary>
        /// <param name="creature">The creature to observe.</param>
        /// <param name="thresholdFraction">The health threshold as a fraction (0.0 to 1.0) of the creature's maximum health.</param>
        public void Subscribe(ICreature creature, double thresholdFraction)
        {
            // Clamp the threshold between 0 and 1
            var frac = Math.Clamp(thresholdFraction, 0.0, 1.0);
            _thresholds[creature] = frac;
            creature.HealthChanged += OnHealthChanged;
        }

        /// <summary>
        /// Handles the health change event of a creature.
        /// Logs the health change and ensures the creature is tracked only if it meets the threshold.
        /// </summary>
        /// <param name="sender">The source of the event (expected to be an <see cref="ICreature"/>).</param>
        /// <param name="e">The event arguments containing the old and new health values.</param>
        private void OnHealthChanged(object? sender, HealthChangedEventArgs e)
        {
            if (sender is not ICreature creature) return;

            // Skip if the creature is not tracked or is already dead
            if (!_thresholds.TryGetValue(creature, out var thresholdFraction)) return;
            if (e.NewHp <= 0) return;

            // Log the health change
            _logger.Log(TraceEventType.Information, LogType.Combat, $"{creature.Name} health changed to {e.NewHp}");
        }
    }
}