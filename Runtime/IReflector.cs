namespace Foxes.Core
{
    using System;
    using System.Reflection;
    using JetBrains.Annotations;

    public interface IReflector : IDisposable
    {
        /// <summary>
        /// Get ConstructorInfo for the constructor with the inject attribute,
        /// or the first constructor if none has the attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [NotNull] ConstructorInfo GetConstructorInfo(Type type);
        
        /// <summary>
        /// Get FieldInfo for fields with the inject attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [NotNull] FieldInfo[] GetFieldInfos(Type type);

        /// <summary>
        /// Get PropertyInfo for properties with the inject attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [NotNull] PropertyInfo[] GetPropertyInfos(Type type);

        /// <summary>
        /// Get MethodInfo for methods with the inject attribute
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        [NotNull] MethodInfo[] GetMethodInfos(Type type);
    }
}
