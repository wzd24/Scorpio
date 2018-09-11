using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using JetBrains.Annotations;

namespace Scorpio.Modularity
{
    /// <summary>
    /// 
    /// </summary>
    public interface IScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void PreCOnfigureServices([NotNull]ConfigureServicesContext context);
        /// <summary>
        /// Adds services to the container. 
        /// </summary>
        /// <param name="context"></param>
        void ConfigureServices([NotNull]ConfigureServicesContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void PostConfigureServices([NotNull]ConfigureServicesContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnPreApplicationInitialization([NotNull] ApplicationInitializationContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnApplicationInitialization([NotNull] ApplicationInitializationContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnPostApplicationInitialization([NotNull] ApplicationInitializationContext context);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        void OnApplicationShutdown([NotNull] ApplicationShutdownContext context);

    }
}
