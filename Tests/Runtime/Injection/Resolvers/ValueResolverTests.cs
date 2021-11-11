namespace Foxes.Core.Injection.Resolvers
{
    using NUnit.Framework;

    public class ValueResolverTests
    {
        [Test]
        public void Resolve_InstanceInjectedAndReturned()
        {
            var value = new object();
            var resolver = new ValueResolver(value);

            var instance = resolver.Resolve();
            Assert.AreSame(value, instance);
        }
    }
}