namespace Foxes.Core.Injection
{
    using System;
    using System.Collections.Generic;
    
    public class ConfigManager : IConfigManager
    {
        private readonly IInjector _injector;
        private readonly HashSet<Type> _configuredTypes;

        public ConfigManager(IInjector injector)
        {
            _injector = injector;
            _configuredTypes = new HashSet<Type>();
        }

        public void AddConfig(IConfig config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }
            
            if (_configuredTypes.Contains(config.GetType()))
            {
                return;
            }

            _injector.Inject(config);
            Configure(config);
        }

        public void AddConfig<T>() where T : IConfig
        {
            AddConfigType(typeof(T));
        }

        public void AddConfig(Type type)
        {
            if (type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            
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

            var config = (IConfig) _injector.Create(type);
            Configure(config);
        }

        private void Configure(IConfig config)
        {
            if (config is IHasConfigValidation configValidation && !configValidation.IsValid())
            {
                throw new ArgumentException($"{config.GetType().FullName} is invalid, please make sure all requirements are met.", nameof(config));
            }

            _configuredTypes.Add(config.GetType());

            config.Configure();
        }

        public void Dispose()
        {
            _configuredTypes.Clear();
        }
    }
}
