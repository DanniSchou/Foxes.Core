namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface IConfigManager : IDisposable
    {
        /// <summary>
        /// Add config from instance.
        /// </summary>
        /// <param name="config">config</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if config is null.</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException if instance is an IHasConfigValidation and isn't valid.</exception>
        void AddConfig(IConfig config);
        
        /// <summary>
        /// Add config from type.
        /// </summary>
        /// <typeparam name="T">type</typeparam>
        /// <exception cref="ArgumentException">Throws ArgumentException if type is UnityEngine.Object or if instance is an IHasConfigValidation and isn't valid.</exception>
        void AddConfig<T>() where T : IConfig;
        
        /// <summary>
        /// Add config from type.
        /// </summary>
        /// <param name="type">type</param>
        /// <exception cref="ArgumentNullException">Throws ArgumentNullException if type is null.</exception>
        /// <exception cref="ArgumentException">Throws ArgumentException if type isn't assignable from IConfig, if type is UnityEngine.Object, or if instance is an IHasConfigValidation and isn't valid.</exception>
        void AddConfig(Type type);
    }
}