using System;

namespace Lucaniss.Dispatcher.Cache
{
    internal struct CacheKey
    {
        public Type RequestType { get; init; }

        public Type ResponseType { get; init; }

        public override Int32 GetHashCode()
        {
            return HashCode.Combine(RequestType, ResponseType);
        }
    }
}