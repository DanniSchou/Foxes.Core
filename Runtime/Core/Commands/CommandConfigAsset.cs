namespace Foxes.Core.Commands
{
    public class CommandConfigAsset : ConfigAsset
    {
        [Inject] protected IConfigManager ConfigManager;
        
        public override void Configure()
        {
            ConfigManager.AddConfig<CommandConfig>();
        }
    }
}