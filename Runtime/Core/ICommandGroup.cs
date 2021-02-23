namespace Foxes.Core
{
    public interface ICommandGroup
    {
        void Add<TK>();
        
        void Remove<TK>();
        
        bool Contains<TK>();
    }
}