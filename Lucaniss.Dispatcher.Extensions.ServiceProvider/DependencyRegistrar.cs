using System;
using System.Collections.Generic;
using Lucaniss.Dispatcher.Dependencies;
using Microsoft.Extensions.DependencyInjection;

namespace Lucaniss.Dispatcher.Extensions.ServiceProvider
{
    internal class DependencyRegistrar : IDependencyRegistrar
    {
        private static readonly IDictionary<DependencyLifetime, ServiceLifetime> LifetimeMap
            = new Dictionary<DependencyLifetime, ServiceLifetime>
            {
                { DependencyLifetime.Transient, ServiceLifetime.Transient },
                { DependencyLifetime.Scoped, ServiceLifetime.Scoped },
                { DependencyLifetime.Singleton, ServiceLifetime.Singleton }
            };

        private readonly IServiceCollection _serviceCollection;

        public DependencyRegistrar(IServiceCollection serviceCollection)
        {
            _serviceCollection = serviceCollection;
        }

        public void Register(Type interfaceType, Type implementationType, DependencyLifetime dependencyLifetime)
        {
            _serviceCollection.Add(new ServiceDescriptor(
                interfaceType,
                implementationType,
                LifetimeMap[dependencyLifetime]));
        }

        public void Register(Type interfaceType, Object implementationIntance, DependencyLifetime dependencyLifetime)
        {
            _serviceCollection.Add(new ServiceDescriptor(
                interfaceType,
                implementationIntance));
        }

        public void RegisterOpenGeneric(Type implementationType, DependencyLifetime dependencyLifetime)
        {
            _serviceCollection.Add(new ServiceDescriptor(
                implementationType,
                implementationType,
                LifetimeMap[dependencyLifetime]));
        }
    }
}