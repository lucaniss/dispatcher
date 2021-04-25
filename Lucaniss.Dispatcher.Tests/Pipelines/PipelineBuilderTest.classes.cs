using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Tests.Pipelines
{
    public partial class PipelineBuilderTest
    {
        internal class SimpleRequest : IRequest<SimpleResponse>
        {
        }

        internal class SimpleResponse
        {
        }

        internal class SimpleRequestHandlerDecorator1<TRequest, TResponse> : IRequestHandlerDecorator<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
        {
            [ExcludeFromCodeCoverage]
            public Task<TResponse> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResponse>> next, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        internal class SimpleRequestHandlerDecorator2<TRequest, TResponse> : IRequestHandlerDecorator<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
        {
            [ExcludeFromCodeCoverage]
            public Task<TResponse> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResponse>> next, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }
    }
}