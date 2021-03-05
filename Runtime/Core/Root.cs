namespace Foxes.Core
{
    using Injection;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class Root
    {
        public static Root Instance { get; } = new Root();

        public IInjector Injector { get; }

        private IContext _context;

        private Root()
        {
            _context = new Context();

            var settings = CoreSettings.GetOrCreateSettings();
            foreach (var config in settings.Configs)
            {
                _context.Configure(config);
            }

            Injector = _context.Injector;
        }

        public void Inject(object target)
        {
            Injector.Inject(target);
        }
    }
}