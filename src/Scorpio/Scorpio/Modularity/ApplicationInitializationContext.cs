using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ApplicationInitializationContext
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        internal ApplicationInitializationContext(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }
    }
}
