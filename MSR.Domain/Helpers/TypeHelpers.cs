using System;
using System.Collections.Generic;
using System.Text;

namespace MSR.Domain.Helpers
{
    public static class SystemTypeExtensions
    {
        public static bool ImplementsInterfaceOf<TInterface>(this Type classType) where TInterface : class
        {
            return !classType.IsInterface && !typeof(TInterface).IsNotInterface() && typeof(TInterface).IsAssignableFrom(classType);
        }

        public static bool IsNotInterface(this Type type)
        {
            return !type.IsInterface;
        }
    }
}
