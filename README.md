# Dispatcher

![GitHub](https://img.shields.io/github/license/Lucaniss/Dispatcher)
![GitHub last commit](https://img.shields.io/github/last-commit/Lucaniss/dispatcher?)
![Nuget](https://img.shields.io/nuget/v/Lucaniss.Dispatcher)

Dispatcher is a simple Command Dispatcher design pattern implementation that links the Request with the appropriate Request-Handler.

#### Installation

If you want to install Dispatcher library then just use Nuget Package Manager console:

    Install-Package Lucaniss.Dispatcher -Version 1.0.1
    
#### Integration with Dependency Injection container

Dispatcher is designed for use with Dependency Injection containers. 
That is why we have provided a few additional packages that can help you integrate with popular solutions:

    Install-Package Lucaniss.Dispatcher.Extensions.ServiceProvider -Version 1.0.1
    Install-Package Lucaniss.Dispatcher.Extensions.AutoFac -Version 1.0.1
    
## Setup and configuration

If you want to attach Dispatcher to your solution then call AddDispatcher() extension method for selected Dependency Injection container.
You have to provide a list of assemblies which will be scanned in search of classes that implements those specific interfaces:

1. `IRequestHandler` - generic interface for request handlers
2. `IRequestHandlerDecorator` - generic interface for request handler decorators

Depending on the selected extension, this may slightly differ.
But here is a sample code for Microsoft ServiceProvider library extension:

```c#
// Create your Dependency Injection container builder.
var services = new ServiceCollection();

// Call AddDispatcher() extension method.
// Of course this is just an example and you should provide actual list of assemblies here.
services.AddDispatcher(new List<Assembly>());

// Build Dependency Injection container.
var provider = services.BuildServiceProvider();

// Resolve IDispatcher instance from container.
var dispatcher = provider.GetRequiredService<IDispatcher>();
```

## Example code

Using Dispatcher is very easy. To show this, let's use our library to write simple *Hello World* example:

#### 1. Create request and response

First, we need to create a few classes that will store the data for the request and response respectively.
Request class should implement the `IRequest<TResponse>` interface, where `TResponse` is the type of the response object.

```c#
public class HelloRequest : IRequest<HelloResponse>
{
    public String Name { get; init; }
}

public class HelloResponse
{
    public String Message { get; init; }
}
```

#### 2. Create handler

The next step is to create a class that will handle our request and return its result.
This class must implement `IRequestHandler<TRequest,TResponse>` interface where `TRequest` is our request object type and `TResponse` is our response object type.

```c#
public class HelloHandler : IRequestHandler<HelloRequest, HelloResponse>
{
    public Task<HelloResponse> Handle(HelloRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new HelloResponse
        {
             Message = $"Hello {request.Name}!"
        });
    }
}
```

#### 3. Dispatch request (with simple ASP.NET Web API controller)

The last step is quite obvious. We need to send our request object to appropriate handler using our Dispatcher.
To do this let's create simple controller where we inject `IDispatcher` instance. Then we will call Dispatch() method inside our controller action.

```c#
[Route("api/[controller]")]
[ApiController]
public class HelloController : ControllerBase
{
    private readonly IDispatcher _dispatcher;
    
    public HelloController(IDispatcher dispatcher)
    {
        _dispatcher = dispatcher;
    }

    [HttpGet("{name}")]
    public Task<String> GetMessage(String name)
    {
        return _dispatcher.Dispatch(new HelloRequest
        {
            Name = name
        });
    }
}
```

## Summary

And that's all! (Hmm, well, maybe not really, there are also some pretty cool decorator features :wink:)

Check out this repository... If you have any ideas on how to improve this simple library, don't hesitate and collaborate! :smile:
