using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.EventBus;
using Scorpio.Modularity;
using Scorpio.DependencyInjection.Conventional;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.EntityFrameworkCore.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(EventBusModule),typeof(EntityFrameworkCoreModule))]
    public class EFEventBusModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConventionOfType<EFEventBusModule>();
            base.ConfigureServices(context);
        }
    }
}
