namespace Foxes.Injection
{
    using System;

    public class Context : IContext
    {
        private readonly ConfigManager _configManager;

        public IInjector Injector { get; }

        public Context()
        {
            Injector = new Injector();

            Injector.Bind<IInjector>().ToValue(Injector);
            Injector.Bind<IContext>().ToValue(this);

            _configManager = new ConfigManager();
            Injector.Inject(_configManager);
        }

        public IContext Configure(IConfig config)
        {
            _configManager.AddConfig(config);

            return this;
        }

        public IContext Configure<T>() where T : IConfig
        {
            _configManager.AddConfig<T>();

            return this;
        }

        public IContext Configure(Type type)
        {
            _configManager.AddConfig(type);

            return this;
        }

        public void Dispose()
        {
            _configManager.Dispose();
            Injector.Dispose();
        }
    }
}
