using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
namespace Scorpio.ConsoleApplication
{
    public sealed class ApplicationModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
