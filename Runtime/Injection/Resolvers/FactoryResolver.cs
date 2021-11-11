namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public class FactoryResolver : IResolver
    {
        private readonly IInjector _injector;
        private readonly Func<object> _factory;

        public FactoryResolver(IInjector injector, Func<object> factory)
        {
            _injector = injector;
            _factory = factory;
        }

        public object Resolve()
        {
            var result = _factory();
            _injector.Inject(result);
            return result;
        }
    }
}
