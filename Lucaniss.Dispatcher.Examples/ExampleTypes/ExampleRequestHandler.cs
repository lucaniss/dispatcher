using System.Threading;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Examples.ExampleTypes
{
    internal class ExampleRequestHandler : IRequestHandler<ExampleRequest, ExampleRequestResponse>
    {
        public Task<ExampleRequestResponse> Handle(ExampleRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult(new ExampleRequestResponse($"({request.Input} + Output)"));
        }
    }
}