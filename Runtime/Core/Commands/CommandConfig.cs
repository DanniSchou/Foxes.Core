namespace Foxes.Core.Commands
{
    using JetBrains.Annotations;

    [PublicAPI]
    public class CommandConfig : IHasConfigValidation
    {
        [Inject] protected IInjector Injector;
        
        public void Configure()
        {
            Injector.Bind<ICommandMap>().ToSingle<CommandMap>();
        }
        
        public bool IsValid()
        {
            return Injector.IsBound<IEventBus>();
        }
    }
}