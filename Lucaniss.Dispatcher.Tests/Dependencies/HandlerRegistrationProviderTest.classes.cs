using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Tests.Dependencies
{
    public partial class HandlerRegistrationProviderTest
    {
        internal class SimpleRequest : IRequest<SimpleResponse>
        {
        }

        internal class SimpleResponse
        {
        }

        internal class GenericHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
        {
            [ExcludeFromCodeCoverage]
            public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        internal abstract class AbstractHandler : IRequestHandler<SimpleRequest, SimpleResponse>
        {
            [ExcludeFromCodeCoverage]
            public Task<SimpleResponse> Handle(SimpleRequest request, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        internal class SimpleHandler : AbstractHandler
        {
        }

        internal class SimpleHandlerDerived1 : SimpleHandler
        {
        }

        internal class SimpleHandlerDerived2 : SimpleHandlerDerived1
        {
        }
    }
}