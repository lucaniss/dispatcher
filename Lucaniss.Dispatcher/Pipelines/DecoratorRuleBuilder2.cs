using System;
using System.Collections.Generic;
using System.Linq;
using Lucaniss.Dispatcher.Assertions;
using Lucaniss.Dispatcher.Configurations;

namespace Lucaniss.Dispatcher.Pipelines
{
    public class DecoratorRuleBuilder2
    {
        private readonly List<DecoratorRule> _decoratorRules;
        private readonly DecoratorRuleDraft _decoratorRule;

        internal DecoratorRuleBuilder2(List<DecoratorRule> decoratorRules, DecoratorRuleDraft decoratorRule)
        {
            Assert.NotNull(decoratorRules, nameof(decoratorRules));
            Assert.NotNull(decoratorRule, nameof(decoratorRule));

            _decoratorRules = decoratorRules;
            _decoratorRule = decoratorRule;
        }

        public void UseDecorators(Action<DecoratorTypesSelector> selectorAction)
        {
            Assert.NotNull(selectorAction, nameof(selectorAction));

            var selector = new DecoratorTypesSelector();
            selectorAction(selector);

            if (selector.DecoratorTypes.Any())
            {
                _decoratorRule.DecoratorTypes = selector.DecoratorTypes;
                
                _decoratorRules.Add(new DecoratorRule(_decoratorRule));
            }
        }
    }
}