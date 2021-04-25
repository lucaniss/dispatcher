using System;

namespace Lucaniss.Dispatcher.Examples.ExampleTypes
{
    internal class ExampleRequestResponse
    {
        public String Output { get; private set; }

        public ExampleRequestResponse(String output)
        {
            Output = output;
        }

        public void Update(String output)
        {
            Output = output;
        }
    }
}