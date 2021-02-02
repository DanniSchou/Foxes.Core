namespace Foxes.Commands
{
    public interface ICommandGroup
    {
        void Add<TK>();
        
        void Remove<TK>();
        
        bool Contains<TK>();
    }
}