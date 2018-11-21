using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssembly(this IServiceCollection services, Assembly assembly, Action<IConventionalConfiguration> configureAction)
        {
            return DoConventionalAction<ConventionalDependencyAction>(services, assembly, configureAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static IServiceCollection DoConventionalAction<TAction>(this IServiceCollection services, Assembly assembly, Action<IConventionalConfiguration> configureAction) where TAction:ConventionalActionBase
        {
            var config = new ConventionalConfiguration(services);
            configureAction(config);
            var action= Activator.CreateInstance(typeof(TAction), config, assembly) as TAction;
            action.Action();
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstanceOrNull<T>(this IServiceCollection services)
        {
            return (T)services
                .FirstOrDefault(d => d.ServiceType == typeof(T))
                ?.ImplementationInstance;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static T GetSingletonInstance<T>(this IServiceCollection services)
        {
            var service = services.GetSingletonInstanceOrNull<T>();
            if (service == null)
            {
                throw new InvalidOperationException("Could not find singleton service: " + typeof(T).AssemblyQualifiedName);
            }

            return service;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceSingleton<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            RemoveService<TService>(services);
            return services.AddSingleton<TService, TImplementation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        /// <param name="instance"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection services, TService instance)
            where TService : class
        {
            RemoveService<TService>(services);
            return services.AddSingleton(instance);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceTransient<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            RemoveService<TService>(services);
            return services.AddTransient<TService, TImplementation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <typeparam name="TImplementation"></typeparam>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection ReplaceScoped<TService, TImplementation>(this IServiceCollection services)
            where TService : class
            where TImplementation : class, TService
        {
            RemoveService<TService>(services);
            return services.AddScoped<TService, TImplementation>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TService"></typeparam>
        /// <param name="services"></param>
        public static IServiceCollection RemoveService<TService>(IServiceCollection services) where TService : class
        {
            var old = services.FirstOrDefault(s => s.ServiceType == typeof(TService));
            if (old != null)
            {
                services.Remove(old);
            }
            return services;
        }
    }
}
