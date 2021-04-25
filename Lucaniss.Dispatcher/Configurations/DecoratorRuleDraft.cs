using System;
using System.Collections.Generic;

namespace Lucaniss.Dispatcher.Configurations
{
    internal class DecoratorRuleDraft
    {
        public Predicate<Type> RequestTypePredicate { get; set; }

        public Predicate<Type> ResponseTypePredicate { get; set; }

        public IReadOnlyList<Type> DecoratorTypes { get; set; }
    }
}