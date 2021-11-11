namespace Foxes.Core.Commands
{
    using System;
    using System.Collections.Generic;
    using JetBrains.Annotations;

    [UsedImplicitly]
    public class CommandMap : ICommandMap
    {
        private readonly IInjector _injector;
        private readonly Dictionary<Type, ICommandGroup> _commandMap;

        public CommandMap(IInjector injector)
        {
            _injector = injector;
            
            _commandMap = new Dictionary<Type, ICommandGroup>();
        }
        
        public void Map<T, TK>() where TK : ICommand<T>
        {
            var eventType = typeof(T);
            if (!_commandMap.TryGetValue(eventType, out var group))
            {
                group = (ICommandGroup)_injector.Create(typeof(CommandGroup<T>));
                _commandMap.Add(eventType, group);
            }

            group.Add<TK>();
        }

        public bool UnMap<T, TK>() where TK : ICommand<T>
        {
            var eventType = typeof(T);
            return _commandMap.TryGetValue(eventType, out var group) && group.Remove<TK>();
        }

        public bool HasMapping<T, TK>() where TK : ICommand<T>
        {
            var eventType = typeof(T);
            return _commandMap.TryGetValue(eventType, out var group) && group.Contains<TK>();
        }

        public void Dispose()
        {
            foreach (var pair in _commandMap)
            {
                pair.Value.Dispose();
            }
            
            _commandMap.Clear();
        }
    }
}