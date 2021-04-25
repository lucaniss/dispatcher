using System;
using System.Threading;
using Lucaniss.Dispatcher.Exceptions;

namespace Lucaniss.Dispatcher.Dispatching
{
    public class DispatcherScopeGuard : IDisposable
    {
        private static readonly AsyncLocal<DispatcherScopeGuard> CurrentScope = new();

        private DispatcherScopeGuard()
        {
            if (CurrentScope.Value != null)
            {
                throw new DispatcherException("You cannot call dispatcher inside request handler pipeline.");
            }

            CurrentScope.Value = this;
        }

        public static DispatcherScopeGuard Create()
        {
            return new();
        }

        public void Dispose()
        {
            CurrentScope.Value = null;

            GC.SuppressFinalize(this);
        }
    }
}