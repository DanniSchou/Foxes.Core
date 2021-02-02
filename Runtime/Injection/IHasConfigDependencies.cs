namespace Foxes.Injection
{
    using System;

    public interface IHasConfigDependencies
    {
        Type[] GetDependencies();
    }
}