using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Shouldly;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Scorpio.DependencyInjection
{
   public class ServiceCollectionExtensions_Tests
    {
        [Fact]
        public void AddConventionalRegistrar()
        {
            var services= new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            services.GetSingletonInstance<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
        }

        [Fact]
        public void RegisterAssemblyByConvention()
        {
            var services = new ServiceCollection();
            var registrar = new EmptyConventionalDependencyRegistrar();
            services.AddConventionalRegistrar(registrar);
            services.GetSingletonInstance<ConventionalRegistrarList>().Contains(registrar).ShouldBeTrue();
            var assembly = typeof(ServiceCollectionExtensions_Tests).Assembly;
            services.RegisterAssemblyByConvention(assembly);
            registrar.RegisterAssemblyInvoked.ShouldBeTrue();
            registrar.Assembly.ShouldBe(assembly);
        }
    }

    class EmptyConventionalDependencyRegistrar : IConventionalDependencyRegistrar
    {
        public bool RegisterAssemblyInvoked { get; set; }

        public Assembly Assembly { get; set; }
        public void RegisterAssembly(IConventionalRegistrationContext context)
        {
            Assembly = context.Assembly;
            RegisterAssemblyInvoked = true;
        }
    }
}
