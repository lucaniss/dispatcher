using System;
using System.Collections.Generic;
using System.Linq;
using Lucaniss.Dispatcher.Assertions;

namespace Lucaniss.Dispatcher.Configurations
{
    internal class ConfigurationResolver : IConfigurationResolver
    {
        private readonly IConfiguration _config;

        public ConfigurationResolver(IConfiguration config)
        {
            Assert.NotNull(config, nameof(config));

            _config = config;
        }

        public IReadOnlyList<Type> ResolveDecoratorTypes(Type requestType, Type responseType)
        {
            Assert.NotNull(requestType, nameof(requestType));
            Assert.NotNull(responseType, nameof(responseType));

            return _config.DecoratorRules
                .Where(e => e.Match(requestType, responseType))
                .SelectMany(e => e.DecoratorTypes)
                .ToList();
        }
    }
}