using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Dispatching
{
    internal interface IDispatcherScopeWrapper<TResponse>
    {
        Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken);
    }
}