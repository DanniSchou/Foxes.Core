namespace Foxes.Core.Commands
{
    using System;
    using NUnit.Framework;

    public class CommandGroupTests
    {
        [Test]
        public void Add_TypeNotCommand_ThrowArgumentException()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            Assert.Throws<ArgumentException>(() => commandGroup.Add<NonCommand>());
        }
        
        [Test]
        public void Add_SubscriptionAdded()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            
            Assert.NotNull(eventBus.Action);
        }
        
        [Test]
        public void AddAndRemove_SubscriptionRemoved()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            commandGroup.Remove<MockCommand>();
            
            Assert.Null(eventBus.Action);
        }
        
        [Test]
        public void AddSameTwiceAndRemove_SubscriptionRemoved()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            commandGroup.Add<MockCommand>();
            commandGroup.Remove<MockCommand>();
            
            Assert.Null(eventBus.Action);
        }
        
        [Test]
        public void AddTwoDifferentAndRemove_SubscriptionRemains()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            commandGroup.Add<SecondMockCommand>();
            commandGroup.Remove<MockCommand>();
            
            Assert.NotNull(eventBus.Action);
        }
        
        [Test]
        public void Add_NoPublish_ZeroCommandsCreated()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            
            Assert.AreEqual(0, injector.CommandsCreated);
        }
        
        [Test]
        public void Add_WithPublish_OneCommandCreatedAndExecuted()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            MockCommand.ExecuteCount = 0;
            
            commandGroup.Add<MockCommand>();
            
            var action = (Action<MockEvent>)eventBus.Action;
            action.Invoke(new MockEvent());
            
            Assert.AreEqual(1, injector.CommandsCreated);
            Assert.AreEqual(1, MockCommand.ExecuteCount);
        }
        
        [Test]
        public void Add_WithPublishTwice_OneCommandCreatedAndTwiceExecuted()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            MockCommand.ExecuteCount = 0;
            
            commandGroup.Add<MockCommand>();
            
            var action = (Action<MockEvent>)eventBus.Action;
            action.Invoke(new MockEvent());
            action.Invoke(new MockEvent());
            
            Assert.AreEqual(1, injector.CommandsCreated);
            Assert.AreEqual(2, MockCommand.ExecuteCount);
        }
        
        [Test]
        public void Add_TwoDifferentWithPublish_OneCommandCreatedAndExecuted()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            MockCommand.ExecuteCount = 0;
            SecondMockCommand.ExecuteCount = 0;
            
            commandGroup.Add<MockCommand>();
            commandGroup.Add<SecondMockCommand>();
            
            var action = (Action<MockEvent>)eventBus.Action;
            action.Invoke(new MockEvent());
            
            Assert.AreEqual(2, injector.CommandsCreated);
            Assert.AreEqual(1, MockCommand.ExecuteCount);
            Assert.AreEqual(1, SecondMockCommand.ExecuteCount);
        }
        
        [Test]
        public void Add_TwoDifferentWithPublishTwice_OneCommandCreatedAndTwiceExecuted()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            MockCommand.ExecuteCount = 0;
            SecondMockCommand.ExecuteCount = 0;
            
            commandGroup.Add<MockCommand>();
            commandGroup.Add<SecondMockCommand>();
            
            var action = (Action<MockEvent>)eventBus.Action;
            action.Invoke(new MockEvent());
            action.Invoke(new MockEvent());
            
            Assert.AreEqual(2, injector.CommandsCreated);
            Assert.AreEqual(2, MockCommand.ExecuteCount);
            Assert.AreEqual(2, SecondMockCommand.ExecuteCount);
        }
        
        [Test]
        public void Add_TwoDifferentRemoveOneWithPublish_OneCommandCreatedAndExecuted()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            MockCommand.ExecuteCount = 0;
            SecondMockCommand.ExecuteCount = 0;
            
            commandGroup.Add<MockCommand>();
            commandGroup.Add<SecondMockCommand>();
            commandGroup.Remove<MockCommand>();
            
            var action = (Action<MockEvent>)eventBus.Action;
            action.Invoke(new MockEvent());
            
            Assert.AreEqual(1, injector.CommandsCreated);
            Assert.AreEqual(0, MockCommand.ExecuteCount);
            Assert.AreEqual(1, SecondMockCommand.ExecuteCount);
        }
        
        [Test]
        public void Add_WithPublishTrackedEvent_CommandExecutedWithTrackedEvent()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockTrackEvent>(injector, eventBus);
            MockTrackCommand.LastEvent = null;
            
            commandGroup.Add<MockTrackCommand>();

            var trackedEvent = new MockTrackEvent();
            var action = (Action<MockTrackEvent>)eventBus.Action;
            action.Invoke(trackedEvent);
            
            Assert.AreSame(trackedEvent, MockTrackCommand.LastEvent);
        }
        
        [Test]
        public void Remove_BeforeAdd_ReturnsFalse()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            var removed = commandGroup.Remove<MockCommand>();

            Assert.False(removed);
        }
        
        [Test]
        public void Remove_AfterAdd_ReturnsTrue()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            var removed = commandGroup.Remove<MockCommand>();

            Assert.True(removed);
        }
        
        [Test]
        public void Remove_AfterRemove_ReturnsFalse()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            var firstRemove = commandGroup.Remove<MockCommand>();
            var secondRemove = commandGroup.Remove<MockCommand>();

            Assert.True(firstRemove);
            Assert.False(secondRemove);
        }
        
        [Test]
        public void Contains_BeforeAdd_ReturnsFalse()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            var contains = commandGroup.Contains<MockCommand>();

            Assert.False(contains);
        }
        
        [Test]
        public void Contains_AfterAdd_ReturnsTrue()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            var contains = commandGroup.Contains<MockCommand>();

            Assert.True(contains);
        }
        
        [Test]
        public void Contains_AfterRemove_ReturnsFalse()
        {
            var injector = new MockInjector();
            var eventBus = new MockEventBus();
            var commandGroup = new CommandGroup<MockEvent>(injector, eventBus);
            
            commandGroup.Add<MockCommand>();
            commandGroup.Remove<MockCommand>();
            var contains = commandGroup.Contains<MockCommand>();

            Assert.False(contains);
        }
        
        private struct MockEvent {}
        
        private class MockCommand : ICommand<MockEvent>
        {
            public static int ExecuteCount;
            
            public void Execute(MockEvent eventData)
            {
                ExecuteCount++;
            }
        }
        
        private class SecondMockCommand : ICommand<MockEvent>
        {
            public static int ExecuteCount;
            
            public void Execute(MockEvent eventData)
            {
                ExecuteCount++;
            }
        }
        
        private class MockTrackEvent {}
        
        private class MockTrackCommand : ICommand<MockTrackEvent>
        {
            public static MockTrackEvent LastEvent;
            
            public void Execute(MockTrackEvent eventData)
            {
                LastEvent = eventData;
            }
        }

        private class NonCommand {}
        
        private class MockInjector : IInjector
        {
            public int CommandsCreated { get; private set; }
            
            public object Create(Type type)
            {
                CommandsCreated++;
                return type.Name switch
                {
                    nameof(MockCommand) => new MockCommand(),
                    nameof(SecondMockCommand) => new SecondMockCommand(),
                    nameof(MockTrackCommand) => new MockTrackCommand(),
                    _ => null
                };
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

            public T Create<T>()
            {
                throw new NotImplementedException();
            }
        }
        
        private class MockEventBus : IEventBus
        {
            public object Action;
            
            public void Subscribe<T>(Action<T> action)
            {
                Action = action;
            }

            public bool Unsubscribe<T>(Action<T> action)
            {
                if ((Action<T>)Action == action)
                {
                    Action = null;
                    return true;
                }
                
                return false;
            }

            public void Dispose()
            {
                throw new NotImplementedException();
            }

            public void Publish<T>(T eventData)
            {
                throw new NotImplementedException();
            }
        }
    }
}