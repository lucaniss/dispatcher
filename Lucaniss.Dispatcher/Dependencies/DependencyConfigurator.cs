using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using Lucaniss.Dispatcher.Assertions;
using Lucaniss.Dispatcher.Configurations;
using Lucaniss.Dispatcher.Dispatching;
using Lucaniss.Dispatcher.Pipelines;

namespace Lucaniss.Dispatcher.Dependencies
{
    [ExcludeFromCodeCoverage]
    public static class DependencyConfigurator
    {
        public static void Setup(
            IDependencyRegistrar dependencyRegistrar,
            IReadOnlyList<Assembly> assembliesToScan,
            Action<PipelineBuilder> builderAction = null)
        {
            Assert.NotNull(dependencyRegistrar, nameof(dependencyRegistrar));
            Assert.NotNull(assembliesToScan, nameof(assembliesToScan));

            foreach (var registration in assembliesToScan.Distinct().SelectMany(e => e.DefinedTypes).GetHandlerRegistrations())
            {
                dependencyRegistrar.Register(
                    registration.InterfaceType,
                    registration.ImplementationType,
                    DependencyLifetime.Transient);
            }

            var pipelineBuilder = new PipelineBuilder();
            builderAction?.Invoke(pipelineBuilder);

            foreach (var decoratorType in pipelineBuilder.DecoratorRules.SelectMany(e => e.DecoratorTypes).Distinct())
            {
                dependencyRegistrar.RegisterOpenGeneric(decoratorType, DependencyLifetime.Transient);
            }

            dependencyRegistrar.Register(typeof(IConfiguration), new Configuration(pipelineBuilder.DecoratorRules), DependencyLifetime.Singleton);
            dependencyRegistrar.Register(typeof(IConfigurationResolver), typeof(ConfigurationResolver), DependencyLifetime.Singleton);

            dependencyRegistrar.Register(typeof(IDispatcher), typeof(DispatcherBase), DependencyLifetime.Scoped);

            dependencyRegistrar.RegisterOpenGeneric(typeof(DispatcherScopeWrapper<,>), DependencyLifetime.Scoped);
        }
    }
}