using System;
using Autofac;
using Autofac.Core;
using Autofac.Core.Registration;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Exceptions;

namespace Lucaniss.Dispatcher.Extensions.AutoFac
{
    internal class DependencyProvider : IDependencyProvider
    {
        private readonly ILifetimeScope _container;

        public DependencyProvider(ILifetimeScope container)
        {
            _container = container;
        }

        public Object Resolve(Type type)
        {
            try
            {
                return _container.Resolve(type);
            }
            catch (ComponentNotRegisteredException e)
            {
                throw new DispatcherException($"Cannot resolve service for type '{type.FullName}'.", e);
            }
            catch (DependencyResolutionException e)
            {
                throw new DispatcherException($"Cannot resolve service for type '{type.FullName}'.", e);
            }
        }
    }
}