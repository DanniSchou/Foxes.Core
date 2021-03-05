namespace Foxes.Core
{
    using System;

    public interface ITypeBinder
    {
        void AsSingle();
        
        void ToSingle<T>() where T : new();
        
        void ToValue(object value);
        
        void AsType();
        
        void ToType<T>();

        void ToMethod(Func<object> factory);
    }
}
