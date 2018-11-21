using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DynamicProxy;
using Microsoft.Extensions.DependencyInjection;
namespace Scorpio.Uow
{
    [DependsOn(typeof(UnitOfWorkModule))]
   public class UnitOfWork_Conventional_Module: ScorpioModule
    {
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddConventionalRegistrar<UnitOfWorkConventionaInterceptorRegistrar>();
            context.Services.AddTransient<IUnitOfWorkConventionalTestService, UnitOfWorkConventionalTestService>();
            base.ConfigureServices(context);
        }
    }

    class UnitOfWorkConventionaInterceptorRegistrar : IConventionaInterceptorRegistrar
    {
        public void Register(IConventionaInterceptorContext context)
        {
            context.Add<IUnitOfWorkConventionalTestService, UnitOfWorkInterceptor>();
        }
    }
}
