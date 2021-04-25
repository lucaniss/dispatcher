using System;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Tests.Dispatching
{
    public partial class DispatcherScopeWrapperTest
    {
        private class SimpleRequest : IRequest<SimpleResponse>
        {
            public String Text { get; }

            public SimpleRequest(String text)
            {
                Text = text;
            }
        }

        private class SimpleResponse
        {
            public String Text { get; }

            public SimpleResponse(String text)
            {
                Text = text;
            }
        }

        private class SimpleHandler : IRequestHandler<SimpleRequest, SimpleResponse>
        {
            public Task<SimpleResponse> Handle(SimpleRequest request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new SimpleResponse(request.Text + "_OUT"));
            }
        }

        private class SimpleHandlerDecorator<TRequest, TResponse> : IRequestHandlerDecorator<TRequest, SimpleResponse>
            where TRequest : SimpleRequest, IRequest<TResponse>
        {
            public async Task<SimpleResponse> Handle(TRequest request, Func<TRequest, CancellationToken, Task<SimpleResponse>> next, CancellationToken cancellationToken)
            {
                return new("PRE_" + (await next(request, cancellationToken)).Text + "_POST");
            }
        }
    }
}