using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity.Plugins;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Modularity
{
    public interface IModuleLoader
    {
        IModuleDescriptor[] LoadModules(
            IServiceCollection services,
            Type startupModuleType,
            PlugInSourceList plugInSources
            );
    }
}
