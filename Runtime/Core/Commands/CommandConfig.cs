namespace Foxes.Core.Commands
{
    using Core;
    using JetBrains.Annotations;

    [PublicAPI]
    public class CommandConfig : IConfig, IHasConfigValidation
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