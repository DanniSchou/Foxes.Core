namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface IInjector : IDisposable
    {
        /// <summary>
        /// Get bound instance for type.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>Resolved instance bound to type.</returns>
        T Get<T>();

        /// <summary>
        /// Get bound instance for type.
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>Resolved instance bound to type.</returns>
        object Get(Type type);

        /// <summary>
        /// Bind type with an ITypeBinder.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <returns>ITypeBinder for selecting desired binding.</returns>
        ITypeBinder Bind<T>();

        /// <summary>
        /// Bind type with an ITypeBinder.
        /// </summary>
        /// <param name="type">Type</param>
        /// <returns>ITypeBinder for selecting desired binding.</returns>
        ITypeBinder Bind(Type type);

        /// <summary>
        /// Unbind type.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns>true if type was unbound.</returns>
        bool Unbind<T>();

        /// <summary>
        /// Unbind type.
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>true if type was unbound.</returns>
        bool Unbind(Type type);

        /// <summary>
        /// Check if type is bound.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns>true if type is bound.</returns>
        bool IsBound<T>();

        /// <summary>
        /// Check if type is bound.
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>true if type is bound.</returns>
        bool IsBound(Type type);
        
        /// <summary>
        /// Injects dependencies into target.
        /// </summary>
        /// <param name="target">target</param>
        void Inject(object target);

        /// <summary>
        /// Creates a new instance of type and injects it with dependencies.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <returns>The created instance.</returns>
        T Create<T>();

        /// <summary>
        /// Creates a new instance of type and injects it with dependencies.
        /// </summary>
        /// <param name="type">type</param>
        /// <returns>The created instance.</returns>
        object Create(Type type);
    }
}