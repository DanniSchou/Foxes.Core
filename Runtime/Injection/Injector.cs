namespace Foxes.Injection
{
    using System;
    using Core;
    using Resolvers;

    public class Injector : IInjector
    {
        private readonly IReflector _reflector;
        private readonly IResolverMap _resolverMap;

        public Injector()
        {
            _reflector = new Reflector<InjectAttribute>();
            _resolverMap = new ResolverMap();
        }

        public ITypeBinder Bind<T>()
        {
            return Bind(typeof(T));
        }

        public ITypeBinder Bind(Type type)
        {
            return new TypeBinder(this, _resolverMap, type);
        }

        public void Unbind<T>()
        {
            Unbind(typeof(T));
        }

        public void Unbind(Type type)
        {
            _resolverMap.Remove(type);
        }

        public bool IsBound<T>()
        {
            return IsBound(typeof(T));
        }

        public bool IsBound(Type type)
        {
            return _resolverMap.Contains(type);
        }

        public void Inject(object target)
        {
            var fieldInfos = _reflector.GetFieldInfos(target.GetType());
            
            var length = fieldInfos.Length;
            for (var i = 0; i < length; i++)
            {
                var fieldInfo = fieldInfos[i];
                var value = _resolverMap.Get(fieldInfo.FieldType);
                fieldInfo.SetValue(target, value);
            }
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            return _resolverMap.Get(type);
        }

        public T Get<T>(params object[] arguments)
        {
            return (T) Get(typeof(T), arguments);
        }

        public object Get(Type type, params object[] arguments)
        {
            return _resolverMap.Get(type, arguments);
        }

        public void Dispose()
        {
            _reflector.Dispose();
            _resolverMap.Dispose();
        }
    }
}