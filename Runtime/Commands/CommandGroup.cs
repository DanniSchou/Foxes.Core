namespace Foxes.Commands
{
    using System;
    using System.Collections.Generic;
    using Events;
    using Injection;

    public class CommandGroup<T> : ICommandGroup where T : struct, IEvent
    {
        [Inject] protected IInjector Injector;
        [Inject] protected IEventBus EventBus; 
        
        private readonly HashSet<Type> _commandTypes;

        public CommandGroup()
        {
            _commandTypes = new HashSet<Type>();
        }

        public void Add<TK>()
        {
            var type = typeof(TK);
            if (!typeof(ICommand<T>).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.FullName} is being mapped as a command but does not implement {nameof(ICommand<T>)}");
            }
            
            _commandTypes.Add(type);
            
            Injector.Bind<TK>().AsSingleton();

            if (_commandTypes.Count == 1)
            {
                EventBus.Subscribe<T>(OnEvent);
            }
        }

        public void Remove<TK>()
        {
            var type = typeof(TK);
            _commandTypes.Remove(type);

            if (_commandTypes.Count == 0)
            {
                EventBus.Unsubscribe<T>(OnEvent);
            }
        }

        public bool Contains<TK>()
        {
            return _commandTypes.Contains(typeof(TK));
        }

        private void OnEvent(T eventData)
        {
            foreach (var type in _commandTypes)
            {
                var command = (ICommand<T>) Injector.Get(type);
                command.Execute(eventData);
            }
        }
    }
}