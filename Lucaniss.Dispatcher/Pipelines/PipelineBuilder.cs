using System.Collections.Generic;
using Lucaniss.Dispatcher.Configurations;

namespace Lucaniss.Dispatcher.Pipelines
{
    public class PipelineBuilder
    {
        private readonly List<DecoratorRule> _decoratorRules;

        internal IReadOnlyList<DecoratorRule> DecoratorRules => _decoratorRules;

        internal PipelineBuilder()
        {
            _decoratorRules = new List<DecoratorRule>();
        }

        public DecoratorRuleBuilder1 Build()
        {
            return new(_decoratorRules, new DecoratorRuleDraft());
        }
    }
}