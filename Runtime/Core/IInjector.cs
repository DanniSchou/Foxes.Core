namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface IInjector : IDisposable
    {
        T Get<T>();

        object Get(Type type);

        ITypeBinder Bind<T>();

        ITypeBinder Bind(Type type);

        bool Unbind<T>();

        bool Unbind(Type type);

        bool IsBound<T>();

        bool IsBound(Type type);
        
        void Inject(object target);

        T Create<T>();

        object Create(Type type);
    }
}