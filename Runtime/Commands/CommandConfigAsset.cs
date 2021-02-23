namespace Foxes.Commands
{
    using Core;

    public class CommandConfigAsset : ConfigAsset
    {
        [Inject] protected IContext Context;
        
        public override void Configure()
        {
            Context.Configure<CommandConfig>();
        }
    }
}