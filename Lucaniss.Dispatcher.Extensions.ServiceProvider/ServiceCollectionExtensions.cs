using System;
using System.Collections.Generic;
using System.Reflection;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Pipelines;
using Microsoft.Extensions.DependencyInjection;

namespace Lucaniss.Dispatcher.Extensions.ServiceProvider
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDispatcher(
            this IServiceCollection services,
            IReadOnlyList<Assembly> assembliesToScan,
            Action<PipelineBuilder> builderAction = null)
        {
            _ = services.AddScoped<IDependencyProvider, DependencyProvider>();

            DependencyConfigurator.Setup(new DependencyRegistrar(services), assembliesToScan, builderAction);
        }
    }
}