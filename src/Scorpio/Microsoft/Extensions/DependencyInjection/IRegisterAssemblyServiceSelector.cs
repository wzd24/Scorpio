using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public interface IRegisterAssemblyServiceSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        IEnumerable<Type> Select(Type componentType);
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IRegisterAssemblyLifetimeSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="componentType"></param>
        /// <returns></returns>
        ServiceLifetime Select(Type componentType);
    }

}
