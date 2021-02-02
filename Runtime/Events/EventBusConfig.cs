namespace Foxes.Events
{
    using Injection;
    using JetBrains.Annotations;

    [PublicAPI]
    public class EventBusConfig : IConfig
    {
        [Inject] protected IInjector Injector;
        
        public void Configure()
        {
            Injector.Bind<IEventBus>().ToSingleton<EventBus>();
        }
    }
}