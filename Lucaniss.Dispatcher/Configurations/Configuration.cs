using System.Collections.Generic;
using Lucaniss.Dispatcher.Assertions;

namespace Lucaniss.Dispatcher.Configurations
{
    internal class Configuration : IConfiguration
    {
        public IReadOnlyList<DecoratorRule> DecoratorRules { get; }

        public Configuration(IReadOnlyList<DecoratorRule> decoratorRules)
        {
            Assert.NotNull(decoratorRules, nameof(decoratorRules));

            DecoratorRules = decoratorRules;
        }
    }
}