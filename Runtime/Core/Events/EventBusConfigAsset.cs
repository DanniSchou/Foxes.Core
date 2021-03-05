namespace Foxes.Core.Events
{
    using Core;

    public class EventBusConfigAsset : ConfigAsset
    {
        [Inject] protected IContext Context;
        
        public override void Configure()
        {
            Context.Configure<EventBusConfig>();
        }
    }
}