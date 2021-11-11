namespace Foxes.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class CommandGroup<T> : ICommandGroup
    {
        private readonly IInjector _injector;
        private readonly IEventBus _eventBus;
        private readonly List<Pair> _commandPairs;

        public CommandGroup(IInjector injector, IEventBus eventBus)
        {
            _injector = injector;
            _eventBus = eventBus;

            _commandPairs = new List<Pair>();
        }

        public void Add<TK>()
        {
            var type = typeof(TK);
            if (!typeof(ICommand<T>).IsAssignableFrom(type))
            {
                throw new ArgumentException($"{type.FullName} is being mapped as a command but does not implement {nameof(ICommand<T>)}");
            }

            if (Contains<TK>())
            {
                return;
            }

            _commandPairs.Add(new Pair(type));

            if (_commandPairs.Count == 1)
            {
                _eventBus.Subscribe<T>(OnEvent);
            }
        }

        public bool Remove<TK>()
        {
            var type = typeof(TK);
            var pair = _commandPairs.FirstOrDefault(p => p.Type == type);
            if (pair == null)
            {
                return false;
            }
            
            _commandPairs.Remove(pair);
            if (pair.Command is IDisposable disposable)
            {
                disposable.Dispose();
            }
            
            if (_commandPairs.Count == 0)
            {
                _eventBus.Unsubscribe<T>(OnEvent);
            }

            return true;
        }

        public bool Contains<TK>()
        {
            var type = typeof(TK);
            return _commandPairs.Any(pair => pair.Type == type);
        }

        public void Dispose()
        {
            foreach (var pair in _commandPairs)
            {
                if (pair.Command is IDisposable disposable)
                {
                    disposable.Dispose();
                }
            }
            
            _commandPairs.Clear();
        }

        private void OnEvent(T eventData)
        {
            foreach (var pair in _commandPairs)
            {
                pair.Command ??= (ICommand<T>)_injector.Create(pair.Type);
                pair.Command.Execute(eventData);
            }
        }

        private class Pair
        {
            public Type Type { get; }

            public ICommand<T> Command { get; set; }

            public Pair(Type type)
            {
                Type = type;
            }
        }
    }
}