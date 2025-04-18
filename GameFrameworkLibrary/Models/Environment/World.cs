using GameFrameworkLibrary.Interfaces;
using GameFrameworkLibrary.Models.Base;
using System.Diagnostics;

namespace GameFrameworkLibrary.Models.Environment
{
    public class World
    {
        public int WorldWidth { get; }
        public int WorldHeight { get; }
        public GameLevel GameLevel { get; }

        private readonly ILogger _logger;
        private readonly List<Creature> _creatures = new();
        private readonly List<WorldObject> _objects = new();

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

        public void AddCreature(Creature creature)
        {
            ValidatePosition(creature);

            _creatures.Add(creature);
            _logger.Log(
                TraceEventType.Information,
                LogType.World,
                $"Creature '{creature.Name}' added at {creature.Position}");
        }

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
        public void AddObject(WorldObject worldObject)
        {
            ValidatePosition(worldObject);

            _objects.Add(worldObject);
            _logger.Log(
                TraceEventType.Information,
                LogType.World,
                $"World object '{worldObject.Name}' added at {worldObject.Position}");
        }
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