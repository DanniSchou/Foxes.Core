namespace Foxes.Core
{
    using Injection;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class Root
    {
        public static IInjector Injector => Instance._injector;
        
        private static Root Instance { get; } = new Root();

        private readonly IInjector _injector;

        private Root()
        {
            _injector = new Injector();
            
            SetupInjector();
            ConfigureInjector();
        }

        private void SetupInjector()
        {
            _injector.Bind<IInjector>().ToValue(_injector);
            _injector.Bind<IConfigManager>().ToSingle<ConfigManager>();
        }

        private void ConfigureInjector()
        {
            var configManager = _injector.Get<IConfigManager>();
            var settings = CoreSettings.GetOrCreateSettings();
            
            foreach (var config in settings.Configs)
            {
                configManager.AddConfig(config);
            }
        }
    }
}