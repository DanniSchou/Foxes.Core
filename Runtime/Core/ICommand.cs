﻿namespace Foxes.Core
{
    using JetBrains.Annotations;

    [PublicAPI]
    public interface ICommand<in T> where T : IEvent
    {
        public void Execute(T eventData);
    }
}