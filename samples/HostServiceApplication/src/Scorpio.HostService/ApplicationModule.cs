using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
namespace Scorpio.HostService
{
    public sealed class ApplicationModule: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            context.Services.AddHostedService<HostedService>();
            base.ConfigureServices(context);
        }

        public override void Initialize(ApplicationInitializationContext context)
        {
            Console.WriteLine($"Module {nameof(ApplicationModule)} is initialized.");
            base.Initialize(context);
        }

        public override void Shutdown(ApplicationShutdownContext context)
        {
            base.Shutdown(context);
        }
    }
}
