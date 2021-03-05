namespace Foxes.Core
{
    using System;

    public interface IInjector : IDisposable
    {
        T Get<T>();

        object Get(Type type);
        
        T Get<T>(params object[] arguments);
        
        object Get(Type type, params object[] arguments);

        ITypeBinder Bind<T>();

        ITypeBinder Bind(Type type);

        void Unbind<T>();

        void Unbind(Type type);

        bool IsBound<T>();

        bool IsBound(Type type);
        
        void Inject(object target);
    }
}