using System;
using Lucaniss.Dispatcher.Assertions;

namespace Lucaniss.Dispatcher.Pipelines
{
    public class PredicateConfigurator
    {
        internal Predicate<Type> Predicate { get; private set; }

        public void WhenType<T>()
        {
            Predicate = t => t == typeof(T);
        }

        public void WhenType(Type type)
        {
            Assert.NotNull(type, nameof(type));

            Predicate = t => t == type;
        }

        public void WhenType(Predicate<Type> predicate)
        {
            Assert.NotNull(predicate, nameof(predicate));

            Predicate = predicate;
        }
    }
}