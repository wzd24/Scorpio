using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Scorpio.EventBus;
using Scorpio.Conventional;
using Scorpio.EventBus.Conventional;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class EventBusServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterEventHandler(this IServiceCollection services, Assembly assembly, Action<IConventionalConfiguration> configureAction)
        {
            return services.DoConventionalAction<EventHandlerConventionalAction>(assembly, configureAction);
        }
    }
}
