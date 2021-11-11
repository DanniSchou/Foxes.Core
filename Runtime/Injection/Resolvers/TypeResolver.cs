namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public class TypeResolver : IResolver
    {
        public Type Target { get; }
        
        private readonly IInjector _injector;

        public TypeResolver(IInjector injector, Type target)
        {
            _injector = injector;
            Target = target;
        }

        public object Resolve()
        {
            return _injector.Create(Target);
        }
    }
}
