namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public interface IResolverMap : IDisposable
    {
        /// <summary>
        /// Get resolved instance of type.
        /// </summary>
        /// <param name="type">Key</param>
        /// <returns>Resolved instance</returns>
        /// <exception cref="ArgumentException">Throws an ArgumentException if type hasn't been set.</exception>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if type is null.</exception>
        object Get(Type type);
        
        /// <summary>
        /// Set resolver for type.
        /// </summary>
        /// <param name="type">Key</param>
        /// <param name="resolver">Resolver</param>
        /// <exception cref="ArgumentException">Throws an ArgumentException if type is already set.</exception>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if type or resolver is null.</exception>
        void Set(Type type, IResolver resolver);

        /// <summary>
        /// Remove resolver for type.
        /// </summary>
        /// <param name="type">Key</param>
        /// <returns>true if resolver for type is successfully removed.</returns>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if type is null.</exception>
        bool Remove(Type type);

        /// <summary>
        /// Check if there's a resolver for type.
        /// </summary>
        /// <param name="type">Key</param>
        /// <returns>true if a resolver is mapped to type.</returns>
        /// <exception cref="ArgumentNullException">Throws an ArgumentNullException if type is null.</exception>
        bool Contains(Type type);
    }
}
