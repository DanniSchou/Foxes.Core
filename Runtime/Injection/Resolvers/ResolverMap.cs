namespace Foxes.Injection.Resolvers
{
    using System;
    using System.Collections.Generic;

    public class ResolverMap : IResolverMap
    {
        private readonly Dictionary<Type, IResolver> _resolverMap;

        public ResolverMap()
        {
            _resolverMap = new Dictionary<Type, IResolver>();
        }

        public object Get(Type type)
        {
            if (!_resolverMap.TryGetValue(type, out var resolver))
            {
                throw new Exception($"{nameof(ResolverMap)} doesn't have a resolver for {type.FullName}.");
            }

            return resolver.Resolve();
        }

        public void Set(Type type, IResolver resolver)
        {
            if (_resolverMap.ContainsKey(type))
            {
                throw new Exception($"{nameof(ResolverMap)} already has resolver for {type.FullName}.");
            }

            _resolverMap.Add(type, resolver);
        }

        public void Remove(Type type)
        {
            _resolverMap.Remove(type);
        }

        public bool Contains(Type type)
        {
            return _resolverMap.ContainsKey(type);
        }

        public void Dispose()
        {
            foreach (var resolver in _resolverMap.Values)
            {
                if (resolver is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            _resolverMap.Clear();
        }
    }
}
