using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.EventBus.TestClasses
{
    [DependsOn(typeof(EventBusModule))]
    public class ServicedEventHandlerModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            
        }
    }
}
