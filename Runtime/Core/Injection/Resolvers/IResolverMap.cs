namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public interface IResolverMap : IDisposable
    {
        object Get(Type type);
        
        void Set(Type type, IResolver resolver);

        bool Remove(Type type);

        bool Contains(Type type);
    }
}
