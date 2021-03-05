namespace Foxes.Core
{
    using System;

    public interface IEventBus : IDisposable
    {
        void Publish<T>(T eventData) where T : struct, IEvent;

        void Subscribe<T>(Action<T> action) where T : struct, IEvent;
        
        void Unsubscribe<T>(Action<T> action) where T : struct, IEvent;
    }
}