namespace Foxes.Core
{
    using System;

    public interface IContext : IDisposable
    {
        IInjector Injector { get; }

        IContext Configure(IConfig config);

        IContext Configure<T>() where T : IConfig;
        
        IContext Configure(Type type);
    }
}
