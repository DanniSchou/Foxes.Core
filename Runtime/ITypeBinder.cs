namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface ITypeBinder
    {
        /// <summary>
        /// Bind as Singleton of the created type.
        /// </summary>
        void AsSingle();

        /// <summary>
        /// Bind to Singleton of selected type.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        void ToSingle<T>();

        /// <summary>
        /// Bind to Singleton of selected type.
        /// </summary>
        /// <param name="type">type</param>
        void ToSingle(Type type);
        
        /// <summary>
        /// Bind to value.
        /// </summary>
        /// <param name="value">value</param>
        void ToValue(object value);
        
        /// <summary>
        /// Bind as created type.
        /// </summary>
        void AsType();
        
        /// <summary>
        /// Bind to selected type.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        void ToType<T>();

        /// <summary>
        /// Bind to selected type.
        /// </summary>
        /// <param name="type">type</param>
        void ToType(Type type);

        /// <summary>
        /// Bind to factory method.
        /// </summary>
        /// <param name="factory">factory</param>
        void ToMethod(Func<object> factory);
    }
}
