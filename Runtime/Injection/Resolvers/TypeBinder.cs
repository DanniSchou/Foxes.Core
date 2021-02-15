namespace Foxes.Injection.Resolvers
{
    using System;
    using Core;

    public readonly struct TypeBinder : ITypeBinder
    {
        private readonly IInjector _injector;
        private readonly IResolverMap _resolverMap;
        private readonly Type _target;

        public TypeBinder(IInjector injector, IResolverMap resolverMap, Type target)
        {
            _injector = injector;
            _resolverMap = resolverMap;
            _target = target;
        }

        public void AsSingle()
        {
            var resolver = new SingletonResolver(_target);
            AddResolver(resolver);
        }

        public void ToSingle<T>() where T : new()
        {
            var resolver = new SingletonResolver(typeof(T));
            AddResolver(resolver);
        }

        public void ToValue(object value)
        {
            _injector.Inject(value);
            var resolver = new ValueResolver(value);
            AddResolver(resolver);
        }

        public void AsType()
        {
            var resolver = new TypeResolver(_target);
            AddResolver(resolver);
        }

        public void ToType<T>()
        {
            var resolver = new TypeResolver(typeof(T));
            AddResolver(resolver);
        }

        public void ToMethod(Func<object> factory)
        {
            var resolver = new FactoryResolver(factory);
            AddResolver(resolver);
        }

        private void AddResolver(IResolver resolver)
        {
            _injector.Inject(resolver);
            _resolverMap.Set(_target, resolver);
        }
    }
}
