using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Uow
{
    [DependsOn(typeof(UnitOfWorkModule))]
   public class UnitOfWork_Attribution_Module : ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddTransient<IUnitOfWorkAttributionTestService, UnitOfWorkAttributionTestService>();
            base.ConfigureServices(context);
        }
    }

}
