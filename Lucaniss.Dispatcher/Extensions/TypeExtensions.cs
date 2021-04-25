using System;
using System.Collections.Generic;
using System.Linq;
using Lucaniss.Dispatcher.Assertions;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Extensions
{
    public static class TypeExtensions
    {
        public static Boolean CanCreateInstance(this Type type)
        {
            Assert.NotNull(type, nameof(type));

            return type.IsClass && !type.IsAbstract && !type.IsGenericType;
        }

        public static IEnumerable<Type> GetRequestHandlerInterfaces(this Type type)
        {
            Assert.NotNull(type, nameof(type));

            return type.GetInterfaces().Where(e => e.IsRequestHandlerInterface());
        }

        public static Boolean IsRequestHandlerInterface(this Type type)
        {
            Assert.NotNull(type, nameof(type));

            return type.IsInterface
                && type.IsGenericType
                && type.GetGenericTypeDefinition() == typeof(IRequestHandler<,>);
        }

        public static Boolean IsLastInInheritanceHierarchy(this Type type, IEnumerable<Type> types)
        {
            Assert.NotNull(type, nameof(type));

            return !types.Any(t => t.GetBaseTypes().Any(b => b == type));
        }

        public static IEnumerable<Type> GetBaseTypes(this Type type)
        {
            Assert.NotNull(type, nameof(type));

            var baseType = type.BaseType;

            while (baseType != null)
            {
                yield return baseType;

                baseType = baseType.BaseType;
            }
        }
    }
}