namespace Foxes.Injection.Resolvers
{
    using System;
    using Core;

    public class FactoryResolver : IResolver
    {
        [Inject]
        protected IInjector Injector;

        private readonly Func<object> _factory;

        public FactoryResolver(Func<object> factory)
        {
            _factory = factory;
        }

        public object Resolve()
        {
            var result = _factory();
            Injector.Inject(result);
            return result;
        }

        public object Resolve(params object[] arguments)
        {
            return Resolve();
        }
    }
}
