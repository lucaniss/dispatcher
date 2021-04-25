using System.Diagnostics.CodeAnalysis;

namespace Lucaniss.Dispatcher.Requests
{
    [SuppressMessage("ReSharper", "UnusedTypeParameter", Justification = "Marker parameter")]
    public interface IRequest<TResponse>
    {
    }
}