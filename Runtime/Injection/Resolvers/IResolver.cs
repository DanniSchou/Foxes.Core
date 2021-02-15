namespace Foxes.Injection.Resolvers
{
    public interface IResolver
    {
        object Resolve();
        
        object Resolve(params object[] arguments);
    }
}
