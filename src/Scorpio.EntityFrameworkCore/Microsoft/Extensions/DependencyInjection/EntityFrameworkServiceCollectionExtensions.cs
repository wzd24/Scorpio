using Scorpio.EntityFrameworkCore;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class EntityFrameworkServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddScorpioDbContext<TDbContext>(this IServiceCollection services,
            Action<IScorpioDbContextOptionsBuilder> builderAction)
            where TDbContext:ScorpioDbContext<TDbContext>
        {
            services.AddMemoryCache().AddLogging();
            var options = new ScorpioDbContextOptionsBuilder();
            builderAction?.Invoke(options);
            return services;
        }
    }
}
