﻿namespace Foxes.Core.Injection.Resolvers
{
    using System;

    public class ValueResolver : IResolver, IDisposable
    {
        private readonly object _value;

        public ValueResolver(object value)
        {
            _value = value;
        }

        public object Resolve()
        {
            return _value;
        }

        public object Resolve(params object[] arguments)
        {
            return Resolve();
        }

        public void Dispose()
        {
            if (_value is IDisposable disposable)
            {
                disposable.Dispose();
            }
        }
    }
}
