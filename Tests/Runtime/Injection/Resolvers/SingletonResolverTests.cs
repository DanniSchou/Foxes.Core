namespace Foxes.Core.Injection.Resolvers
{
    using System;
    using NUnit.Framework;

    public class SingletonResolverTests
    {
        [Test]
        public void Resolve_ReturnsInjectorCreatedInstance()
        {
            var type = typeof(MockTarget);
            var injector = new MockInjector();
            var resolver = new SingletonResolver(injector, type);

            var instance = resolver.Resolve();
            
            Assert.NotNull(instance);
            Assert.IsAssignableFrom<MockTarget>(instance);
            Assert.AreEqual(1, injector.CreateCalled);
        }
        
        [Test]
        public void SecondResolve_ReturnsAlreadyCreatedInstance()
        {
            var type = typeof(MockTarget);
            var injector = new MockInjector();
            var resolver = new SingletonResolver(injector, type);

            var firstInstance = resolver.Resolve();
            var secondInstance = resolver.Resolve();
            
            Assert.AreSame(firstInstance, secondInstance);
            Assert.AreEqual(1, injector.CreateCalled);
        }

        private class MockTarget
        {
        }
        
        private class MockInjector : IInjector
        {
            public int CreateCalled { get; private set; }
            
            public object Create(Type type)
            {
                CreateCalled++;
                return new MockTarget();
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

            public void Inject(object target)
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