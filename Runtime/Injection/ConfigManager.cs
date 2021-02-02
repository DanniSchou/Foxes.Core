namespace Foxes.Injection
{
    using System;
    using System.Collections.Generic;

    public class ConfigManager
    {
        [Inject]
        protected IInjector Injector;
        
        private readonly HashSet<Type> _configuredTypes;

        public ConfigManager()
        {
            _configuredTypes = new HashSet<Type>();
        }

        public void AddConfig(IConfig config)
        {
            if (_configuredTypes.Contains(config.GetType()))
            {
                return;
            }

            Configure(config);
        }

        public void AddConfig<T>() where T : IConfig
        {
            AddConfigType(typeof(T));
        }

        public void AddConfig(Type type)
        {
            if (!typeof(IConfig).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.FullName} needs to implement {nameof(IConfig)}", nameof(type));
            }
            
            AddConfigType(type);
        }

        private void AddConfigType(Type type)
        {
            if (_configuredTypes.Contains(type))
            {
                return;
            }
            
            if (typeof(UnityEngine.Object).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.FullName} is a Unity Object and needs to be passed as an instance.", nameof(type));
            }
            
            var config = (IConfig) Activator.CreateInstance(type);
            Configure(config);
        }

        private void Configure(IConfig config)
        {
            Injector.Inject(config);
            
            if (config is IHasConfigValidation configValidation && !configValidation.IsValid())
            {
                throw new ArgumentException($"{config.GetType().FullName} is invalid, please make sure all requirements are met.", nameof(config));
            }

            _configuredTypes.Add(config.GetType());

            if (config is IHasConfigDependencies configDependencies)
            {
                AddConfigDependencies(configDependencies);
            }

            config.Configure();
        }

        private void AddConfigDependencies(IHasConfigDependencies configDependencies)
        {
            foreach (var configType in configDependencies.GetDependencies())
            {
                AddConfig(configType);
            }
        }

        public void Dispose()
        {
            _configuredTypes.Clear();
        }
    }
}
