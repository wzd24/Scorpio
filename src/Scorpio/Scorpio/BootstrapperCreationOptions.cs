using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;
using Scorpio.Modularity.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class BootstrapperCreationOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public PlugInSourceList PlugInSources { get; }

        internal BootstrapperCreationOptions(IServiceCollection services)
        {
            Services = Check.NotNull(services, nameof(services));
            PlugInSources = new PlugInSourceList();
        }
    }
}
