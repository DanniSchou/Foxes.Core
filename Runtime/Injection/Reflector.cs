namespace Foxes.Injection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using Core;

    public class Reflector<T> : IReflector
    {
        private readonly Type _injectAttributeType;
        private readonly Dictionary<Type, FieldInfo[]> _cachedFieldInfos;
        private readonly List<FieldInfo> _tempFieldInfos;

        public Reflector()
        {
            _injectAttributeType = typeof(T);
            _cachedFieldInfos = new Dictionary<Type, FieldInfo[]>();
            _tempFieldInfos = new List<FieldInfo>();
        }

        public FieldInfo[] GetFieldInfos(Type type)
        {
            if (_cachedFieldInfos.TryGetValue(type, out var fieldInfos))
            {
                return fieldInfos;
            }

            var fields = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            var fieldsLength = fields.Length;
            for (var i = 0; i < fieldsLength; i++)
            {
                var field = fields[i];
                var isInjectField = field.IsDefined(_injectAttributeType, false);
                if (isInjectField)
                {
                    _tempFieldInfos.Add(field);
                }
            }

            fieldInfos = _tempFieldInfos.ToArray();
            _cachedFieldInfos.Add(type, fieldInfos);
            _tempFieldInfos.Clear();

            return fieldInfos;
        }

        public void Dispose()
        {
            _cachedFieldInfos.Clear();
            _tempFieldInfos.Clear();
        }
    }
}
