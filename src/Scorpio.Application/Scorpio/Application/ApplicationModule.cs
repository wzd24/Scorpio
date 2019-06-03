using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Domain;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Application
{
    [DependsOn(typeof(DomainModule))]
    public class ApplicationModule: ScorpioModule
    {
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<ConventionaInterceptorRegistrar>();
            base.PreConfigureServices(context);
        }
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }
    }
}
