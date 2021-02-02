namespace Foxes.Commands
{
    using System;
    using Events;

    public interface ICommandMap : IDisposable
    {
        void Map<T, TK>() where T : struct, IEvent where TK : ICommand<T>;
        
        void UnMap<T, TK>() where T : struct, IEvent where TK : ICommand<T>;
        
        bool HasMapping<T, TK>() where T : struct, IEvent where TK : ICommand<T>;
    }
}