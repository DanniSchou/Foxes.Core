namespace Foxes.Core
{
    using System;

    public interface IHasConfigDependencies
    {
        Type[] GetDependencies();
    }
}