namespace Foxes.Core.Injection.Resolvers
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class TypeBinderTests
    {
        [Test]
        public void AddAsSingle_TypeAddedWithSingletonResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var type = typeof(MockTarget);
            
            new TypeBinder(injector, resolverMap, type).AsSingle();
            
            Assert.True(resolverMap.Map.ContainsKey(type));
            var resolver = resolverMap.Map[type];
            Assert.IsAssignableFrom<SingletonResolver>(resolver);
            Assert.AreEqual(type, ((SingletonResolver)resolver).Target);
        }
        
        [Test]
        public void AddToSingleGeneric_TypeAddedWithSingletonResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var bindType = typeof(IMockTarget);
            var resolveType = typeof(MockTarget);
            
            new TypeBinder(injector, resolverMap, bindType).ToSingle<MockTarget>();
            
            Assert.True(resolverMap.Map.ContainsKey(bindType));
            var resolver = resolverMap.Map[bindType];
            Assert.IsAssignableFrom<SingletonResolver>(resolver);
            Assert.AreEqual(resolveType, ((SingletonResolver)resolver).Target);
        }
        
        [Test]
        public void AddToSingle_TypeAddedWithSingletonResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var bindType = typeof(IMockTarget);
            var resolveType = typeof(MockTarget);
            
            new TypeBinder(injector, resolverMap, bindType).ToSingle(resolveType);
            
            Assert.True(resolverMap.Map.ContainsKey(bindType));
            var resolver = resolverMap.Map[bindType];
            Assert.IsAssignableFrom<SingletonResolver>(resolver);
            Assert.AreEqual(resolveType, ((SingletonResolver)resolver).Target);
        }
        
        [Test]
        public void AddToValue_TypeAddedWithValueResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var type = typeof(MockTarget);
            var value = new MockTarget();

            new TypeBinder(injector, resolverMap, type).ToValue(value);
            
            Assert.True(resolverMap.Map.ContainsKey(type));
            var resolver = resolverMap.Map[type];
            Assert.IsAssignableFrom<ValueResolver>(resolver);
            Assert.AreSame(injector.InjectedInstance, value);
        }
        
        [Test]
        public void AddAsType_TypeAddedWithTypeResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var type = typeof(MockTarget);
            
            new TypeBinder(injector, resolverMap, type).AsType();
            
            Assert.True(resolverMap.Map.ContainsKey(type));
            var resolver = resolverMap.Map[type];
            Assert.IsAssignableFrom<TypeResolver>(resolver);
            Assert.AreEqual(type, ((TypeResolver)resolver).Target);
        }
        
        [Test]
        public void AddToTypeGeneric_TypeAddedWithTypeResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var bindType = typeof(IMockTarget);
            var resolveType = typeof(MockTarget);
            
            new TypeBinder(injector, resolverMap, bindType).ToType<MockTarget>();
            
            Assert.True(resolverMap.Map.ContainsKey(bindType));
            var resolver = resolverMap.Map[bindType];
            Assert.IsAssignableFrom<TypeResolver>(resolver);
            Assert.AreEqual(resolveType, ((TypeResolver)resolver).Target);
        }
        
        [Test]
        public void AddToType_TypeAddedWithTypeResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var bindType = typeof(IMockTarget);
            var resolveType = typeof(MockTarget);
            
            new TypeBinder(injector, resolverMap, bindType).ToType(resolveType);
            
            Assert.True(resolverMap.Map.ContainsKey(bindType));
            var resolver = resolverMap.Map[bindType];
            Assert.IsAssignableFrom<TypeResolver>(resolver);
            Assert.AreEqual(resolveType, ((TypeResolver)resolver).Target);
        }
        
        [Test]
        public void AddToMethod_TypeAddedWithFactoryResolver()
        {
            var injector = new MockInjector();
            var resolverMap = new MockResolverMap();
            var type = typeof(IMockTarget);
            var factoryCalled = false;

            object Factory()
            {
                factoryCalled = true;
                return new MockTarget();
            }

            new TypeBinder(injector, resolverMap, type).ToMethod(Factory);
            
            Assert.True(resolverMap.Map.ContainsKey(type));
            var resolver = resolverMap.Map[type];
            Assert.IsAssignableFrom<FactoryResolver>(resolver);
            resolver.Resolve();
            Assert.True(factoryCalled);
        }
        
        private interface IMockTarget { }
        
        private class MockTarget : IMockTarget {}
        
        private class MockInjector : IInjector
        {
            public object InjectedInstance { get; private set; }

            public void Inject(object target)
            {
                InjectedInstance = target;
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

            public object Create(Type type)
            {
                throw new NotImplementedException();
            }
        }

        private class MockResolverMap : IResolverMap
        {
            public readonly Dictionary<Type, IResolver> Map = new Dictionary<Type, IResolver>();
        
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                throw new NotImplementedException();
            }

            public void Set(Type type, IResolver resolver)
            {
                Map[type] = resolver;
            }

            public bool Remove(Type type)
            {
                throw new NotImplementedException();
            }

            public bool Contains(Type type)
            {
                throw new NotImplementedException();
            }
        }
    }
}