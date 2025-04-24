using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameFrameworkLibrary.Factories
{
    /// <summary>
    /// Interface for a generic factory that can register and create objects of type T using string keys.
    /// </summary>
    public interface IFactory<T>
    {
        /// <summary>
        /// Registers a creator function under a specific string key.
        /// </summary>
        /// <param name="key">The key to associate with the creator function.</param>
        /// <param name="creator">The function that creates instances of T.</param>
        void Register(string key, Func<T> creator);

        /// <summary>
        /// Creates an instance of T using the creator function associated with the given key.
        /// </summary>
        /// <param name="key">The key of the registered creator.</param>
        /// <returns>An instance of type T.</returns>
        T Create(string key);

        /// <summary>
        /// Gets all registered keys in the factory.
        /// </summary>
        IEnumerable<string> RegisteredKeys { get; }
    }

    /// <summary>
    /// Generic factory implementation that maps string keys to creator functions.
    /// Allows dynamic creation of objects based on registered types.
    /// </summary>
    public class Factory<T> : IFactory<T>
    {
        // Dictionary that stores creator functions associated with string keys.
        private readonly Dictionary<string, Func<T>> _creators = new();

        /// <summary>
        /// Registers a new creator function for a given key.
        /// Throws if the key or creator is null or invalid.
        /// </summary>
        /// <param name="key">The key to associate with the creator.</param>
        /// <param name="creator">The function that creates instances of T.</param>
        public void Register(string key, Func<T> creator)
        {
            // Validate that key is not null
            ArgumentNullException.ThrowIfNull(key);

            // Reject empty strings as keys
            if (key == string.Empty)
                throw new ArgumentException("Key cannot be empty", nameof(key));

            // Validate that creator is not null
            ArgumentNullException.ThrowIfNull(creator);

            // Add or update the key with the new creator
            _creators[key] = creator;
        }

        /// <summary>
        /// Creates an instance of T based on a registered key.
        /// Throws an exception if the key is not found.
        /// </summary>
        /// <param name="key">The key of the desired creator.</param>
        /// <returns>A new instance of T.</returns>
        public T Create(string key)
        {
            if (!_creators.TryGetValue(key, out var creator))
                throw new KeyNotFoundException($"No creator for key '{key}'");

            return creator();
        }

        /// <summary>
        /// Returns a collection of all registered keys.
        /// </summary>
        public IEnumerable<string> RegisteredKeys => _creators.Keys;
    }
}