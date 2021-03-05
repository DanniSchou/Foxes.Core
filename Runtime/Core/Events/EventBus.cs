namespace Foxes.Core.Events
{
    using System;
    using System.Collections.Generic;
    using Core;
    using JetBrains.Annotations;

    [PublicAPI]
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, HashSet<Delegate>> _eventSubscriptions;

        public EventBus()
        {
            _eventSubscriptions = new Dictionary<Type, HashSet<Delegate>>();
        }
        
        public void Publish<T>(T eventData) where T : struct, IEvent
        {
            var type = eventData.GetType();
            if (!_eventSubscriptions.TryGetValue(type, out var delegates))
            {
                return;
            }

            foreach (var action in delegates)
            {
                action.DynamicInvoke(eventData);
            }
        }

        public void Subscribe<T>(Action<T> action) where T : struct, IEvent
        {
            var type = typeof(T);
            if (_eventSubscriptions.TryGetValue(type, out var delegates))
            {
                delegates.Add(action);
            }
            else
            {
                delegates = new HashSet<Delegate> { action };
                _eventSubscriptions.Add(type, delegates);
            }
        }

        public void Unsubscribe<T>(Action<T> action) where T : struct, IEvent
        {
            var type = action.GetType();
            if (!_eventSubscriptions.TryGetValue(type, out var delegates))
            {
                return;
            }

            delegates.Remove(action);
        }

        public void Dispose()
        {
            foreach (var subscription in _eventSubscriptions)
            {
                subscription.Value.Clear();
            }
            
            _eventSubscriptions.Clear();
        }
    }
}