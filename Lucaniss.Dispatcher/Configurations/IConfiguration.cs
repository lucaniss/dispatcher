using System.Collections.Generic;

namespace Lucaniss.Dispatcher.Configurations
{
    internal interface IConfiguration
    {
        IReadOnlyList<DecoratorRule> DecoratorRules { get; }
    }
}