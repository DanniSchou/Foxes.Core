namespace Foxes.Core.Injection
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
            _reflector = new Reflector(typeof(InjectAttribute));
            _resolverMap = new ResolverMap();
        }

        public T Get<T>()
        {
            return (T)Get(typeof(T));
        }

        public object Get(Type type)
        {
            return _resolverMap.Get(type);
        }

        public ITypeBinder Bind<T>()
        {
            return Bind(typeof(T));
        }

        public ITypeBinder Bind(Type type)
        {
            return new TypeBinder(this, _resolverMap, type);
        }

        public bool Unbind<T>()
        {
            return Unbind(typeof(T));
        }

        public bool Unbind(Type type)
        {
            return _resolverMap.Remove(type);
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
            InjectFields(target);
            InjectProperties(target);
            InjectMethods(target);
        }

        private void InjectFields(object target)
        {
            var fieldInfos = _reflector.GetFieldInfos(target.GetType());
            foreach (var fieldInfo in fieldInfos)
            {
                var value = Get(fieldInfo.FieldType);
                fieldInfo.SetValue(target, value);
            }
        }

        private void InjectProperties(object target)
        {
            var propertyInfos = _reflector.GetPropertyInfos(target.GetType());
            foreach (var propertyInfo in propertyInfos)
            {
                var value = Get(propertyInfo.PropertyType);
                propertyInfo.SetValue(target, value);
            }
        }

        private void InjectMethods(object target)
        {
            var methodInfos = _reflector.GetMethodInfos(target.GetType());
            foreach (var methodInfo in methodInfos)
            {
                var parameters = methodInfo.GetParameters();
                var parameterCount = parameters.Length;
                var arguments = new object[parameterCount];
                for (var i = 0; i < parameterCount; i++)
                {
                    arguments[i] = Get(parameters[i].ParameterType);
                }
                
                methodInfo.Invoke(target, arguments);
            }
        }

        public T Create<T>()
        {
            return (T)Create(typeof(T));
        }

        public object Create(Type type)
        {
            var constructorInfo = _reflector.GetConstructorInfo(type);
            var parameters = constructorInfo.GetParameters();
            var parameterCount = parameters.Length;
            var arguments = new object[parameterCount];
            for (var i = 0; i < parameterCount; i++)
            {
                arguments[i] = Get(parameters[i].ParameterType);
            }
            
            var instance = constructorInfo.Invoke(arguments);
            Inject(instance);
            return instance;
        }

        public void Dispose()
        {
            _reflector.Dispose();
            _resolverMap.Dispose();
        }
    }
}