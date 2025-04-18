using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using System.Diagnostics;

namespace GameFrameworkLibrary.Models.Environment
{
    /// <summary>
    /// Represents the game world, managing its dimensions, difficulty level, and entities (creatures and objects).
    /// Provides methods to add, remove, and retrieve entities while ensuring valid positions and logging actions.
    /// </summary>
    public class World
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }
        /// <summary>
        /// Gets the difficulty level of the game world.
        /// </summary>
        public GameLevel GameLevel { get; }

        private readonly ILogger _logger;
        private readonly List<Creature> _creatures = new();
        private readonly List<WorldObject> _objects = new();

        /// <summary>
        /// Initializes a new instance of the <see cref="World"/> class with the specified dimensions, logger, and difficulty level.
        /// </summary>
        /// <param name="width">The width of the game world.</param>
        /// <param name="height">The height of the game world.</param>
        /// <param name="logger">The logger instance for logging actions and events.</param>
        /// <param name="level">The difficulty level of the game world (default is <see cref="GameLevel.Normal"/>).</param>
        public World(int width, int height, ILogger logger, GameLevel level = GameLevel.Normal)
        {
            WorldWidth = width;
            WorldHeight = height;
            GameLevel = level;

            _logger = logger;
            _logger.Log(
                TraceEventType.Information,
                LogType.World,
                $"World created: {width}x{height}, Level={level}");
        }

        /// <summary>
        /// Adds a creature to the game world after validating its position.
        /// </summary>
        /// <param name="creature">The creature to add.</param>
        public void AddCreature(Creature creature)
        {
            ValidatePosition(creature);

            _creatures.Add(creature);
            _logger.Log(
                TraceEventType.Information,
                LogType.World,
                $"Creature '{creature.Name}' added at {creature.Position}");
        }

        /// <summary>
        /// Validates that the position of an entity is within the bounds of the world.
        /// </summary>
        /// <param name="entity">The entity to validate.</param>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the position is outside the world bounds.</exception>
        private void ValidatePosition(IHasPosition entity)
        {
            var pos = entity.Position;
            if (pos.X < 0 || pos.X > WorldWidth || pos.Y < 0 || pos.Y > WorldHeight)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(entity),
                    pos,
                    $"Position {pos} is outside world bounds (0,0) to ({WorldWidth},{WorldHeight})");
            }
        }

        /// <summary>
        /// Adds a world object to the game world after validating its position.
        /// </summary>
        /// <param name="worldObject">The world object to add.</param>
        public void AddObject(WorldObject worldObject)
        {
            ValidatePosition(worldObject);

            _objects.Add(worldObject);
            _logger.Log(
                TraceEventType.Information,
                LogType.World,
                $"World object '{worldObject.Name}' added at {worldObject.Position}");
        }

        /// <summary>
        /// Removes a world object from the game world if it is marked as removable.
        /// </summary>
        /// <param name="worldObject">The world object to remove.</param>
        public void RemoveObject(WorldObject worldObject)
        {
            if (worldObject.IsRemovable)
            {
                _objects.Remove(worldObject);
                _logger.Log(
                    TraceEventType.Information,
                    LogType.World,
                    $"Removable object '{worldObject.Name}' removed from world");
            }
        }
        /// <summary>
        /// Returns all creatures currently in the world.
        /// </summary>
        /// <returns>An enumerable of <see cref="Creature"/> instances.</returns>
        public IEnumerable<Creature> GetCreatures() => _creatures;

        /// <summary>
        /// Returns all environment objects currently in the world.
        /// </summary>
        /// <returns>An enumerable of <see cref="WorldObject"/> instances.</returns>
        public IEnumerable<WorldObject> GetObjects() => _objects;
    }
}