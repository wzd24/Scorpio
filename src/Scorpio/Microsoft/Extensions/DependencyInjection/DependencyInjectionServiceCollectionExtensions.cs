using Microsoft.Extensions.DependencyInjection;
using Scorpio.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    /// 
    /// </summary>
    public static class DependencyInjectionServiceCollectionExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="registrar"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalRegistrar(this IServiceCollection services, IConventionalDependencyRegistrar registrar)
        {
            GetOrCreateRegistrarList(services).Add(registrar);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddConventionalRegistrar<T>(this IServiceCollection services)
          where T: IConventionalDependencyRegistrar
        {
            return services.AddConventionalRegistrar(Activator.CreateInstance<T>());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="assembly"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyByConvention(this IServiceCollection services, Assembly assembly)
        {
            var context = new ConventionalRegistrationContext(assembly, services);
            GetOrCreateRegistrarList(services).ForEach(registrar => registrar.RegisterAssembly(context));
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyByConventionOfType<T>(this IServiceCollection services)
        {
            return services.RegisterAssemblyByConvention(typeof(T).GetTypeInfo().Assembly);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private static ConventionalRegistrarList GetOrCreateRegistrarList(IServiceCollection services)
        {
            var conventionalRegistrars = services.GetSingletonInstanceOrNull<ConventionalRegistrarList>();
            if (conventionalRegistrars == null)
            {
                conventionalRegistrars = new ConventionalRegistrarList();
                services.AddSingleton(conventionalRegistrars);
            }
            return conventionalRegistrars;
        }
    }
}
