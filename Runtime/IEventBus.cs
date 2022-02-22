namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [PublicAPI]
    public interface IEventBus : IDisposable
    {
        /// <summary>
        /// Publish event, all subscribed handlers will be invoked with the given event.
        /// </summary>
        /// <param name="eventData">Event</param>
        void Publish(object eventData);

        /// <summary>
        /// Subscribes action to be invoked when event type is published.
        /// </summary>
        /// <param name="action">Action</param>
        /// <typeparam name="T">Type</typeparam>
        void Subscribe<T>(Action<T> action);
        
        /// <summary>
        /// Unsubscribes action to no longer be invoked when event type is published.
        /// </summary>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns>true if the action was removed.</returns>
        bool Unsubscribe<T>(Action<T> action);
    }
}