using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.DependencyInjection.Conventional
{
    class DefaultInterfaceSelector : IRegisterAssemblyServiceSelector
    {
        public static ICollection<Predicate<Type>> ExcludeServicePredicates { get; } = new HashSet<Predicate<Type>>();

        public DefaultInterfaceSelector()
        {
        }

        public IEnumerable<Type> Select(Type componentType)
        {
            var services = componentType.GetInterfaces().Where(s => !ExcludeServicePredicates.Any(p=>p(s))).ToList();
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
    class ExposeServicesSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
           var attr= componentType.GetAttribute<ExposeServicesAttribute>(true);
            return attr.GetExposedServiceTypes(componentType);
        }
    }
}
