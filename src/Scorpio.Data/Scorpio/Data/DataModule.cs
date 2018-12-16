using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class DataModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.Configure<DataFilterOptions>(options =>
            {
                options.RegiesterFilter(new SoftDeleteDataFilterDescriptor());
            });
            context.Services.AddSingleton(typeof(IDataFilter<>), typeof(DataFilter<>));
            context.Services.RegisterAssemblyByConventionOfType<DataModule>();
        }
    }
}
