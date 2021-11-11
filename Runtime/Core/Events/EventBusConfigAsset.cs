namespace Foxes.Core.Events
{
    public class EventBusConfigAsset : ConfigAsset
    {
        [Inject] protected IConfigManager ConfigManager;
        
        public override void Configure()
        {
            ConfigManager.AddConfig<EventBusConfig>();
        }
    }
}