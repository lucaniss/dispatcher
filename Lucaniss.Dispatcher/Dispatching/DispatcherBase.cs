using System;
using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Cache;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Dispatching
{
    internal class DispatcherBase : IDispatcher
    {
        private readonly ConcurrentDictionary<CacheKey, Type> _types = new();

        private readonly IDependencyProvider _dependencyProvider;

        public DispatcherBase(IDependencyProvider dependencyProvider)
        {
            _dependencyProvider = dependencyProvider;
        }

        public async Task<TResponse> Dispatch<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            using (DispatcherScopeGuard.Create())
            {
                return await CreateWrapper<TResponse>(request.GetType())
                    .Handle(request, cancellationToken)
                    .ConfigureAwait(false);
            }
        }

        private IDispatcherScopeWrapper<TResponse> CreateWrapper<TResponse>(Type requestType)
        {
            var cacheKey = new CacheKey
            {
                RequestType = requestType,
                ResponseType = typeof(TResponse)
            };

            var wrapperType = _types.GetOrAdd(cacheKey, _ => typeof(DispatcherScopeWrapper<,>).MakeGenericType(cacheKey.RequestType, cacheKey.ResponseType));

            return (IDispatcherScopeWrapper<TResponse>) _dependencyProvider.Resolve(wrapperType);
        }
    }
}