using System;
using System.Threading;
using System.Threading.Tasks;

namespace Lucaniss.Dispatcher.Requests
{
    public interface IRequestHandlerDecorator<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, Func<TRequest, CancellationToken, Task<TResponse>> next, CancellationToken cancellationToken);
    }
}