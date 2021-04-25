using System;
using System.Collections.Generic;
using FluentAssertions;
using Lucaniss.Dispatcher.Configurations;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Configurations
{
    public class DecoratorRuleTest
    {
        [Theory]
        [InlineData(null, typeof(String), "requestType")]
        [InlineData(typeof(String), null, "responseType")]
        public void Match_WhenGivenNullParams_ThenThrowException(Type requestType, Type responseType, String paramName)
        {
            // Arrange
            var decoratorRule = new DecoratorRule(new DecoratorRuleDraft
            {
                ResponseTypePredicate = _ => true,
                RequestTypePredicate = _ => true,
                DecoratorTypes = new List<Type>()
            });

            // Act
            void Action() => decoratorRule.Match(requestType, responseType);

            // Assert
            var exception = Assert.Throws<ArgumentNullException>(Action);

            exception.Message.Should().Be($"Value cannot be null. (Parameter '{paramName}')");
        }

        [Theory]
        [InlineData(typeof(String), typeof(String), true)]
        [InlineData(typeof(String), typeof(Guid), false)]
        [InlineData(typeof(Guid), typeof(String), false)]
        [InlineData(typeof(Guid), typeof(Guid), false)]
        public void Match_WhenGivenValidParams_ThenReturnValidResponse(Type requestType, Type responseType, Boolean matchResult)
        {
            // Arrange
            var decoratorRule = new DecoratorRule(new DecoratorRuleDraft
            {
                ResponseTypePredicate = t => t == typeof(String),
                RequestTypePredicate = t => t == typeof(String),
                DecoratorTypes = new List<Type>()
            });

            // Act
            var match = decoratorRule.Match(requestType, responseType);

            // Assert
            match.Should().Be(matchResult);
        }
    }
}