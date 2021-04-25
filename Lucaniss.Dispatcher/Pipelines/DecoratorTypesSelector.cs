using System;
using System.Collections.Generic;
using Lucaniss.Dispatcher.Assertions;

namespace Lucaniss.Dispatcher.Pipelines
{
    public class DecoratorTypesSelector
    {
        private readonly List<Type> _decoratorTypes;

        internal IReadOnlyList<Type> DecoratorTypes => _decoratorTypes;

        internal DecoratorTypesSelector()
        {
            _decoratorTypes = new List<Type>();
        }

        public void Use(Type type)
        {
            Assert.NotNull(type, nameof(type));

            _decoratorTypes.Add(type);
        }
    }
}