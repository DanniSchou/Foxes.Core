namespace Foxes.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using NUnit.Framework;

    public class CommandMapTests
    {
        [Test]
        public void UnMap_BeforeMap_ReturnsFalse()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            var wasUnMapped = commandMap.UnMap<MockEvent1, MockCommand1A>();
            
            Assert.False(wasUnMapped);
        }
        
        [Test]
        public void UnMap_AfterMap_ReturnsTrue()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            commandMap.Map<MockEvent1, MockCommand1A>();
            var wasUnMapped = commandMap.UnMap<MockEvent1, MockCommand1A>();
            
            Assert.True(wasUnMapped);
        }
        
        [Test]
        public void UnMap_AfterUnMap_ReturnsTrue()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            commandMap.Map<MockEvent1, MockCommand1A>();
            commandMap.UnMap<MockEvent1, MockCommand1A>();
            var wasUnMapped = commandMap.UnMap<MockEvent1, MockCommand1A>();
            
            Assert.False(wasUnMapped);
        }
        
        [Test]
        public void HasMapping_BeforeMap_ReturnsFalse()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            var hasMapping = commandMap.HasMapping<MockEvent1, MockCommand1A>();
            
            Assert.False(hasMapping);
        }
        
        [Test]
        public void HasMapping_AfterMap_ReturnsTrue()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            commandMap.Map<MockEvent1, MockCommand1A>();
            var hasMapping = commandMap.HasMapping<MockEvent1, MockCommand1A>();
            
            Assert.True(hasMapping);
        }
        
        [Test]
        public void HasMapping_AfterUnMap_ReturnsTrue()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            commandMap.Map<MockEvent1, MockCommand1A>();
            commandMap.UnMap<MockEvent1, MockCommand1A>();
            var hasMapping = commandMap.HasMapping<MockEvent1, MockCommand1A>();
            
            Assert.False(hasMapping);
        }
        
        [Test]
        public void HasMapping_For2CommandsWithSameEvent_ReturnsTrue()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            commandMap.Map<MockEvent1, MockCommand1A>();
            commandMap.Map<MockEvent1, MockCommand1B>();
            var hasMapping1A = commandMap.HasMapping<MockEvent1, MockCommand1A>();
            var hasMapping1B = commandMap.HasMapping<MockEvent1, MockCommand1B>();
            
            Assert.True(hasMapping1A);
            Assert.True(hasMapping1B);
        }
        
        [Test]
        public void HasMapping_For2CommandsWithDifferentEvent_ReturnsTrue()
        {
            var injector = new MockInjector();
            var commandMap = new CommandMap(injector);

            commandMap.Map<MockEvent1, MockCommand1A>();
            commandMap.Map<MockEvent2, MockCommand2>();
            var hasMapping1 = commandMap.HasMapping<MockEvent1, MockCommand1A>();
            var hasMapping2 = commandMap.HasMapping<MockEvent2, MockCommand2>();
            
            Assert.True(hasMapping1);
            Assert.True(hasMapping2);
        }
        
        private struct MockEvent1 {}
        private struct MockEvent2 {}
        
        private class MockCommand1A : ICommand<MockEvent1>
        {
            public void Execute(MockEvent1 eventData)
            {
                throw new NotImplementedException();
            }
        }

        private class MockCommand1B : ICommand<MockEvent1>
        {
            public void Execute(MockEvent1 eventData)
            {
                throw new NotImplementedException();
            }
        }

        private class MockCommand2 : ICommand<MockEvent2>
        {
            public void Execute(MockEvent2 eventData)
            {
                throw new NotImplementedException();
            }
        }

        private class MockCommandGroup : ICommandGroup
        {
            public readonly HashSet<Type> Types = new HashSet<Type>();

            public void Add<TK>()
            {
                Types.Add(typeof(TK));
            }

            public bool Remove<TK>()
            {
                return Types.Remove(typeof(TK));
            }

            public bool Contains<TK>()
            {
                return Types.Contains(typeof(TK));
            }

            public void Dispose() {}
        }

        private class MockInjector : IInjector
        {
            public MockCommandGroup LastCreatedGroup;
            
            public T Create<T>()
            {
                return (T)Create(typeof(T));
            }

            public object Create(Type type)
            {
                return LastCreatedGroup = new MockCommandGroup();
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public T Get<T>()
            {
                throw new NotImplementedException();
            }

            public object Get(Type type)
            {
                throw new NotImplementedException();
            }

            public ITypeBinder Bind<T>()
            {
                throw new NotImplementedException();
            }

            public ITypeBinder Bind(Type type)
            {
                throw new NotImplementedException();
            }

            public bool Unbind<T>()
            {
                throw new NotImplementedException();
            }

            public bool Unbind(Type type)
            {
                throw new NotImplementedException();
            }

            public bool IsBound<T>()
            {
                throw new NotImplementedException();
            }

            public bool IsBound(Type type)
            {
                throw new NotImplementedException();
            }

            public void Inject(object target)
            {
                throw new NotImplementedException();
            }
        }
    }
}