using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection.Conventional;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Security;
using Scorpio.Uow;
using Scorpio.Threading;

namespace Scorpio.AspNetCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(SecurityModule))]
    [DependsOn(typeof(UnitOfWorkModule))]
    [DependsOn(typeof(ThreadingModule))]
    public sealed class AspNetCoreModule : ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            base.ConfigureServices(context);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void Initialize(ApplicationInitializationContext context)
        {
            base.Initialize(context);
        }
    }
}
