namespace Foxes.Injection.Resolvers
{
    using System;

    public interface IResolverMap : IDisposable
    {
        object Get(Type type);
        
        void Set(Type type, IResolver resolver);

        void Remove(Type type);

        bool Contains(Type type);
    }
}
