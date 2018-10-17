using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegisterAssemblyContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceLifetime"></param>
        /// <returns></returns>
        public static RegisterAssemblyContext Lifetime(this RegisterAssemblyContext context, ServiceLifetime serviceLifetime)
        {
            context.ServiceLifetime = serviceLifetime;
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="serviceSelector"></param>
        /// <returns></returns>
        public static RegisterAssemblyContext As(this RegisterAssemblyContext context, IRegisterAssemblyServiceSelector serviceSelector)
        {
            context.ServiceSelectors.Add(serviceSelector);
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static RegisterAssemblyContext As<T>(this RegisterAssemblyContext context)
        {
            context.As(new TypeSelector<T>());
            return context;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static RegisterAssemblyContext AsDefault(this RegisterAssemblyContext context)
        {
            context.As(new DefaultInterfaceSelector());
            return context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public static RegisterAssemblyContext AsSelf(this RegisterAssemblyContext context)
        {
            context.As(new SelfSelector());
            return context;
        }
    }
}
