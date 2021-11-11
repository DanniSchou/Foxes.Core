namespace Foxes.Core.Injection.Resolvers
{
    public interface IResolver
    {
        /// <summary>
        /// Resolve instance.
        /// </summary>
        /// <returns>Resolved instance.</returns>
        object Resolve();
    }
}
