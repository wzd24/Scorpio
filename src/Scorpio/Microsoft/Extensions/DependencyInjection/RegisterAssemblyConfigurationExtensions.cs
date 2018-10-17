using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class RegisterAssemblyConfigurationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static RegisterAssemblyContext Where( this RegisterAssemblyConfiguration configuration, Func<Type, bool> predicate)
        {
            var context = configuration.GetContext();
            context.TypePredicate = predicate;
            return context;
        }
    }
}
