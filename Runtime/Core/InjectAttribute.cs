namespace Foxes.Core
{
    using System;
    using JetBrains.Annotations;

    [AttributeUsage(AttributeTargets.Field)]
    [MeansImplicitUse(ImplicitUseKindFlags.Assign)]
    public class InjectAttribute : Attribute
    {
    }
}