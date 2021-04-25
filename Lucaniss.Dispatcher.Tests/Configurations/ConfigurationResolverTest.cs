using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Lucaniss.Dispatcher.Configurations;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Configurations
{
    public partial class ConfigurationResolverTest
    {
        [Theory]
        [InlineData(null, typeof(SimpleResponse), "requestType")]
        [InlineData(typeof(SimpleRequest), null, "responseType")]
        public void Get_WhenGivenNullParams_ThenThrowException(Type requestType, Type responseType, String paramName)
        {
            // Arrange
            var config = new Configuration(new List<DecoratorRule>());
            var resolver = new ConfigurationResolver(config);

            // Act
            void Action() =>
                _ = resolver
                    .ResolveDecoratorTypes(requestType, responseType)
                    .ToList();

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(Action);

            exception.Message.Should().Be($"Value cannot be null. (Parameter '{paramName}')");
        }

        [Fact]
        public void Get_WhenConfigurationIsEmpty_ThenReturnEmptyList()
        {
            // Arrange
            var config = new Configuration(new List<DecoratorRule>());
            var resolver = new ConfigurationResolver(config);

            // Act
            var types = resolver
                .ResolveDecoratorTypes(typeof(SimpleRequest), typeof(SimpleResponse))
                .ToList();

            // Assert
            types.Count.Should().Be(0);
        }

        [Fact]
        public void Get_WhenConfigurationHasMultipleMatchingRules_ThenReturnDecoratorTypesInExpectedOrder()
        {
            // Arrange
            var ruleDraft = new DecoratorRuleDraft
            {
                RequestTypePredicate = t => t == typeof(SimpleRequest),
                ResponseTypePredicate = t => t == typeof(SimpleResponse),
                DecoratorTypes = new List<Type>
                {
                    typeof(SimpleDecorator1<,>)
                }
            };

            var config = new Configuration(new List<DecoratorRule>
            {
                new( new DecoratorRuleDraft
                {
                    RequestTypePredicate = t => t == typeof(SimpleRequest),
                    ResponseTypePredicate = t => t == typeof(SimpleResponse),
                    DecoratorTypes = new List<Type>
                    {
                        typeof(SimpleDecorator1<,>)
                    }
                }),
                new( new DecoratorRuleDraft
                {
                    RequestTypePredicate = t => t == typeof(SimpleRequest),
                    ResponseTypePredicate = t => t == typeof(SimpleResponse),
                    DecoratorTypes = new List<Type>
                    {
                        typeof(SimpleDecorator2<,>)
                    }
                }),
            });

            var resolver = new ConfigurationResolver(config);

            // Act
            var types = resolver
                .ResolveDecoratorTypes(typeof(SimpleRequest), typeof(SimpleResponse))
                .ToList();

            // Assert
            types.Count.Should().Be(2);

            using (new AssertionScope())
            {
                types[0].Should().Be(typeof(SimpleDecorator1<,>));
                types[1].Should().Be(typeof(SimpleDecorator2<,>));
            }
        }
    }
}