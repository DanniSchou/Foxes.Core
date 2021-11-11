namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Constructor | AttributeTargets.Method)]
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    public class InjectAttribute : Attribute
    {
    }
}