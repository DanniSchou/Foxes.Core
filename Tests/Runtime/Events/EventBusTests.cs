namespace Foxes.Core.Events
{
    using System;
    using NUnit.Framework;

    public class EventBusTests
    {
        [Test]
        public void Subscribe_PublishEvent_EventInvokesAction()
        {
            var eventBus = new EventBus();

            var actionInvoked = false;
            Action<MockEvent> action = eventData => actionInvoked = true;
            eventBus.Subscribe(action);
            
            eventBus.Publish(new MockEvent());
            
            Assert.True(actionInvoked);
        }
        
        [Test]
        public void SubscribeAndUnsubscribe_PublishEvent_EventDoesntInvokeAction()
        {
            var eventBus = new EventBus();

            var actionInvoked = false;
            Action<MockEvent> action = eventData => actionInvoked = true;
            eventBus.Subscribe(action);
            eventBus.Unsubscribe(action);
            
            eventBus.Publish(new MockEvent());
            
            Assert.False(actionInvoked);
        }
        
        [Test]
        public void Subscribe_PublishDifferentEvent_EventDoesntInvokeAction()
        {
            var eventBus = new EventBus();

            var actionInvoked = false;
            Action<MockEvent> action = eventData => actionInvoked = true;
            eventBus.Subscribe(action);
            
            eventBus.Publish(new MockWrongEvent());
            
            Assert.False(actionInvoked);
        }
        
        [Test]
        public void TwoDifferentSubscribes_PublishEvent_EventInvokesOneAction()
        {
            var eventBus = new EventBus();

            var rightActionInvoked = false;
            var wrongActionInvoked = false;
            Action<MockEvent> rightAction = eventData => rightActionInvoked = true;
            Action<MockWrongEvent> wrongAction = eventData => wrongActionInvoked = true;
            
            eventBus.Subscribe(rightAction);
            eventBus.Subscribe(wrongAction);
            
            eventBus.Publish(new MockEvent());
            
            Assert.True(rightActionInvoked);
            Assert.False(wrongActionInvoked);
        }
        
        [Test]
        public void TwoSameSubscribes_PublishEvent_EventInvokesBothActions()
        {
            var eventBus = new EventBus();

            var firstActionInvoked = false;
            var secondActionInvoked = false;
            Action<MockEvent> firstAction = eventData => firstActionInvoked = true;
            Action<MockEvent> secondAction = eventData => secondActionInvoked = true;
            
            eventBus.Subscribe(firstAction);
            eventBus.Subscribe(secondAction);
            
            eventBus.Publish(new MockEvent());
            
            Assert.True(firstActionInvoked);
            Assert.True(secondActionInvoked);
        }
        
        [Test]
        public void Unsubscribe_WithoutSubscription_ReturnsFalse()
        {
            var eventBus = new EventBus();

            Action<MockEvent> action = eventData => { };
            var unsubscribed = eventBus.Unsubscribe(action);
            
            Assert.False(unsubscribed);
        }
        
        [Test]
        public void Unsubscribe_WithSubscription_ReturnsFalse()
        {
            var eventBus = new EventBus();

            Action<MockEvent> action = eventData => { };
            eventBus.Subscribe(action);
            var unsubscribed = eventBus.Unsubscribe(action);
            
            Assert.True(unsubscribed);
        }
        
        private struct MockEvent {}
        private struct MockWrongEvent {}
    }
}