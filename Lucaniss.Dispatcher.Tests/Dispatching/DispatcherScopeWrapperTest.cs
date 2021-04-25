using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Lucaniss.Dispatcher.Configurations;
using Lucaniss.Dispatcher.Dispatching;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Dispatching
{
    public partial class DispatcherScopeWrapperTest
    {
        [Theory]
        [InlineData("Echo_1", "Echo_1_OUT")]
        [InlineData("Echo_2", "Echo_2_OUT")]
        [InlineData("Echo_3", "Echo_3_OUT")]
        public async Task Handle_WhenThereIsNoDecoratorToUse_ThenReturnSimpleResponse(String input, String output)
        {
            // Arrange
            var configuration = new Configuration(new List<DecoratorRule>());

            var wrapper = new DispatcherScopeWrapper<SimpleRequest, SimpleResponse>(
                new SimpleHandler(),
                new ConfigurationResolver(configuration),
                new FakeDependencyProvider());

            // Act
            var result = await wrapper.Handle(new SimpleRequest(input), default);

            // Assert
            result.Text.Should().Be(output);
        }

        [Theory]
        [InlineData("Echo_1", "PRE_Echo_1_OUT_POST")]
        [InlineData("Echo_2", "PRE_Echo_2_OUT_POST")]
        [InlineData("Echo_3", "PRE_Echo_3_OUT_POST")]
        public async Task Handle_WhenThereIsDecoratorToUse_ThenReturnModifiedResponse(String text1, String text2)
        {
            // Arrange
            var configuration = new Configuration(new List<DecoratorRule>
            {
                new(new DecoratorRuleDraft
                {
                    RequestTypePredicate = t => t == typeof(SimpleRequest),
                    ResponseTypePredicate = t => t == typeof(SimpleResponse),
                    DecoratorTypes = new List<Type>
                    {
                        typeof(SimpleHandlerDecorator<,>)
                    }
                })
            });

            var dependencyProvider = new FakeDependencyProvider();
            dependencyProvider.Add(typeof(SimpleHandlerDecorator<SimpleRequest, SimpleResponse>), new SimpleHandlerDecorator<SimpleRequest, SimpleResponse>());

            var wrapper = new DispatcherScopeWrapper<SimpleRequest, SimpleResponse>(
                new SimpleHandler(),
                new ConfigurationResolver(configuration),
                dependencyProvider);

            // Act
            var result = await wrapper.Handle(new SimpleRequest(text1), default);

            // Assert
            result.Text.Should().Be(text2);
        }
    }
}