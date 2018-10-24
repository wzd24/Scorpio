using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Xunit;
using Shouldly;
using Scorpio.DependencyInjection;

namespace Microsoft.Extensions.DependencyInjection
{
    public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void RegisterAssembly_1()
        {
            var services = new ServiceCollection();
            services.RegisterAssembly(typeof(ServiceCollectionExtensions_Tests).Assembly, config =>
            {
                config.Where(t => t.Name == nameof(Service1)).AsDefault();
                config.Where(t => t.Name == nameof(Service2)).AsDefault();
            });
            services.Where(s => s.ServiceType == typeof(IService1)).SingleOrDefault().ShouldNotBeNull();
            services.Where(s => s.ServiceType == typeof(Service1)).SingleOrDefault().ShouldNotBeNull();
            services.Where(s => s.ServiceType == typeof(IService2)).SingleOrDefault().ShouldBeNull();
        }
        [Fact]
        public void RegisterAssembly_2()
        {
            var services = new ServiceCollection();
            services.RegisterAssembly(typeof(ServiceCollectionExtensions_Tests).Assembly, config =>
            {
                config.Where(t => t.Name == nameof(Service1)).AsSelf();
            });
            services.Where(s => s.ServiceType == typeof(IService1)).SingleOrDefault().ShouldBeNull();
            services.Where(s => s.ServiceType == typeof(Service1)).SingleOrDefault().ShouldNotBeNull();
            services.Where(s => s.ServiceType == typeof(IService2)).SingleOrDefault().ShouldBeNull();
        }

        [Fact]
        public void RegisterAssembly_3()
        {
            var services = new ServiceCollection();
            services.RegisterAssembly(typeof(ServiceCollectionExtensions_Tests).Assembly, config =>
            {
                config.Where(t => t.Name == nameof(Service1)).As<IService2>().Lifetime(ServiceLifetime.Singleton);
                config.Where(t => t.Name == nameof(Service1)).As<IService1>().Lifetime(ServiceLifetime.Transient);
            });
            services.Where(s => s.ServiceType == typeof(IService1)).SingleOrDefault().ShouldNotBeNull();
            services.Where(s => s.ServiceType == typeof(IService1)).SingleOrDefault().Lifetime.ShouldBe(ServiceLifetime.Transient);
            services.Where(s => s.ServiceType == typeof(Service1)).SingleOrDefault().ShouldBeNull();
            services.Where(s => s.ServiceType == typeof(IService2)).SingleOrDefault().ShouldNotBeNull();
            services.Where(s => s.ServiceType == typeof(IService2)).SingleOrDefault().Lifetime.ShouldBe(ServiceLifetime.Singleton);
            services.Where(s => s.ServiceType == typeof(IService3)).SingleOrDefault().ShouldBeNull();
        }

        [Fact]
        public void RegisterAssembly_4()
        {
            var services = new ServiceCollection();
            services.RegisterAssembly(typeof(ServiceCollectionExtensions_Tests).Assembly, config =>
            {
                config.Where(t => t.Name == nameof(ExposeService)).AsExposeService();
            });
            services.Where(s => s.ServiceType == typeof(IExposeService)).SingleOrDefault().ShouldNotBeNull();
            services.Where(s => s.ServiceType == typeof(IExposeService)).SingleOrDefault().Lifetime.ShouldBe(ServiceLifetime.Singleton);
        }

        [Fact]
        public void GetSingletonInstanceOrNull()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.GetSingletonInstanceOrNull<IService1>().ShouldBeOfType<Service1>().ShouldNotBeNull();
            services.GetSingletonInstanceOrNull<IService2>().ShouldBeNull();
        }

        [Fact]
        public void GetSingletonInstance()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IService1>(new Service1());
            services.GetSingletonInstance<IService1>().ShouldBeOfType<Service1>().ShouldNotBeNull();
            Should.Throw<InvalidOperationException>(() => services.GetSingletonInstance<IService2>());
        }
    }

    public interface IService1
    {

    }
    public interface IService2
    {

    }

    public interface IExposeService
    {

    }

    public interface IService3
    {

    }

    class Service1 : IService1, IService2, IService3
    {

    }

    class Service2 : IService1
    {

    }

    [ExposeServices(typeof(IExposeService), ServiceLifetime = ServiceLifetime.Singleton)]
    class ExposeService : IExposeService
    {

    }
}
