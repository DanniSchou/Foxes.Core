namespace Foxes.Core.Injection
{
    using System;
    using System.Reflection;
    using NUnit.Framework;
    
    public class ReflectorTests
    {
        private readonly Type _injectAttributeType = typeof(InjectAttribute);
        
        [Test]
        public void GetConstructorInfoFromClassWithNoDeclaredConstructor()
        {
            var reflector = new Reflector(_injectAttributeType);
            var constructorInfo = reflector.GetConstructorInfo(typeof(ClassWithoutConstructor));
            Assert.NotNull(constructorInfo);
        }

        private class ClassWithoutConstructor {}

        [Test]
        public void GetConstructorInfoFromClassWithDeclaredConstructor()
        {
            var reflector = new Reflector(_injectAttributeType);
            var constructorInfo = reflector.GetConstructorInfo(typeof(ClassWithConstructor));
            Assert.NotNull(constructorInfo);
        }

        private class ClassWithConstructor
        {
            private ClassWithConstructor() {}
        }

        [Test]
        public void GetFirstConstructorInfoFromClassWithMultipleConstructors()
        {
            var reflector = new Reflector(_injectAttributeType);
            var constructorInfo = reflector.GetConstructorInfo(typeof(ClassWithMultipleConstructors));
            Assert.NotNull(constructorInfo);
        }

        private class ClassWithMultipleConstructors
        {
            private ClassWithMultipleConstructors() {}
            private ClassWithMultipleConstructors(int arg) {}
        }

        [Test]
        public void GetMarkedConstructorInfoFromClassWithMultipleConstructors()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedConstructor);
            var constructorInfo = reflector.GetConstructorInfo(type);
            Assert.NotNull(constructorInfo);
            Assert.True(constructorInfo.IsDefined(_injectAttributeType));
        }

        private class ClassWithMarkedConstructor
        {
            private ClassWithMarkedConstructor() {}
            [Inject] private ClassWithMarkedConstructor(int arg) {}
        }
        
        [Test]
        public void GetFieldInfosNoMarkedFields()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithoutMarkedFields);
            var fieldInfos = reflector.GetFieldInfos(type);
            Assert.NotNull(fieldInfos);
            Assert.True(fieldInfos.Length == 0);
        }

        private class ClassWithoutMarkedFields
        {
            private int _foo;
            private string _bar;
        }
        
        [Test]
        public void GetFieldInfosOneMarkedField()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedField);
            var fieldInfos = reflector.GetFieldInfos(type);
            Assert.NotNull(fieldInfos);
            Assert.True(fieldInfos.Length == 1);
        }
        
        private class ClassWithMarkedField
        {
            [Inject] private int _foo;
            private string _bar;
        }
        
        [Test]
        public void GetFieldInfosTwoMarkedFields()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedFields);
            var fieldInfos = reflector.GetFieldInfos(type);
            Assert.NotNull(fieldInfos);
            Assert.True(fieldInfos.Length == 2);
        }
        
        private class ClassWithMarkedFields
        {
            [Inject] private int _foo;
            [Inject] private string _bar;
        }
        
        [Test]
        public void GetPropertyInfosNoMarkedProperties()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithoutMarkedProperty);
            var propertyInfos = reflector.GetPropertyInfos(type);
            Assert.NotNull(propertyInfos);
            Assert.True(propertyInfos.Length == 0);
        }

        private class ClassWithoutMarkedProperty
        {
            private int _foo { get; set; }
            private string _bar { get; }
        }
        
        [Test]
        public void GetPropertyInfosOneMarkedProperty()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedProperty);
            var propertyInfos = reflector.GetPropertyInfos(type);
            Assert.NotNull(propertyInfos);
            Assert.True(propertyInfos.Length == 1);
        }
        
        private class ClassWithMarkedProperty
        {
            [Inject] private int _foo { get; set; }
            private string _bar { get; }
        }
        
        [Test]
        public void GetPropertyInfosMarkedPropertyWithoutSetter_ThrowsNotSupportedException()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedPropertyWithoutSetter);

            Assert.Throws<NotSupportedException>(() => reflector.GetPropertyInfos(type));
        }
        
        private class ClassWithMarkedPropertyWithoutSetter
        {
            private int _foo { get; set; }
            [Inject] private string _bar { get; }
        }
        
        [Test]
        public void GetPropertyInfosTwoMarkedProperties()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedProperties);
            var propertyInfos = reflector.GetPropertyInfos(type);
            Assert.NotNull(propertyInfos);
            Assert.True(propertyInfos.Length == 2);
        }
        
        private class ClassWithMarkedProperties
        {
            [Inject] private int _foo { get; set; }
            [Inject] private string _bar { get; set; }
        }
        
        [Test]
        public void GetMethodInfosNoMarkedMethods()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithoutMarkedMethod);
            var methodInfos = reflector.GetMethodInfos(type);
            Assert.NotNull(methodInfos);
            Assert.True(methodInfos.Length == 0);
        }

        private class ClassWithoutMarkedMethod
        {
            private void Foo(int value){}
            private void Bar(string value){}
        }
        
        [Test]
        public void GetMethodInfosOneMarkedMethod()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedMethod);
            var methodInfos = reflector.GetMethodInfos(type);
            Assert.NotNull(methodInfos);
            Assert.True(methodInfos.Length == 1);
        }
        
        private class ClassWithMarkedMethod
        {
            [Inject] private void Foo(int value){}
            private void Bar(string value){}
        }
        
        [Test]
        public void GetMethodInfosTwoMarkedMethods()
        {
            var reflector = new Reflector(_injectAttributeType);
            var type = typeof(ClassWithMarkedMethods);
            var methodInfos = reflector.GetMethodInfos(type);
            Assert.NotNull(methodInfos);
            Assert.True(methodInfos.Length == 2);
        }
        
        private class ClassWithMarkedMethods
        {
            [Inject] private void Foo(int value){}
            [Inject] private void Bar(string value){}
        }
    }
}
