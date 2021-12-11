namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public readonly struct TypeBinder : ITypeBinder, IEquatable<TypeBinder>
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

        public void ToSingle<T>()
        {
            ToSingle(typeof(T));
        }
        
        public void ToSingle(Type type)
        {
            AddResolver(new SingletonResolver(_injector, type));
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
            ToType(typeof(T));
        }
        
        public void ToType(Type type)
        {
            AddResolver(new TypeResolver(_injector, type));
        }

        public void ToMethod(Func<object> factory)
        {
            AddResolver(new FactoryResolver(_injector, factory));
        }

        private void AddResolver(IResolver resolver)
        {
            _resolverMap.Set(_target, resolver);
        }

        public bool Equals(TypeBinder other)
        {
            return _target == other._target;
        }

        public override bool Equals(object obj)
        {
            return obj is TypeBinder other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(_injector, _resolverMap, _target);
        }
    }
}
