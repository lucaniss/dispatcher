using System;
using Autofac;
using Lucaniss.Dispatcher.Dependencies;

namespace Lucaniss.Dispatcher.Extensions.AutoFac
{
    internal class DependencyRegistrar : IDependencyRegistrar
    {
        private readonly ContainerBuilder _serviceCollection;

        public DependencyRegistrar(ContainerBuilder serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void Add(Type implementationType, DependencyLifetime dependencyLifetime)
        {
            var builder = _serviceCollection
                .RegisterType(implementationType)
                .AsSelf();

            if (dependencyLifetime == DependencyLifetime.Singleton)
            {
                _ = builder.SingleInstance();
            }

            if (dependencyLifetime == DependencyLifetime.Transient)
            {
                _ = builder.InstancePerDependency();
            }
        }

        public void Register(Type interfaceType, Type implementationType, DependencyLifetime dependencyLifetime)
        {
            var builder = _serviceCollection
                .RegisterType(implementationType)
                .As(interfaceType);

            if (dependencyLifetime == DependencyLifetime.Singleton)
            {
                _ = builder.SingleInstance();
            }

            if (dependencyLifetime == DependencyLifetime.Transient)
            {
                _ = builder.InstancePerDependency();
            }
        }

        public void Register(Type interfaceType, Object implementationIntance, DependencyLifetime dependencyLifetime)
        {
            var builder = _serviceCollection
                .RegisterInstance(implementationIntance)
                .As(interfaceType);

            if (dependencyLifetime == DependencyLifetime.Singleton)
            {
                _ = builder.SingleInstance();
            }

            if (dependencyLifetime == DependencyLifetime.Transient)
            {
                _ = builder.InstancePerDependency();
            }
        }

        public void RegisterOpenGeneric(Type implementationType, DependencyLifetime dependencyLifetime)
        {
            var builder = _serviceCollection
                .RegisterGeneric(implementationType);

            if (dependencyLifetime == DependencyLifetime.Singleton)
            {
                _ = builder.SingleInstance();
            }

            if (dependencyLifetime == DependencyLifetime.Transient)
            {
                _ = builder.InstancePerDependency();
            }
        }
    }
}