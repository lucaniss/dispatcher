using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Lucaniss.Dispatcher.Examples.ExampleTypes;
using Lucaniss.Dispatcher.Extensions.ServiceProvider;
using Microsoft.Extensions.DependencyInjection;

namespace Lucaniss.Dispatcher.Examples
{
    internal static class ExampleUsingServiceProvider
    {
        public static IReadOnlyList<Assembly> AssembliesToScan =>
            new List<Assembly>
            {
                typeof(ExampleUsingAutoFac).Assembly
            };

        public static async Task Execute()
        {
            Console.WriteLine((await CreateDispatcher().Dispatch(new ExampleRequest("Hello World"))).Output);
        }

        private static IDispatcher CreateDispatcher()
        {
            var serviceCollection = new ServiceCollection();

            serviceCollection.AddDispatcher(AssembliesToScan, builder =>
            {
                builder
                    .Build()
                    .ForAny()
                    .UseDecorators(d =>
                    {
                        d.Use(typeof(ExampleRequestHandlerDecorator1<,>));
                        d.Use(typeof(ExampleRequestHandlerDecorator2<,>));
                    });
            });

            return serviceCollection
                .BuildServiceProvider()
                .GetRequiredService<IDispatcher>();
        }
    }
}