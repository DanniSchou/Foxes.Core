namespace Foxes.Core
{
    using JetBrains.Annotations;

    [PublicAPI]
    public interface IHasConfigValidation : IConfig
    {
        /// <summary>
        /// Check if configuration is valid.
        /// </summary>
        /// <returns>true if valid.</returns>
        bool IsValid();
    }
}