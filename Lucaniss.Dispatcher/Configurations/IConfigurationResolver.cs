using System;
using System.Collections.Generic;

namespace Lucaniss.Dispatcher.Configurations
{
    internal interface IConfigurationResolver
    {
        IReadOnlyList<Type> ResolveDecoratorTypes(Type requestType, Type responseType);
    }
}