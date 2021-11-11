namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface ICommandMap : IDisposable
    {
        /// <summary>
        /// Maps event type to command type.
        /// </summary>
        /// <typeparam name="T">Event Type</typeparam>
        /// <typeparam name="TK">Command Type</typeparam>
        void Map<T, TK>() where TK : ICommand<T>;
        
        /// <summary>
        /// Removes mapping from event type to command type.
        /// </summary>
        /// <typeparam name="T">Event Type</typeparam>
        /// <typeparam name="TK">Command Type</typeparam>
        /// <returns>true if mapping was removed.</returns>
        bool UnMap<T, TK>() where TK : ICommand<T>;
        
        /// <summary>
        /// Checks if mapping exists from event type to command type.
        /// </summary>
        /// <typeparam name="T">Event Type</typeparam>
        /// <typeparam name="TK">Command Type</typeparam>
        /// <returns>true if mapping exists.</returns>
        bool HasMapping<T, TK>() where TK : ICommand<T>;
    }
}