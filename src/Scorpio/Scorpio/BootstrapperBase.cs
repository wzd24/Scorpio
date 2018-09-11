using System;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    internal class BootstrapperBase : IBootstrapper, IModuleContainer
    {
        /// <summary>
        /// 
        /// </summary>
        public Type StartupModuleType { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<IModuleDescriptor> Modules { get; }

        public BootstrapperBase([NotNull] Type startupModuleType,
            [NotNull] IServiceCollection services,
            [CanBeNull] Action<BootstrapperCreationOptions> optionsAction)
        {
            StartupModuleType = startupModuleType;
            Services = services;
            var context = new BootstrapperCreationOptions(services);
            optionsAction(context);
            services.AddSingleton<IBootstrapper>(this);
            services.AddSingleton<IModuleContainer>(this);
        }

        /// <summary>
        /// 
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        public void Shutdown()
        {
        }


        public void Initialize()
        {
        }
    }
}
