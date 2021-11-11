namespace Foxes.Core.Injection
{
    using System;
    using NUnit.Framework;
    using UnityEngine;

    public class ConfigManagerTests
    {
        [Test]
        public void AddConfigInstance_NullConfig_ThrowsArgumentNullException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            Assert.Throws<ArgumentNullException>(() => configManager.AddConfig(null as IConfig));
        }
        
        [Test]
        public void AddConfigInstance_FirstConfig_InjectedAndConfigured()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            var config = new MockConfig();
            MockConfig.ConfigureCount = 0;

            configManager.AddConfig(config);
            
            Assert.AreEqual(1, MockConfig.ConfigureCount);
            Assert.AreEqual(1, injector.InjectCount);
            Assert.AreSame(config, injector.LastInjectTarget);
        }
        
        [Test]
        public void AddConfigInstance_Twice_OnlyConfiguredOnce()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            var config = new MockConfig();
            MockConfig.ConfigureCount = 0;

            configManager.AddConfig(config);
            configManager.AddConfig(config);
            
            Assert.AreEqual(1, MockConfig.ConfigureCount);
            Assert.AreEqual(1, injector.InjectCount);
            Assert.AreSame(config, injector.LastInjectTarget);
        }
        
        [Test]
        public void AddConfigInstance_WithValidValidation_InjectedAndConfigured()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            var config = new MockConfigWithValidation();
            MockConfig.ConfigureCount = 0;
            MockConfigWithValidation.ReturnIsValid = true;
            
            configManager.AddConfig(config);
            
            Assert.AreEqual(1, MockConfig.ConfigureCount);
            Assert.AreEqual(1, injector.InjectCount);
            Assert.AreSame(config, injector.LastInjectTarget);
        }
        
        [Test]
        public void AddConfigInstance_WithInvalidValidation_ThrowsArgumentException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            var config = new MockConfigWithValidation();
            MockConfigWithValidation.ReturnIsValid = false;

            Assert.Throws<ArgumentException>(() => configManager.AddConfig(config));
        }
        
        [Test]
        public void AddConfigGeneric_FirstConfig_InjectedAndConfigured()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            MockConfig.ConfigureCount = 0;
            injector.CreateFactory = () => new MockConfig();
            
            configManager.AddConfig<MockConfig>();
            
            Assert.AreEqual(typeof(MockConfig), injector.CreateType);
            Assert.AreEqual(1, MockConfig.ConfigureCount);
        }
        
        [Test]
        public void AddConfigGeneric_Twice_OnlyConfiguredOnce()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);
            
            MockConfig.ConfigureCount = 0;
            injector.CreateFactory = () => new MockConfig();
            
            configManager.AddConfig<MockConfig>();
            configManager.AddConfig<MockConfig>();
            
            Assert.AreEqual(typeof(MockConfig), injector.CreateType);
            Assert.AreEqual(1, MockConfig.ConfigureCount);
        }
        
        [Test]
        public void AddConfigGeneric_WithValidValidation_InjectedAndConfigured()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);
            
            MockConfig.ConfigureCount = 0;
            MockConfigWithValidation.ReturnIsValid = true;
            injector.CreateFactory = () => new MockConfigWithValidation();
            
            configManager.AddConfig<MockConfigWithValidation>();
            
            Assert.AreEqual(typeof(MockConfigWithValidation), injector.CreateType);
            Assert.AreEqual(1, MockConfig.ConfigureCount);
        }
        
        [Test]
        public void AddConfigGeneric_WithInvalidValidation_ThrowsArgumentException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            MockConfigWithValidation.ReturnIsValid = false;
            injector.CreateFactory = () => new MockConfigWithValidation();

            Assert.Throws<ArgumentException>(() => configManager.AddConfig<MockConfigWithValidation>());
        }
        
        [Test]
        public void AddConfigGeneric_FromUnityType_ThrowsArgumentException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            Assert.Throws<ArgumentException>(() => configManager.AddConfig<MockConfigFromUnityType>());
        }
        
        [Test]
        public void AddConfigType_NullConfig_ThrowsArgumentNullException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            Assert.Throws<ArgumentNullException>(() => configManager.AddConfig(null as Type));
        }

        [Test]
        public void AddConfigType_FirstConfig_InjectedAndConfigured()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            MockConfig.ConfigureCount = 0;
            injector.CreateFactory = () => new MockConfig();
            
            configManager.AddConfig(typeof(MockConfig));
            
            Assert.AreEqual(typeof(MockConfig), injector.CreateType);
            Assert.AreEqual(1, MockConfig.ConfigureCount);
        }
        
        [Test]
        public void AddConfigType_Twice_OnlyConfiguredOnce()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);
            
            MockConfig.ConfigureCount = 0;
            injector.CreateFactory = () => new MockConfig();
            
            configManager.AddConfig(typeof(MockConfig));
            configManager.AddConfig(typeof(MockConfig));
            
            Assert.AreEqual(typeof(MockConfig), injector.CreateType);
            Assert.AreEqual(1, MockConfig.ConfigureCount);
        }
        
        [Test]
        public void AddConfigType_WithValidValidation_InjectedAndConfigured()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);
            
            MockConfig.ConfigureCount = 0;
            MockConfigWithValidation.ReturnIsValid = true;
            injector.CreateFactory = () => new MockConfigWithValidation();
            
            configManager.AddConfig(typeof(MockConfigWithValidation));
            
            Assert.AreEqual(typeof(MockConfigWithValidation), injector.CreateType);
            Assert.AreEqual(1, MockConfig.ConfigureCount);
        }
        
        [Test]
        public void AddConfigType_WithInvalidValidation_ThrowsArgumentException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            MockConfigWithValidation.ReturnIsValid = false;
            injector.CreateFactory = () => new MockConfigWithValidation();

            Assert.Throws<ArgumentException>(() => configManager.AddConfig(typeof(MockConfigWithValidation)));
        }
        
        [Test]
        public void AddConfigType_FromUnityType_ThrowsArgumentException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            Assert.Throws<ArgumentException>(() => configManager.AddConfig(typeof(MockConfigFromUnityType)));
        }
        
        [Test]
        public void AddConfigType_FromNonIConfig_ThrowsArgumentException()
        {
            var injector = new MockInjector();
            var configManager = new ConfigManager(injector);

            Assert.Throws<ArgumentException>(() => configManager.AddConfig(typeof(object)));
        }
        
        private class MockConfig : IConfig
        {
            public static int ConfigureCount { get; set; }
            
            public void Configure()
            {
                ConfigureCount++;
            }
        }
        
        private class MockConfigWithValidation : MockConfig, IHasConfigValidation
        {
            public static bool ReturnIsValid { get; set; }
            
            public bool IsValid()
            {
                return ReturnIsValid;
            }
        }
        
        private class MockConfigFromUnityType : ScriptableObject, IConfig
        {
            public void Configure()
            {
                throw new NotImplementedException();
            }
        }

        private class MockInjector : IInjector
        {
            public object LastInjectTarget { get; private set; }

            public int InjectCount { get; private set; }
            
            public Func<object> CreateFactory { get; set; }
            
            public Type CreateType { get; private set; }

            public void Inject(object target)
            {
                InjectCount++;
                LastInjectTarget = target;
            }

            public object Create(Type type)
            {
                CreateType = type;
                return CreateFactory();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Get<T>()
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                throw new NotImplementedException();
            }

            public ITypeBinder Bind<T>()
            {
                throw new NotImplementedException();
            }

            public ITypeBinder Bind(Type type)
            {
                throw new NotImplementedException();
            }

            public bool Unbind<T>()
            {
                throw new NotImplementedException();
            }

            public bool Unbind(Type type)
            {
                throw new NotImplementedException();
            }

            public bool IsBound<T>()
            {
                throw new NotImplementedException();
            }

            public bool IsBound(Type type)
            {
                throw new NotImplementedException();
            }

            public T Create<T>()
            {
                throw new NotImplementedException();
            }
        }
    }
}