namespace Foxes.Core.Injection.Resolvers
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
            if (type == null) throw new ArgumentNullException(nameof(type));
            
            if (!_resolverMap.TryGetValue(type, out var resolver))
            {
                throw new ArgumentException($"{nameof(ResolverMap)} doesn't have a resolver for {type.FullName}.");
            }

            return resolver.Resolve();
        }

        public void Set(Type type, IResolver resolver)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            if (resolver == null) throw new ArgumentNullException(nameof(resolver));
            
            if (_resolverMap.ContainsKey(type))
            {
                throw new ArgumentException($"{nameof(ResolverMap)} already has resolver for {type.FullName}.");
            }

            _resolverMap.Add(type, resolver);
        }

        public bool Remove(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
            return _resolverMap.Remove(type);
        }

        public bool Contains(Type type)
        {
            if (type == null) throw new ArgumentNullException(nameof(type));
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
