using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using Scorpio.DynamicProxy;
namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    internal sealed class KernelModule : ScorpioModule
    {

        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar(new BasicConventionalRegistrar());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention(typeof(KernelModule).Assembly);
        }

        public override void PostConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterConventionalInterceptor();
        }
    }
}
