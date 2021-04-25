using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Cache;
using Lucaniss.Dispatcher.Configurations;
using Lucaniss.Dispatcher.Dependencies;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Dispatching
{
    internal class DispatcherScopeWrapper<TRequest, TResponse> : IDispatcherScopeWrapper<TResponse>
        where TRequest : class, IRequest<TResponse>
    {
        private readonly ConcurrentDictionary<CacheKey, IReadOnlyList<Type>> _decoratorTypesCache = new();

        private readonly IRequestHandler<TRequest, TResponse> _handler;
        private readonly IConfigurationResolver _configurationResolver;
        private readonly IDependencyProvider _dependencyProvider;

        public DispatcherScopeWrapper(
            IRequestHandler<TRequest, TResponse> handler,
            IConfigurationResolver configurationResolver,
            IDependencyProvider dependencyProvider)
        {
            _handler = handler;
            _configurationResolver = configurationResolver;
            _dependencyProvider = dependencyProvider;
        }

        public Task<TResponse> Handle(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            return InvokeHandler((TRequest) request, cancellationToken);
        }

        private Task<TResponse> InvokeHandler(TRequest request, CancellationToken cancellationToken)
        {
            var cacheKey = new CacheKey
            {
                RequestType = typeof(TRequest),
                ResponseType = typeof(TResponse)
            };

            var decoratorTypes = _decoratorTypesCache.GetOrAdd(cacheKey, _ =>
            {
                return _configurationResolver
                    .ResolveDecoratorTypes(cacheKey.RequestType, cacheKey.ResponseType)
                    .Select(t => t.MakeGenericType(cacheKey.RequestType, cacheKey.ResponseType))
                    .ToList();
            });

            var decorators = decoratorTypes.Select(t => (IRequestHandlerDecorator<TRequest, TResponse>) _dependencyProvider.Resolve(t));

            Func<TRequest, CancellationToken, Task<TResponse>> baseFunc = _handler.Handle;

            foreach (var decorator in decorators)
            {
                var nextFunc = baseFunc;

                Task<TResponse> DecoratorFunc(TRequest req, CancellationToken token) => decorator.Handle(req, nextFunc, token);

                baseFunc = DecoratorFunc;
            }

            return baseFunc(request, cancellationToken);
        }
    }
}