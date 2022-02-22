namespace Foxes.Core.Events
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    [PublicAPI]
    public class EventBus : IEventBus
    {
        private readonly Dictionary<Type, HashSet<Delegate>> _eventSubscriptions;

        public EventBus()
        {
            _eventSubscriptions = new Dictionary<Type, HashSet<Delegate>>();
        }
        
        public void Publish(object eventData)
        {
            var type = eventData.GetType();
            if (!_eventSubscriptions.TryGetValue(type, out var delegates))
            {
                return;
            }

            var delegateArray = delegates.ToArray();
            foreach (var action in delegateArray)
            {
                action.DynamicInvoke(eventData);
            }
        }

        public void Subscribe<T>(Action<T> action)
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

        public bool Unsubscribe<T>(Action<T> action)
        {
            var type = typeof(T);
            return _eventSubscriptions.TryGetValue(type, out var delegates) && delegates.Remove(action);
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