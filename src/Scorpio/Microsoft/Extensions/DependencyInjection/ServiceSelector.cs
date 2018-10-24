﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using Scorpio.DependencyInjection;

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
    class ExposeServicesSelector : IRegisterAssemblyServiceSelector
    {
        public IEnumerable<Type> Select(Type componentType)
        {
           var attr= componentType.GetAttribute<ExposeServicesAttribute>(true);
            return attr.GetExposedServiceTypes(componentType);
        }
    }
}
