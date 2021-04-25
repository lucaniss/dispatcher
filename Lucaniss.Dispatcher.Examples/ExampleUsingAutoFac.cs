using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using Autofac;
using Lucaniss.Dispatcher.Examples.ExampleTypes;
using Lucaniss.Dispatcher.Extensions.AutoFac;

namespace Lucaniss.Dispatcher.Examples
{
    internal static class ExampleUsingAutoFac
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
            var containerBuilder = new ContainerBuilder();

            containerBuilder.AddDispatcher(AssembliesToScan, builder =>
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

            return containerBuilder
                .Build()
                .Resolve<IDispatcher>();
        }
    }
}