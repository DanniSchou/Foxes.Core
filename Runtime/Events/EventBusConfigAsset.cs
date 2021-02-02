namespace Foxes.Events
{
    using Core;
    using Injection;

    public class EventBusConfigAsset : ConfigAsset
    {
        [Inject] protected IContext Context;
        
        public override void Configure()
        {
            Context.Configure<EventBusConfig>();
        }
    }
}