namespace Foxes.Injection
{
    using System;

    public interface ITypeBinder
    {
        void AsSingleton();
        
        void ToSingleton<T>() where T : new();
        
        void ToValue(object value);
        
        void AsType();
        
        void ToType<T>();

        void ToMethod(Func<object> factory);
    }
}
