﻿namespace Foxes.Core.Events
{
    using Core;
    using JetBrains.Annotations;

    [PublicAPI]
    public class EventBusConfig : IConfig
    {
        [Inject] protected IInjector Injector;
        
        public void Configure()
        {
            Injector.Bind<IEventBus>().ToSingle<EventBus>();
        }
    }
}