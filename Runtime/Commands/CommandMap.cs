namespace Foxes.Commands
{
    using System;
    using System.Collections.Generic;
    using Events;
    using Injection;
    using JetBrains.Annotations;

    [PublicAPI]
    public class CommandMap : ICommandMap
    {
        [Inject]
        protected IInjector Injector;
        
        private readonly Dictionary<Type, ICommandGroup> _commandMap;

        public CommandMap()
        {
            _commandMap = new Dictionary<Type, ICommandGroup>();
        }
        
        public void Map<T, TK>() where T : struct, IEvent where TK : ICommand<T>
        {
            var eventType = typeof(T);
            if (!_commandMap.TryGetValue(eventType, out var group))
            {
                group = new CommandGroup<T>();
                Injector.Inject(group);
                _commandMap.Add(eventType, group);
            }

            group.Add<TK>();
        }

        public void UnMap<T, TK>() where T : struct, IEvent where TK : ICommand<T>
        {
            var eventType = typeof(T);
            if (_commandMap.TryGetValue(eventType, out var group))
            {
                group.Remove<TK>();
            }
        }

        public bool HasMapping<T, TK>() where T : struct, IEvent where TK : ICommand<T>
        {
            var eventType = typeof(T);
            if (_commandMap.TryGetValue(eventType, out var group))
            {
                return group.Contains<TK>();
            }

            return false;
        }

        public void Dispose()
        {
            _commandMap.Clear();
        }
    }
}