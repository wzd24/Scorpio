using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public class ConfigureServicesContext
    {
        internal ConfigureServicesContext(IServiceCollection services)
        {
            Services = services;
        }

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }
    }
}
