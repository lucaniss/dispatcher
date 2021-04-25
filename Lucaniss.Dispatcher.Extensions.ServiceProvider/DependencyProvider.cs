using System;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Exceptions;
using Microsoft.Extensions.DependencyInjection;

namespace Lucaniss.Dispatcher.Extensions.ServiceProvider
{
    internal class DependencyProvider : IDependencyProvider
    {
        private readonly IServiceProvider _serviceProvider;

        public DependencyProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public Object Resolve(Type type)
        {
            try
            {
                return _serviceProvider.GetRequiredService(type);
            }
            catch (InvalidOperationException e)
            {
                throw new DispatcherException($"Cannot resolve service for type '{type.FullName}'.", e);
            }
        }
    }
}