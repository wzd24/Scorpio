using Scorpio.Modularity;
using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Scorpio.Uow
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class UnitOfWorkModule:ScorpioModule
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConventionOfType<UnitOfWorkModule>();
            context.Services.TryAddTransient<IUnitOfWork, NullUnitOfWork>();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PostConfigureServices(ConfigureServicesContext context)
        {

            base.PostConfigureServices(context);
        }
    }
}
