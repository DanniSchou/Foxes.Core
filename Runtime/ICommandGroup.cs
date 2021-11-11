namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface ICommandGroup : IDisposable
    {
        /// <summary>
        /// Add command type to group.
        /// </summary>
        /// <typeparam name="TK">Type</typeparam>
        /// <exception cref="ArgumentException">Throws ArgumentException if type is not an ICommand of the desired type.</exception>
        void Add<TK>();
        
        /// <summary>
        /// Remove command type from group.
        /// </summary>
        /// <typeparam name="TK">Type</typeparam>
        /// <returns>true if type was removed from the group.</returns>
        bool Remove<TK>();

        /// <summary>
        /// Check if command type has been added to the group.
        /// </summary>
        /// <typeparam name="TK">Type</typeparam>
        /// <returns>true if command type is already in the group.</returns>
        bool Contains<TK>();
    }
}