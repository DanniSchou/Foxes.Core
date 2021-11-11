namespace Foxes.Core.Injection
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;
    using NUnit.Framework;
    using Resolvers;

    public class InjectorTests
    {
        [Test]
        public void GetTypeGeneric_ReturnsMappedType()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var value = new MockTarget();
            var type = typeof(MockTarget);
            resolverMap.Map[type] = value;

            var instance = injector.Get<MockTarget>();
            
            Assert.AreSame(value, instance);
        }

        [Test]
        public void GetType_ReturnsMappedType()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var value = new MockTarget();
            var type = typeof(MockTarget);
            resolverMap.Map[type] = value;

            var instance = injector.Get(type);
            
            Assert.AreSame(value, instance);
        }
        
        [Test]
        public void BindTypeGeneric_ReturnsBinderForMap()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var typeBinder = injector.Bind<MockTarget>();
            
            Assert.NotNull(typeBinder);
            typeBinder.AsSingle();
            Assert.True(resolverMap.Map.ContainsKey(typeof(MockTarget)));
        }
        
        [Test]
        public void BindType_ReturnsBinderForMap()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var type = typeof(MockTarget);
            var typeBinder = injector.Bind(type);
            
            Assert.NotNull(typeBinder);
            typeBinder.AsSingle();
            Assert.True(resolverMap.Map.ContainsKey(type));
        }
        
        [Test]
        public void UnbindTypeGeneric_WithNoBinding_ReturnsFalse()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var wasUnbound = injector.Unbind<MockTarget>();
            
            Assert.False(wasUnbound);
        }
        
        [Test]
        public void UnbindType_WithNoBinding_ReturnsFalse()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var type = typeof(MockTarget);
            var wasUnbound = injector.Unbind(type);
            
            Assert.False(wasUnbound);
        }
        
        [Test]
        public void UnbindTypeGeneric_WithBinding_ReturnsTrue()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(MockTarget)] = new MockTarget();
            var wasUnbound = injector.Unbind<MockTarget>();
            
            Assert.True(wasUnbound);
        }
        
        [Test]
        public void UnbindType_WithBinding_ReturnsTrue()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var type = typeof(MockTarget);
            resolverMap.Map[type] = new MockTarget();
            var wasUnbound = injector.Unbind(type);
            
            Assert.True(wasUnbound);
        }
        
        [Test]
        public void IsBoundGeneric_WithNoBinding_ReturnsFalse()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var wasUnbound = injector.IsBound<MockTarget>();
            
            Assert.False(wasUnbound);
        }
        
        [Test]
        public void IsBound_WithNoBinding_ReturnsFalse()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var type = typeof(MockTarget);
            var wasUnbound = injector.IsBound(type);
            
            Assert.False(wasUnbound);
        }
        
        [Test]
        public void IsBoundGeneric_WithBinding_ReturnsTrue()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(MockTarget)] = new MockTarget();
            var wasUnbound = injector.IsBound<MockTarget>();
            
            Assert.True(wasUnbound);
        }
        
        [Test]
        public void IsBound_WithBinding_ReturnsTrue()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            var type = typeof(MockTarget);
            resolverMap.Map[type] = new MockTarget();
            var wasUnbound = injector.IsBound(type);
            
            Assert.True(wasUnbound);
        }

        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void Inject_WithFields_ValuesInjected(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;

            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.FieldInfos = new[]
            {
                type.GetField("IntField", bindingFlags),
                type.GetField("FloatField", bindingFlags),
                type.GetField("_doubleField", bindingFlags)
            };
            
            var instance = new MockTarget();
            injector.Inject(instance);
            
            Assert.AreEqual(intValue, instance.IntField);
            Assert.AreEqual(floatValue, instance.FloatFieldValue);
            Assert.AreEqual(doubleValue, instance.DoubleFieldValue);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void Inject_WithProperties_ValuesInjected(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;

            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.PropertyInfos = new[]
            {
                type.GetProperty("IntProperty", bindingFlags),
                type.GetProperty("FloatProperty", bindingFlags),
                type.GetProperty("DoubleProperty", bindingFlags)
            };
            
            var instance = new MockTarget();
            injector.Inject(instance);
            
            Assert.AreEqual(intValue, instance.IntProperty);
            Assert.AreEqual(floatValue, instance.FloatProperty);
            Assert.AreEqual(doubleValue, instance.DoubleProperty);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void Inject_WithMethods_ValuesInjected(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;

            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.MethodInfos = new[]
            {
                type.GetMethod("IntAndFloatInjection", bindingFlags),
                type.GetMethod("DoubleInjection", bindingFlags)
            };
            
            var instance = new MockTarget();
            injector.Inject(instance);
            
            Assert.AreEqual(intValue, instance.IntFromMethod);
            Assert.AreEqual(floatValue, instance.FloatFromMethod);
            Assert.AreEqual(doubleValue, instance.DoubleFromMethod);
        }

        [Test]
        public void CreateTypeGeneric_InstanceCreated()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            reflector.ConstructorInfo = type.GetConstructors()[0];

            var instance = injector.Create<MockTarget>();
            
            Assert.NotNull(instance);
            Assert.True(instance.Created);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void CreateTypeGeneric_WithFields_InstanceCreated(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;
            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.ConstructorInfo = type.GetConstructors()[0];
            reflector.FieldInfos = new[]
            {
                type.GetField("IntField", bindingFlags),
                type.GetField("FloatField", bindingFlags),
                type.GetField("_doubleField", bindingFlags)
            };

            var instance = injector.Create<MockTarget>();

            Assert.NotNull(instance);
            Assert.True(instance.Created);
            Assert.AreEqual(intValue, instance.IntField);
            Assert.AreEqual(floatValue, instance.FloatFieldValue);
            Assert.AreEqual(doubleValue, instance.DoubleFieldValue);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void CreateTypeGeneric_WithProperties_InstanceCreated(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;
            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.ConstructorInfo = type.GetConstructors()[0];
            reflector.PropertyInfos = new[]
            {
                type.GetProperty("IntProperty", bindingFlags),
                type.GetProperty("FloatProperty", bindingFlags),
                type.GetProperty("DoubleProperty", bindingFlags)
            };

            var instance = injector.Create<MockTarget>();

            Assert.NotNull(instance);
            Assert.True(instance.Created);
            Assert.AreEqual(intValue, instance.IntProperty);
            Assert.AreEqual(floatValue, instance.FloatProperty);
            Assert.AreEqual(doubleValue, instance.DoubleProperty);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void CreateTypeGeneric_WithMethods_InstanceCreated(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;
            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.ConstructorInfo = type.GetConstructors()[0];
            reflector.MethodInfos = new[]
            {
                type.GetMethod("IntAndFloatInjection", bindingFlags),
                type.GetMethod("DoubleInjection", bindingFlags)
            };

            var instance = injector.Create<MockTarget>();

            Assert.NotNull(instance);
            Assert.True(instance.Created);
            Assert.AreEqual(intValue, instance.IntFromMethod);
            Assert.AreEqual(floatValue, instance.FloatFromMethod);
            Assert.AreEqual(doubleValue, instance.DoubleFromMethod);
        }
        
        [Test]
        public void CreateType_InstanceCreated()
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            reflector.ConstructorInfo = type.GetConstructors()[0];

            var instance = injector.Create(type);
            
            Assert.NotNull(instance);
            Assert.IsAssignableFrom<MockTarget>(instance);
            var target = (MockTarget)instance;
            Assert.True(target.Created);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void CreateType_WithFields_InstanceCreated(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;
            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.ConstructorInfo = type.GetConstructors()[0];
            reflector.FieldInfos = new[]
            {
                type.GetField("IntField", bindingFlags),
                type.GetField("FloatField", bindingFlags),
                type.GetField("_doubleField", bindingFlags)
            };

            var instance = injector.Create(type);

            Assert.NotNull(instance);
            Assert.IsAssignableFrom<MockTarget>(instance);
            var target = (MockTarget)instance;
            Assert.True(target.Created);
            Assert.AreEqual(intValue, target.IntField);
            Assert.AreEqual(floatValue, target.FloatFieldValue);
            Assert.AreEqual(doubleValue, target.DoubleFieldValue);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void CreateType_WithProperties_InstanceCreated(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;
            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.ConstructorInfo = type.GetConstructors()[0];
            reflector.PropertyInfos = new[]
            {
                type.GetProperty("IntProperty", bindingFlags),
                type.GetProperty("FloatProperty", bindingFlags),
                type.GetProperty("DoubleProperty", bindingFlags)
            };

            var instance = injector.Create(type);

            Assert.NotNull(instance);
            Assert.IsAssignableFrom<MockTarget>(instance);
            var target = (MockTarget)instance;
            Assert.True(target.Created);
            Assert.AreEqual(intValue, target.IntProperty);
            Assert.AreEqual(floatValue, target.FloatProperty);
            Assert.AreEqual(doubleValue, target.DoubleProperty);
        }
        
        [TestCase(5, 3.5f, .25)]
        [TestCase(-25, .5f, 20)]
        public void CreateType_WithMethods_InstanceCreated(int intValue, float floatValue, double doubleValue)
        {
            var reflector = new MockReflector();
            var resolverMap = new MockResolverMap();
            var injector = new Injector(reflector, resolverMap);

            resolverMap.Map[typeof(int)] = intValue;
            resolverMap.Map[typeof(float)] = floatValue;
            resolverMap.Map[typeof(double)] = doubleValue;
            resolverMap.Map[typeof(bool)] = true;
            
            var type = typeof(MockTarget);
            const BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance;
            reflector.ConstructorInfo = type.GetConstructors()[0];
            reflector.MethodInfos = new[]
            {
                type.GetMethod("IntAndFloatInjection", bindingFlags),
                type.GetMethod("DoubleInjection", bindingFlags)
            };

            var instance = injector.Create(type);

            Assert.NotNull(instance);
            Assert.IsAssignableFrom<MockTarget>(instance);
            var target = (MockTarget)instance;
            Assert.True(target.Created);
            Assert.AreEqual(intValue, target.IntFromMethod);
            Assert.AreEqual(floatValue, target.FloatFromMethod);
            Assert.AreEqual(doubleValue, target.DoubleFromMethod);
        }

        private class MockTarget
        {
            public int IntField;
            protected float FloatField;
            private double _doubleField;

            public float FloatFieldValue => FloatField;
            public double DoubleFieldValue => _doubleField;
            
            public int IntProperty { get; set; }
            public float FloatProperty { get; protected set; }
            public double DoubleProperty { get; private set; }

            public int IntFromMethod;
            public float FloatFromMethod;
            public double DoubleFromMethod;
            
            public bool Created { get; }

            public MockTarget(bool created)
            {
                Created = created;
            }
            
            public MockTarget() {}

            private void IntAndFloatInjection(int intValue, float floatValue)
            {
                IntFromMethod = intValue;
                FloatFromMethod = floatValue;
            }
            
            protected void DoubleInjection(double doubleValue) => DoubleFromMethod = doubleValue;
        }

        private class MockReflector : IReflector
        {
            public ConstructorInfo ConstructorInfo;
            public FieldInfo[] FieldInfos = Array.Empty<FieldInfo>();
            public PropertyInfo[] PropertyInfos = Array.Empty<PropertyInfo>();
            public MethodInfo[] MethodInfos = Array.Empty<MethodInfo>();
            
            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public ConstructorInfo GetConstructorInfo(Type type)
            {
                return ConstructorInfo;
            }

            public FieldInfo[] GetFieldInfos(Type type)
            {
                return FieldInfos;
            }

            public PropertyInfo[] GetPropertyInfos(Type type)
            {
                return PropertyInfos;
            }

            public MethodInfo[] GetMethodInfos(Type type)
            {
                return MethodInfos;
            }
        }
        
        private class MockResolverMap : IResolverMap
        {
            public Dictionary<Type, object> Map { get; } = new Dictionary<Type, object>();

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                return Map[type];
            }

            public void Set(Type type, IResolver resolver)
            {
                Map[type] = resolver;
            }

            public bool Remove(Type type)
            {
                return Map.Remove(type);
            }

            public bool Contains(Type type)
            {
                return Map.ContainsKey(type);
            }
        }
    }
}