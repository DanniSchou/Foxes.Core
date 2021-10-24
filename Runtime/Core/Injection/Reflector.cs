namespace Foxes.Core.Injection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Core;

    public class Reflector : IReflector
    {
        private const BindingFlags PublicAndPrivateFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
        
        private readonly Type _injectAttributeType;
        
        private readonly Dictionary<Type, ConstructorInfo> _cachedConstructorInfo;
        private readonly Dictionary<Type, FieldInfo[]> _cachedFieldInfos;
        private readonly Dictionary<Type, PropertyInfo[]> _cachedPropertyInfos;
        private readonly Dictionary<Type, MethodInfo[]> _cachedMethodInfos;
        
        private readonly List<FieldInfo> _tempFieldInfos;
        private readonly List<PropertyInfo> _tempPropertyInfos;
        private readonly List<MethodInfo> _tempMethodInfos;

        public Reflector(Type injectAttributeType)
        {
            _injectAttributeType = injectAttributeType;

            _cachedConstructorInfo = new Dictionary<Type, ConstructorInfo>();
            _cachedFieldInfos = new Dictionary<Type, FieldInfo[]>();
            _cachedPropertyInfos = new Dictionary<Type, PropertyInfo[]>();
            _cachedMethodInfos = new Dictionary<Type, MethodInfo[]>();

            _tempFieldInfos = new List<FieldInfo>();
            _tempPropertyInfos = new List<PropertyInfo>();
            _tempMethodInfos = new List<MethodInfo>();
        }

        public ConstructorInfo GetConstructorInfo(Type type)
        {
            if (_cachedConstructorInfo.TryGetValue(type, out var constructorInfo))
            {
                return constructorInfo;
            }

            var constructors = type.GetConstructors(PublicAndPrivateFlags);
            constructorInfo = constructors[0];
            
            foreach (var constructor in constructors)
            {
                if (constructor.IsDefined(_injectAttributeType, false))
                {
                    constructorInfo = constructor;
                    break;
                }
            }

            _cachedConstructorInfo[type] = constructorInfo;
            return constructorInfo;
        }

        public FieldInfo[] GetFieldInfos(Type type)
        {
            if (_cachedFieldInfos.TryGetValue(type, out var fieldInfos))
            {
                return fieldInfos;
            }

            var fields = type.GetFields(PublicAndPrivateFlags);
            if (fields.Length > 0)
            {
                foreach (var field in fields)
                {
                    if (field.IsDefined(_injectAttributeType, false))
                    {
                        _tempFieldInfos.Add(field);
                    }
                }

                fieldInfos = _tempFieldInfos.ToArray();
                _tempFieldInfos.Clear();
            }
            else
            {
                fieldInfos = Array.Empty<FieldInfo>();
            }

            _cachedFieldInfos[type] = fieldInfos;
            return fieldInfos;
        }

        public PropertyInfo[] GetPropertyInfos(Type type)
        {
            if (_cachedPropertyInfos.TryGetValue(type, out var propertyInfos))
            {
                return propertyInfos;
            }

            var properties = type.GetProperties(PublicAndPrivateFlags);
            if (properties.Length > 0)
            {
                foreach (var property in properties)
                {
                    if (property.IsDefined(_injectAttributeType, false))
                    {
                        _tempPropertyInfos.Add(property);
                    }
                }

                propertyInfos = _tempPropertyInfos.ToArray();
                _tempPropertyInfos.Clear();
            }
            else
            {
                propertyInfos = Array.Empty<PropertyInfo>();
            }

            _cachedPropertyInfos[type] = propertyInfos;
            return propertyInfos;
        }

        public MethodInfo[] GetMethodInfos(Type type)
        {
            if (_cachedMethodInfos.TryGetValue(type, out var methodInfos))
            {
                return methodInfos;
            }

            var methods = type.GetMethods(PublicAndPrivateFlags);
            if (methods.Length > 0)
            {
                foreach (var method in methods)
                {
                    if (method.IsDefined(_injectAttributeType, false))
                    {
                        _tempMethodInfos.Add(method);
                    }
                }

                methodInfos = _tempMethodInfos.ToArray();
                _tempMethodInfos.Clear();
            }
            else
            {
                methodInfos = Array.Empty<MethodInfo>();
            }

            _cachedMethodInfos[type] = methodInfos;
            return methodInfos;
        }

        public void Dispose()
        {
            _cachedConstructorInfo.Clear();
            _cachedFieldInfos.Clear();
            _cachedPropertyInfos.Clear();
            _cachedMethodInfos.Clear();
            
            _tempFieldInfos.Clear();
            _tempPropertyInfos.Clear();
            _tempMethodInfos.Clear();
        }
    }
}
