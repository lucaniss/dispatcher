using System;

namespace Lucaniss.Dispatcher.Dependencies
{
    public interface IDependencyProvider
    {
        Object Resolve(Type type);
    }
}