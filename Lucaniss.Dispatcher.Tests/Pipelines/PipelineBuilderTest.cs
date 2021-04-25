using System;
using FluentAssertions;
using FluentAssertions.Execution;
using Lucaniss.Dispatcher.Pipelines;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Pipelines
{
    public partial class PipelineBuilderTest
    {
        [Fact]
        public void ForAny_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForAny());
        }

        [Fact]
        public void ForRequest_WhenGenericParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForRequest(r => r.WhenType<SimpleRequest>()));
        }

        [Fact]
        public void ForRequest_WhenNormalParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForRequest(r => r.WhenType(typeof(SimpleRequest))));
        }

        [Fact]
        public void ForRequest_WhenPredicateParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForRequest(r => r.WhenType(t => t == typeof(SimpleRequest))));
        }

        [Fact]
        public void ForResponse_WhenGenericParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForResponse(r => r.WhenType<SimpleResponse>()));
        }

        [Fact]
        public void ForResponse_WhenNormalParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForResponse(r => r.WhenType(typeof(SimpleResponse))));
        }

        [Fact]
        public void ForResponse_WhenPredicateParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().ForResponse(r => r.WhenType(t => t == typeof(SimpleResponse))));
        }

        [Fact]
        public void For_WhenGenericParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().For(
                r => r.WhenType<SimpleRequest>(),
                r => r.WhenType<SimpleResponse>()));
        }

        [Fact]
        public void For_WhenNormalParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().For(
                r => r.WhenType(typeof(SimpleRequest)),
                r => r.WhenType(typeof(SimpleResponse))));
        }

        [Fact]
        public void For_WhenPredicateParamUsed_ThenConfigurationAsExpected()
        {
            Test(c => c.Build().For(
                r => r.WhenType(t => t == typeof(SimpleRequest)),
                r => r.WhenType(t => t == typeof(SimpleResponse))));
        }

        private static void Test(Func<PipelineBuilder, DecoratorRuleBuilder2> builderFunc)
        {
            // Arrange
            var pipelineBuilder = new PipelineBuilder();

            // Act
            builderFunc(pipelineBuilder)
                .UseDecorators(d =>
                {
                    d.Use(typeof(SimpleRequestHandlerDecorator1<,>));
                    d.Use(typeof(SimpleRequestHandlerDecorator2<,>));
                });

            var config = pipelineBuilder.DecoratorRules;

            // Assert
            config.Count.Should().Be(1);

            using (new AssertionScope())
            {
                config[0].RequestTypePredicate(typeof(SimpleRequest)).Should().BeTrue();
                config[0].ResponseTypePredicate(typeof(SimpleResponse)).Should().BeTrue();

                config[0].DecoratorTypes.Count.Should().Be(2);

                config[0].DecoratorTypes[0].Should().BeSameAs(typeof(SimpleRequestHandlerDecorator1<,>));
                config[0].DecoratorTypes[1].Should().BeSameAs(typeof(SimpleRequestHandlerDecorator2<,>));
            }
        }
    }
}