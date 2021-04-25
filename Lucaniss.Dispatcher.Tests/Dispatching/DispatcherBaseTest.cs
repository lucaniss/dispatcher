using System;
using System.Threading.Tasks;
using FluentAssertions;
using Lucaniss.Dispatcher.Dispatching;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Dispatching
{
    public partial class DispatcherBaseTest
    {
        [Fact]
        public async Task Dispatch_WhenRequestIsNull_ThenThrowException()
        {
            // Arrange
            var dependencyProvider = new FakeDependencyProvider();
            var dispatcher = new DispatcherBase(dependencyProvider);

            // Act
            Task Action() => dispatcher.Dispatch<SimpleResponse>(null, default);

            // Assert
            var exception = await Assert.ThrowsAsync<ArgumentNullException>(Action);

            exception.Message.Should().Be("Value cannot be null. (Parameter 'request')");
        }

        [Fact]
        public async Task Dispatch_WhenRequestIsNotNull_ThenExecuteScopeWrapper()
        {
            // Arrange
            var dependencyProvider = new FakeDependencyProvider();
            dependencyProvider.Add(typeof(DispatcherScopeWrapper<SimpleRequest, SimpleResponse>), new FakeScopeWrapper());

            var dispatcher = new DispatcherBase(dependencyProvider);

            // Act
            var result = await dispatcher.Dispatch(new SimpleRequest(), default);

            // Assert
            result.Text.Should().Be("Echo");
        }
    }
}