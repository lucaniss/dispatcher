using System;

namespace Lucaniss.Dispatcher.Dependencies
{
    public interface IDependencyRegistrar
    {
        void Register(Type interfaceType, Type implementationType, DependencyLifetime dependencyLifetime);

        void Register(Type interfaceType, Object implementationIntance, DependencyLifetime dependencyLifetime);

        void RegisterOpenGeneric(Type implementationType, DependencyLifetime dependencyLifetime);
    }
}