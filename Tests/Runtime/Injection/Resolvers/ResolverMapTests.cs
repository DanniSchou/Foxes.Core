namespace Foxes.Core.Injection.Resolvers
{
    using System;
    using NUnit.Framework;

    public class ResolverMapTests
    {
        [Test]
        public void GetNullType_ThrowsArgumentNullException()
        {
            var resolverMap = new ResolverMap();
            
            Assert.Throws<ArgumentNullException>(() => resolverMap.Get(null));
        }
        
        [Test]
        public void GetTypeWithNoResolver_ThrowsArgumentException()
        {
            var resolverMap = new ResolverMap();
            var type = typeof(object);
            
            Assert.Throws<ArgumentException>(() => resolverMap.Get(type));
        }
        
        [Test]
        public void SetAndGetTypeWithResolver_ReturnsInstance()
        {
            var resolverMap = new ResolverMap();
            var testValue = new object();
            var testResolver = new MockResolver(testValue);
            var type = typeof(MockResolver);
            
            resolverMap.Set(type, testResolver);
            var resolvedInstance = resolverMap.Get(type);
            
            Assert.AreEqual(testValue, resolvedInstance);
        }

        [Test]
        public void SetWithNullType_ThrowsArgumentNullException()
        {
            var resolverMap = new ResolverMap();
            var testResolver = new MockResolver(new object());
            
            Assert.Throws<ArgumentNullException>(() => resolverMap.Set(null, testResolver));
        }

        [Test]
        public void SetWithNullResolver_ThrowsArgumentNullException()
        {
            var resolverMap = new ResolverMap();
            var type = typeof(object);
            
            Assert.Throws<ArgumentNullException>(() => resolverMap.Set(type, null));
        }

        [Test]
        public void SetTypeWithAlreadyMappedType_ThrowsArgumentException()
        {
            var resolverMap = new ResolverMap();
            var testResolver = new MockResolver(new object());
            var type = typeof(object);
            
            resolverMap.Set(type, testResolver);
            Assert.Throws<ArgumentException>(() => resolverMap.Set(type, testResolver));
        }

        [Test]
        public void RemoveNull_ThrowsArgumentNullException()
        {
            var resolverMap = new ResolverMap();
            
            Assert.Throws<ArgumentNullException>(() => resolverMap.Remove(null));
        }

        [Test]
        public void RemoveTypeWithNoMappedType_ReturnsFalse()
        {
            var resolverMap = new ResolverMap();
            var type = typeof(object);
            
            var removed = resolverMap.Remove(type);
            Assert.False(removed);
        }

        [Test]
        public void RemoveTypeWithMappedType_ReturnsTrue()
        {
            var resolverMap = new ResolverMap();
            var testResolver = new MockResolver(new object());
            var type = typeof(object);
            
            resolverMap.Set(type, testResolver);
            var removed = resolverMap.Remove(type);
            Assert.True(removed);
        }
        
        [Test]
        public void ContainsNull_ThrowsArgumentNullException()
        {
            var resolverMap = new ResolverMap();
            
            Assert.Throws<ArgumentNullException>(() => resolverMap.Contains(null));
        }

        [Test]
        public void ContainsTypeWithNoMappedType_ReturnsFalse()
        {
            var resolverMap = new ResolverMap();
            var type = typeof(object);
            
            var contains = resolverMap.Contains(type);
            Assert.False(contains);
        }

        [Test]
        public void ContainsTypeWithMappedType_ReturnsTrue()
        {
            var resolverMap = new ResolverMap();
            var testResolver = new MockResolver(new object());
            var type = typeof(object);
            
            resolverMap.Set(type, testResolver);
            var contains = resolverMap.Contains(type);
            Assert.True(contains);
        }

        private class MockResolver : IResolver
        {
            private readonly object _instance;

            public MockResolver(object value)
            {
                _instance = value;
                
            }
            
            public object Resolve() => _instance;
        }
    }
}