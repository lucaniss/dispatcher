using System;

namespace Lucaniss.Dispatcher.Exceptions
{
    public sealed class DispatcherException : Exception
    {
        public DispatcherException()
        {
        }

        public DispatcherException(String message)
            : base(message)
        {
        }

        public DispatcherException(String message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}