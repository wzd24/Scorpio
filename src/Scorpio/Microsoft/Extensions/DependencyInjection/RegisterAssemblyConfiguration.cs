using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class RegisterAssemblyConfiguration
    {
        /// <summary>
        /// 
        /// </summary>
        internal ICollection<RegisterAssemblyContext> Contexts { get; } = new List<RegisterAssemblyContext>();

        internal RegisterAssemblyContext GetContext()
        {
            var context = new RegisterAssemblyContext();
            Contexts.Add(context);
            return context;
        }
    }
}
