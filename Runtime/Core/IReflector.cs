﻿namespace Foxes.Core
{
    using System;
    using System.Reflection;

    public interface IReflector : IDisposable
    {
        FieldInfo[] GetFieldInfos(Type type);
    }
}
