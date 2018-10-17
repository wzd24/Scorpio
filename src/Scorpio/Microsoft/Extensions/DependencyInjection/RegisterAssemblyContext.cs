using System;
using System.Collections.Generic;
using System.Text;
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public class RegisterAssemblyContext
    {
        internal Func<Type, bool> TypePredicate { get; set; }

        internal ICollection<IRegisterAssemblyServiceSelector> ServiceSelectors { get; set; }

        internal ServiceLifetime ServiceLifetime { get; set; }
        
        internal RegisterAssemblyContext()
        {
            ServiceSelectors = new HashSet<IRegisterAssemblyServiceSelector>();
            ServiceLifetime = ServiceLifetime.Transient;
        }
    }
}
