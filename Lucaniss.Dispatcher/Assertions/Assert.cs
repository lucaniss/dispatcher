using System;

namespace Lucaniss.Dispatcher.Assertions
{
    internal static class Assert
    {
        public static void NotNull<T>([ValidatedNotNull] T value, String name)
            where T : class
        {
            if (value == null)
            {
                throw new ArgumentNullException(name);
            }
        }
    }
}