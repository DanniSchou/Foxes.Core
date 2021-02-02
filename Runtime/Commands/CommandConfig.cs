namespace Foxes.Commands
{
    using Events;
    using Injection;

    public class CommandConfig : IConfig, IHasConfigValidation
    {
        [Inject] protected IInjector Injector;
        
        public void Configure()
        {
            Injector.Bind<ICommandMap>().ToSingleton<CommandMap>();
        }
        
        public bool IsValid()
        {
            return Injector.IsBound<IEventBus>();
        }
    }
}