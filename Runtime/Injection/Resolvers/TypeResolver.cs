namespace Foxes.Injection.Resolvers
{
    using System;

    public class TypeResolver : IResolver
    {
        [Inject]
        protected IInjector Injector;

        private readonly Type _target;

        public TypeResolver(Type target)
        {
            _target = target;
        }

        public object Resolve()
        {
            var instance = Activator.CreateInstance(_target);
            Injector.Inject(instance);
            return instance;
        }
    }
}
