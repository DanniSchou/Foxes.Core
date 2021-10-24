namespace Foxes.Core.Injection.Resolvers
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
            AddResolver(new SingletonResolver(_injector, _target));
        }

        public void ToSingle<T>() where T : new()
        {
            AddResolver(new SingletonResolver(_injector, typeof(T)));
        }

        public void ToValue(object value)
        {
            _injector.Inject(value);
            AddResolver(new ValueResolver(value));
        }

        public void AsType()
        {
            AddResolver(new TypeResolver(_injector, _target));
        }

        public void ToType<T>()
        {
            AddResolver(new TypeResolver(_injector, typeof(T)));
        }

        public void ToMethod(Func<object> factory)
        {
            AddResolver(new FactoryResolver(_injector, factory));
        }

        private void AddResolver(IResolver resolver)
        {
            _resolverMap.Set(_target, resolver);
        }
    }
}
