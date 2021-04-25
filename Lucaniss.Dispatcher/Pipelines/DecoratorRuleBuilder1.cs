using System;
using System.Collections.Generic;
using Lucaniss.Dispatcher.Assertions;
using Lucaniss.Dispatcher.Configurations;

namespace Lucaniss.Dispatcher.Pipelines
{
    public class DecoratorRuleBuilder1
    {
        private readonly List<DecoratorRule> _decoratorRules;
        private readonly DecoratorRuleDraft _decoratorRule;

        internal DecoratorRuleBuilder1(List<DecoratorRule> decoratorRules, DecoratorRuleDraft decoratorRule)
        {
            Assert.NotNull(decoratorRules, nameof(decoratorRules));
            Assert.NotNull(decoratorRule, nameof(decoratorRule));

            _decoratorRules = decoratorRules;
            _decoratorRule = decoratorRule;
        }

        public DecoratorRuleBuilder2 ForAny()
        {
            _decoratorRule.RequestTypePredicate = _ => true;
            _decoratorRule.ResponseTypePredicate = _ => true;

            return new(_decoratorRules, _decoratorRule);
        }

        public DecoratorRuleBuilder2 ForRequest(Action<PredicateConfigurator> configAction)
        {
            Assert.NotNull(configAction, nameof(configAction));

            _decoratorRule.RequestTypePredicate = configAction.ResolvePredicate();
            _decoratorRule.ResponseTypePredicate = _ => true;

            return new(_decoratorRules, _decoratorRule);
        }

        public DecoratorRuleBuilder2 ForResponse(Action<PredicateConfigurator> configAction)
        {
            Assert.NotNull(configAction, nameof(configAction));

            _decoratorRule.RequestTypePredicate = _ => true;
            _decoratorRule.ResponseTypePredicate = configAction.ResolvePredicate();

            return new(_decoratorRules, _decoratorRule);
        }

        public DecoratorRuleBuilder2 For(Action<PredicateConfigurator> requestConfigAction, Action<PredicateConfigurator> responseConfigAction)
        {
            Assert.NotNull(requestConfigAction, nameof(requestConfigAction));
            Assert.NotNull(responseConfigAction, nameof(responseConfigAction));

            _decoratorRule.RequestTypePredicate = requestConfigAction.ResolvePredicate();
            _decoratorRule.ResponseTypePredicate = responseConfigAction.ResolvePredicate();

            return new(_decoratorRules, _decoratorRule);
        }
    }
}