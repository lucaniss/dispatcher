﻿using System.Threading;
using System.Threading.Tasks;

namespace Lucaniss.Dispatcher.Requests
{
    public interface IRequestHandler<in TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}