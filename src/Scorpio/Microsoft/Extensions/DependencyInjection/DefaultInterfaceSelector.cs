using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    class DefaultInterfaceSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            var services = componentType.GetInterfaces().Where(s => componentType.Name.EndsWith(s.Name.RemovePreFix("I"))).ToList();
            services.Add(componentType);
            return services;
        }
    }

    class SelfSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            return new Type[] { componentType };
        }
    }

    class TypeSelector<T> : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
            return new Type[] { typeof(T) };
        }
    }
}
