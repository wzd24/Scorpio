using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using Scorpio.DependencyInjection.Conventional;
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
        public static IServiceCollection AddConventionalRegistrar(this IServiceCollection services, IConventionalRegistrar registrar)
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
          where T : IConventionalRegistrar
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
            GetOrCreateRegistrarList(services).ForEach(registrar => registrar.Register(context));
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static IServiceCollection AddExcludeServiceOfRegisterAssemblyByConvention(this IServiceCollection services, Predicate<Type> predicate)
        {
            DefaultInterfaceSelector.ExcludeServicePredicates.Add(predicate);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static IServiceCollection AddExcludeServiceOfRegisterAssemblyByConvention(this IServiceCollection services, Type type)
        {
            DefaultInterfaceSelector.ExcludeServicePredicates.Add(p => p == type);
            return services;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <typeparam name="TService"></typeparam>
        /// <returns></returns>
        public static IServiceCollection AddExcludeServiceOfRegisterAssemblyByConvention<TService>(this IServiceCollection services)
        {
            return AddExcludeServiceOfRegisterAssemblyByConvention(services, typeof(TService));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection RegisterAssemblyByConvention(this IServiceCollection services)
        {
            var assembly = Assembly.GetCallingAssembly();
            return RegisterAssemblyByConvention(services, assembly);
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
            return ConventionalRegistrarList.Registrars;
        }
    }
}
