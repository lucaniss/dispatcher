using System;
using System.Threading.Tasks;

namespace Lucaniss.Dispatcher.Examples
{
    internal class Program
    {
        private static async Task Main()
        {
            // 1. Microsoft.ServiceProvider

            Console.WriteLine();
            Console.WriteLine("Example using Microsoft.ServicProvider:");

            await ExampleUsingServiceProvider.Execute();

            // 2. AutoFac

            Console.WriteLine();
            Console.WriteLine("Example using AutoFac:");

            await ExampleUsingAutoFac.Execute();
        }
    }
}