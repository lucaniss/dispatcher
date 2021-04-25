using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Tests.Configurations
{
    public partial class ConfigurationResolverTest
    {
        internal class SimpleRequest : IRequest<SimpleResponse>
        {
        }

        internal class SimpleResponse
        {
        }

        internal class SimpleDecorator1<TRequest, TResponse> : IRequestHandlerDecorator<TRequest, TResponse>
            where TRequest : IRequest<TResponse>
        {
            [ExcludeFromCodeCoverage]
            public Task<TResponse> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResponse>> next, CancellationToken cancellationToken)
            {
                throw new NotImplementedException();
            }
        }

        internal class SimpleDecorator2<TRequest, TResponse> : IRequestHandlerDecorator<TRequest, TResponse>
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