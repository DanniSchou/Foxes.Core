namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public class SingletonResolver : IResolver, IDisposable
    {
        public Type Target { get; }
        
        private readonly IInjector _injector;
        
        private object _value;

        public SingletonResolver(IInjector injector, Type target)
        {
            _injector = injector;
            Target = target;
        }

        public object Resolve()
        {
            if (_value != null)
            {
                return _value;
            }
            
            return _value = _injector.Create(Target);;
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
