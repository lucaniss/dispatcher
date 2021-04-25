using System;
using System.Runtime.Serialization;

namespace Lucaniss.Dispatcher.Exceptions
{
    [Serializable]
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

        private DispatcherException(SerializationInfo serializationInfo, StreamingContext streamingContext)
            : base(serializationInfo, streamingContext)
        {
        }
    }
}