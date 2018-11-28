using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Domain;
using Scorpio.Modularity;
using System.Linq;
namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    [DependsOn(typeof(DomainModule))]
    public sealed class EntityFrameworkCoreModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            base.ConfigureServices(context);
        }
    }
}
