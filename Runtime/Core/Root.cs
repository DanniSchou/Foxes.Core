namespace Foxes.Core
{
    using Injection;
    using JetBrains.Annotations;

    [PublicAPI]
    public sealed class Root
    {
        public static Root Instance { get; } = new Root();

        public IContext Context { get; }

        private Root()
        {
            Context = new Context();

            var settings = CoreSettings.GetOrCreateSettings();
            foreach (var config in settings.Configs)
            {
                Context.Configure(config);
            }
        }

        public void Inject(object target)
        {
            Context.Injector.Inject(target);
        }
    }
}