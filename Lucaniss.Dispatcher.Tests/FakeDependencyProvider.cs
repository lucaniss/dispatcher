using System;
using System.Collections.Generic;
using Lucaniss.Dispatcher.Dependencies;

namespace Lucaniss.Dispatcher.Tests
{
    public class FakeDependencyProvider : IDependencyProvider
    {
        private readonly IDictionary<Type, Object> _dictionary;

        public FakeDependencyProvider()
        {
            _dictionary = new Dictionary<Type, Object>();
        }

        public Object Resolve(Type type)
        {
            return _dictionary[type];
        }

        public void Add(Type type, Object instance)
        {
            _dictionary.Add(type, instance);
        }
    }
}