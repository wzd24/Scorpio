using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.DependencyInjection;
namespace Scorpio.Threading
{
    /// <summary>
    /// 
    /// </summary>
    public class ThreadingModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.TryAddSingleton<ICancellationTokenProvider>(NullCancellationTokenProvider.Instance);
            context.Services.RegisterAssemblyByConventionOfType<ThreadingModule>();
        }
    }
}
