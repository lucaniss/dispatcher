using System;
using System.Collections.Generic;
using Lucaniss.Dispatcher.Assertions;

namespace Lucaniss.Dispatcher.Configurations
{
    internal class DecoratorRule
    {
        public Predicate<Type> RequestTypePredicate { get; }

        public Predicate<Type> ResponseTypePredicate { get; }

        public IReadOnlyList<Type> DecoratorTypes { get; }

        public DecoratorRule(DecoratorRuleDraft draft)
        {
            Assert.NotNull(draft, nameof(draft));
            Assert.NotNull(draft.RequestTypePredicate, nameof(draft.RequestTypePredicate));
            Assert.NotNull(draft.ResponseTypePredicate, nameof(draft.ResponseTypePredicate));
            Assert.NotNull(draft.DecoratorTypes, nameof(draft.DecoratorTypes));

            RequestTypePredicate = draft.RequestTypePredicate;
            ResponseTypePredicate = draft.ResponseTypePredicate;
            DecoratorTypes = draft.DecoratorTypes;
        }

        public Boolean Match(Type requestType, Type responseType)
        {
            Assert.NotNull(requestType, nameof(requestType));
            Assert.NotNull(responseType, nameof(responseType));

            return RequestTypePredicate(requestType) && ResponseTypePredicate(responseType);
        }
    }
}