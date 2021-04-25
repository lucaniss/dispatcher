using System;
using System.Collections.Generic;
using System.Linq;
using Lucaniss.Dispatcher.Extensions;

namespace Lucaniss.Dispatcher.Dependencies
{
    internal static class HandlerRegistrationProvider
    {
        public static IEnumerable<HandlerRegistration> GetHandlerRegistrations(this IEnumerable<Type> types)
        {
            var handlerMapping = types
                .Where(t => t.CanCreateInstance())
                .Select(t => (
                    InterfaceTypes: t.GetRequestHandlerInterfaces(),
                    ImplementationType: t
                ))
                .Where(map => map.InterfaceTypes.Any())
                .SelectMany(map => map.InterfaceTypes.Select(it => (
                    InterfaceType: it,
                    map.ImplementationType)))
                .ToList();

            var implementationTypes = handlerMapping
                .Select(e => e.ImplementationType)
                .ToList();

            return handlerMapping
                .Where(map => map.ImplementationType.IsLastInInheritanceHierarchy(implementationTypes))
                .Select(map => new HandlerRegistration(map.InterfaceType, map.ImplementationType));
        }
    }
}