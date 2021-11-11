namespace Foxes.Core.Injection.Resolvers
{
    using System;
    using NUnit.Framework;


    public class FactoryResolverTests
    {
        [Test]
        public void Resolve_InstanceInjectedAndReturned()
        {
            var injector = new MockInjector();
            var factoryInstance = new object();
            var factoryResolver = new FactoryResolver(injector, () => factoryInstance);

            var resolvedInstance = factoryResolver.Resolve();
            Assert.AreSame(factoryInstance, resolvedInstance);
            Assert.AreSame(injector.InjectedInstance, resolvedInstance);
        }

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
    }
}