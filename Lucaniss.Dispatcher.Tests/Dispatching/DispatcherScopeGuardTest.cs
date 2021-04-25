using FluentAssertions;
using Lucaniss.Dispatcher.Dispatching;
using Lucaniss.Dispatcher.Exceptions;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Dispatching
{
    public class DispatcherScopeGuardTest
    {
        [Fact]
        public void Create_WhenCreatingScopeGuardInsideAnotherScope_ThenThrowException()
        {
            // Act
            static void Action()
            {
                using (DispatcherScopeGuard.Create())
                {
                    using (DispatcherScopeGuard.Create())
                    {
                    }
                }
            }

            // Assert
            var exception = Assert.Throws<DispatcherException>(Action);

            exception.Message.Should().Be("You cannot call dispatcher inside request handler pipeline.");
        }

        [Fact]
        public void Create_WhenCreatingScopeGuardOutsideAnotherScope_ThenNothingHappens()
        {
            // Act, Assert
            using (DispatcherScopeGuard.Create())
            {
            }

            using (DispatcherScopeGuard.Create())
            {
            }
        }
    }
}