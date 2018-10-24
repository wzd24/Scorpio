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

        internal ICollection<IRegisterAssemblyServiceSelector> ServiceSelectors { get;  }
        internal IRegisterAssemblyLifetimeSelector LifetimeSelector { get; set; }
        
        internal RegisterAssemblyContext()
        {
            ServiceSelectors = new HashSet<IRegisterAssemblyServiceSelector>();
            LifetimeSelector =new LifetimeSelector( ServiceLifetime.Transient);
        }
    }
}
