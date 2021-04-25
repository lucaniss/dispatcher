using System;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Examples.ExampleTypes
{
    internal class ExampleRequestHandlerDecorator2<TRequest, TResponse> : IRequestHandlerDecorator<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : ExampleRequestResponse
    {
        public async Task<TResponse> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResponse>> next, CancellationToken cancellationToken)
        {
            var result = await next(request, cancellationToken);

            result.Update("(OuterDecoratorBefore + " + (await next(request, cancellationToken)).Output + " + OuterDecoratorAfter)");

            return result;
        }
    }
}