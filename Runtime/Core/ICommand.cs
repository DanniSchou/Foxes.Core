namespace Foxes.Core
{
    using JetBrains.Annotations;

    [PublicAPI]
    public interface ICommand<in T>
    {
        /// <summary>
        /// Executes command with event data
        /// </summary>
        /// <param name="eventData">Event data.</param>
        public void Execute(T eventData);
    }
}