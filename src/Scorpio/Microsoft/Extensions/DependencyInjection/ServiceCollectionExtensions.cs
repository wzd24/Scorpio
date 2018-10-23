using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

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
        public static IServiceCollection RegisterAssembly(this IServiceCollection services, Assembly assembly, Action<RegisterAssemblyConfiguration> configureAction)
        {
            var config = new RegisterAssemblyConfiguration();
            configureAction(config);
            var types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition).ToList();
            config.Contexts.ForEach(
                context => types.Where(context.TypePredicate).ForEach(
                    t => context.ServiceSelectors.ForEach(selector => selector.Select(t).ForEach(
                        s => services.Add(ServiceDescriptor.Describe(s, t, context.ServiceLifetime))))));
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
        public static IServiceCollection ReplaceSingleton<TService>(this IServiceCollection services,TService instance)
            where TService:class
        {
            RemoveService<TService>(services);
            return services.AddSingleton(instance);
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
