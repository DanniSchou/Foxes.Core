namespace Foxes.Injection
{
    using System;

    public interface IInjector : IDisposable
    {
        T Get<T>();

        object Get(Type type);

        ITypeBinder Bind<T>();

        ITypeBinder Bind(Type type);

        void Unbind<T>();

        void Unbind(Type type);

        bool IsBound<T>();

        bool IsBound(Type type);
        
        void Inject(object target);
    }
}