using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using AspectCore.Extensions;
using AspectCore.Injector;
using AspectCore.Extensions.DependencyInjection;
using System.Reflection;
using AspectCore.DynamicProxy;
using AspectCore.Configuration;

namespace Scorpio
{
    /// <summary>
    /// 
    /// </summary>
    public abstract class Bootstrapper : IBootstrapper, IModuleContainer
    {

        /// <summary>
        /// 
        /// </summary>
        public Type StartupModuleType { get; }

        private readonly BootstrapperCreationOptions _options;

        /// <summary>
        /// 
        /// </summary>
        public IServiceCollection Services { get; }

        /// <summary>
        /// 
        /// </summary>
        public IServiceProvider ServiceProvider { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        public IReadOnlyList<IModuleDescriptor> Modules { get; }

        /// <summary>
        /// 
        /// </summary>
        internal protected IModuleLoader ModuleLoader { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <param name="services"></param>
        /// <param name="optionsAction"></param>
        protected Bootstrapper(Type startupModuleType, IServiceCollection services, Action<BootstrapperCreationOptions> optionsAction)
        {
            Services = services;
            StartupModuleType = startupModuleType;
            _options = new BootstrapperCreationOptions(services);
            ModuleLoader = new ModuleLoader();
            optionsAction(_options);
            ConfigureCoreService(services);
            Modules = LoadModules();
            ConfigureServices();
        }

        private void ConfigureCoreService(IServiceCollection services)
        {
            services.AddLogging();
            services.AddSingleton<IBootstrapper>(this);
            services.AddSingleton<IModuleContainer>(this);
            services.AddSingleton(ModuleLoader);
            services.AddSingleton<IModuleManager, ModuleManager>();
        }

        private void ConfigureServices()
        {
            var context = new ConfigureServicesContext(Services);
            Services.AddSingleton(context);
            Modules.ForEach(m => m.Instance.PreConfigureServices(context));
            Modules.ForEach(m => m.Instance.ConfigureServices(context));
            Modules.ForEach(m => m.Instance.PostConfigureServices(context));
        }

        private IReadOnlyList<IModuleDescriptor> LoadModules()
        {
            return ModuleLoader.LoadModules(Services, StartupModuleType, _options.PlugInSources);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        protected void SetServiceProvider(IServiceProvider serviceProvider)
        {
            ServiceProvider = serviceProvider;
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Dispose()
        {
            Shutdown();
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Initialize()
        {
            InitializeModules();
        }

        /// <summary>
        /// 
        /// </summary>
        protected virtual void InitializeModules()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .InitializeModules(new ApplicationInitializationContext(scope.ServiceProvider));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public virtual void Shutdown()
        {
            using (var scope = ServiceProvider.CreateScope())
            {
                scope.ServiceProvider
                    .GetRequiredService<IModuleManager>()
                    .ShutdownModules(new ApplicationShutdownContext(scope.ServiceProvider));
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <param name="optionsAction"></param>
        /// <returns></returns>
        public static IBootstrapper Create<TStartupModule>(Action<BootstrapperCreationOptions> optionsAction) where TStartupModule : IScorpioModule
        {
            return Create(typeof(TStartupModule), optionsAction);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="optionsAction"></param>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static IBootstrapper Create(Type startupModuleType, Action<BootstrapperCreationOptions> optionsAction)
        {
            if (!startupModuleType.IsAssignableTo<IScorpioModule>())
            {
                throw new ArgumentException($"{nameof(startupModuleType)} should be derived from {typeof(IScorpioModule)}");
            }
            var services = new ServiceCollection();
            var bootstrapper = new InternalBootstrapper(startupModuleType, services, optionsAction);
            bootstrapper.SetServiceProvider(services.BuildDynamicProxyServiceProvider());
            return bootstrapper;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startupModuleType"></param>
        /// <returns></returns>
        public static IBootstrapper Create(Type startupModuleType)
        {
            return Create(startupModuleType, o => { });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TStartupModule"></typeparam>
        /// <returns></returns>
        public static IBootstrapper Create<TStartupModule>() where TStartupModule : IScorpioModule
        {
            return Create(typeof(TStartupModule));
        }
    }
}
