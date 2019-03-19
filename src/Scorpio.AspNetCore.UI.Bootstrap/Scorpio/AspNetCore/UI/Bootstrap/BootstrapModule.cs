using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;

namespace Scorpio.AspNetCore.UI.Bootstrap
{
    [DependsOn(typeof(AspNetCoreUiModule))]
    public class BootstrapModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {

            context.Services.RegisterAssemblyByConvention();
        }
    }
}
