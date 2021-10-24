namespace Foxes.Core.Injection.Resolvers
{
    using System;
    using Core;

    public class SingletonResolver : IResolver, IDisposable
    {
        private readonly IInjector _injector;
        private readonly Type _target;
        
        private object _value;

        public SingletonResolver(IInjector injector, Type target)
        {
            _injector = injector;
            _target = target;
        }

        public object Resolve()
        {
            if (_value != null)
            {
                return _value;
            }
            
            return _value = _injector.Create(_target);;
        }

        public void Dispose()
        {
            if (_value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
