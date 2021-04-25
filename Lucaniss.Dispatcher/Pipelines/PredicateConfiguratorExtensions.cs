using System;

namespace Lucaniss.Dispatcher.Pipelines
{
    internal static class PredicateConfiguratorExtensions
    {
        internal static Predicate<Type> ResolvePredicate(this Action<PredicateConfigurator> configuratorAction)
        {
            var configurator = new PredicateConfigurator();
            configuratorAction(configurator);

            return configurator.Predicate;
        }
    }
}