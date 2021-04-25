using System;
using System.Collections.Generic;
using System.Reflection;
using Autofac;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Pipelines;

namespace Lucaniss.Dispatcher.Extensions.AutoFac
{
    public static class ContainerBuilderExtensions
    {
        public static void AddDispatcher(
            this ContainerBuilder services,
            IReadOnlyList<Assembly> assembliesToScan,
            Action<PipelineBuilder> pipelineBuilderAction = null)
        {
            _ = services.RegisterType<DependencyProvider>().As<IDependencyProvider>();

            DependencyConfigurator.Setup(new DependencyRegistrar(services), assembliesToScan, pipelineBuilderAction);
        }
    }
}