using System;
using Lucaniss.Dispatcher.Assertions;

namespace Lucaniss.Dispatcher.Dependencies
{
    internal class HandlerRegistration
    {
        public Type InterfaceType { get; }

        public Type ImplementationType { get; }

        public HandlerRegistration(Type interfaceType, Type implementationType)
        {
            Assert.NotNull(interfaceType, nameof(interfaceType));
            Assert.NotNull(implementationType, nameof(implementationType));

            InterfaceType = interfaceType;
            ImplementationType = implementationType;
        }
    }
}