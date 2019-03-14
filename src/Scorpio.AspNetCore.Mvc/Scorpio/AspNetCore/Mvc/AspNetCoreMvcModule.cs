using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;

namespace Scorpio.AspNetCore.Mvc
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(AspNetCoreModule))]
    public sealed class AspNetCoreMvcModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Options<MvcOptions>().PreConfigure<IServiceProvider>(
                (options, serviceProvider) => options.AddScorpio(serviceProvider));
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
