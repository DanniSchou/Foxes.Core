namespace Foxes.Core
{
    using System;
    using System.Reflection;

    public interface IReflector : IDisposable
    {
        ConstructorInfo GetConstructorInfo(Type type);
        
        FieldInfo[] GetFieldInfos(Type type);

        PropertyInfo[] GetPropertyInfos(Type type);

        MethodInfo[] GetMethodInfos(Type type);
    }
}
