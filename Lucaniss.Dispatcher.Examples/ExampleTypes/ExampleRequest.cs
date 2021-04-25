using System;
using Lucaniss.Dispatcher.Requests;

namespace Lucaniss.Dispatcher.Examples.ExampleTypes
{
    internal class ExampleRequest : IRequest<ExampleRequestResponse>
    {
        public String Input { get; }

        public ExampleRequest(String input)
        {
            Input = input;
        }
    }
}