namespace Foxes.Injection.Resolvers
{
    using System;

    public class SingletonResolver : IResolver, IDisposable
    {
        [Inject]
        protected IInjector Injector;

        private readonly Type _target;
        private object _value;

        public SingletonResolver(Type target)
        {
            _target = target;
        }

        public object Resolve()
        {
            if (_value != null)
            {
                return _value;
            }
            
            _value = Activator.CreateInstance(_target);
            Injector.Inject(_value);

            return _value;
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
