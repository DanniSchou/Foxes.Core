namespace Foxes.Injection.Resolvers
{
    using System;

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
    }
}
