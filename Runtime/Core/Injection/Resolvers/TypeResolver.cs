namespace Foxes.Core.Injection.Resolvers
{
    using System;
    using Core;

    public class TypeResolver : IResolver
    {
        private readonly IInjector _injector;
        private readonly Type _target;

        public TypeResolver(IInjector injector, Type target)
        {
            _injector = injector;
            _target = target;
        }

        public object Resolve()
        {
            return _injector.Create(_target);
        }
    }
}
