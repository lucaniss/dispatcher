using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using FluentAssertions.Execution;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Requests;
using Xunit;

namespace Lucaniss.Dispatcher.Tests.Dependencies
{
    public partial class HandlerRegistrationProviderTest
    {
        [Fact]
        public void GetHandlerRegistrations_WhenHandlerTypeIsGeneric_ThenReturnNothing()
        {
            // Arrange
            var types = new List<Type>
            {
                typeof(GenericHandler<,>)
            };

            // Act
            var mappings = types.GetHandlerRegistrations().ToList();

            // Assert
            mappings.Should().NotBeNull();
            mappings.Count.Should().Be(0);
        }

        [Fact]
        public void GetHandlerRegistrations_WhenHandlerTypeIsAbstract_ThenReturnNothing()
        {
            // Arrange
            var types = new List<Type>
            {
                typeof(AbstractHandler)
            };

            // Act
            var mappings = types.GetHandlerRegistrations().ToList();

            // Assert
            mappings.Should().NotBeNull();
            mappings.Count.Should().Be(0);
        }

        [Fact]
        public void GetHandlerRegistrations_WhenHandlerTypeDerivedFromAnother_ThenReturnLastEntry()
        {
            // Arrange
            var types = new List<Type>
            {
                typeof(SimpleHandler),
                typeof(SimpleHandlerDerived1),
                typeof(SimpleHandlerDerived2)
            };

            // Act
            var mappings = types.GetHandlerRegistrations().ToList();

            // Assert
            mappings.Should().NotBeNull();
            mappings.Count.Should().Be(1);

            using (new AssertionScope())
            {
                mappings[0].InterfaceType.Should().Be(typeof(IRequestHandler<SimpleRequest, SimpleResponse>));
                mappings[0].ImplementationType.Should().Be(typeof(SimpleHandlerDerived2));
            }
        }

        [Fact]
        public void GetHandlerRegistrations_WhenHandlerTypeIsNonAbstract_ThenReturnEntry()
        {
            // Arrange
            var types = new List<Type>
            {
                typeof(GenericHandler<,>),
                typeof(AbstractHandler),
                typeof(SimpleHandler)
            };

            // Act
            var mappings = types.GetHandlerRegistrations().ToList();

            // Assert
            mappings.Should().NotBeNull();
            mappings.Count.Should().Be(1);

            using (new AssertionScope())
            {
                mappings[0].InterfaceType.Should().Be(typeof(IRequestHandler<SimpleRequest, SimpleResponse>));
                mappings[0].ImplementationType.Should().Be(typeof(SimpleHandler));
            }
        }
    }
}