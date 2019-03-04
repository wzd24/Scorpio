﻿using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Scorpio.EntityFrameworkCore;
using Scorpio.EntityFrameworkCore.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class EntityFrameworkServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="builderAction"></param>
        /// <returns></returns>
        public static IServiceCollection AddScorpioDbContext<TDbContext>(this IServiceCollection services,
            Action<IScorpioDbContextOptionsBuilder<TDbContext>> builderAction)
            where TDbContext : ScorpioDbContext<TDbContext>
        {
            services.AddMemoryCache().AddLogging();
            var options = new ScorpioDbContextOptionsBuilder<TDbContext>(services);
            builderAction?.Invoke(options);
            services.TryAddTransient(serviceProvider => DbContextOptionsFactory.Create(serviceProvider, options));
            services.AddTransient<DbContextOptions>(p => p.GetRequiredService<DbContextOptions<TDbContext>>());
            services.AddTransient<TDbContext>();
            new EfCoreRepositoryRegistrar<TDbContext>(options).RegisterRepositories();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="THandler"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddSaveChangeHandler<THandler>(this IServiceCollection services)
            where THandler : class, IOnSaveChangeHandler
        {
            services.TryAddEnumerable(ServiceDescriptor.Transient<IOnSaveChangeHandler, THandler>());
            return services;
        }
    }
}
