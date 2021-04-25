using System;
using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Dispatching;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Tests.Dispatching
{
    partial class DispatcherBaseTest
    {
        private class SimpleRequest : IRequest<SimpleResponse>
        {
        }

        private class SimpleResponse
        {
            public String Text { get; init; }
        }

        private class FakeScopeWrapper : IDispatcherScopeWrapper<SimpleResponse>
        {
            public Task<SimpleResponse> Handle(IRequest<SimpleResponse> request, CancellationToken cancellationToken)
            {
                return Task.FromResult(new SimpleResponse
                {
                    Text = "Echo"
                });
            }
        }
    }
}