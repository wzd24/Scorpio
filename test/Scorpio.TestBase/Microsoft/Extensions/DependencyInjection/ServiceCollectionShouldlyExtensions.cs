﻿using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ServiceCollectionShouldlyExtensions
    {
        public static void ShouldContainTransient(this IServiceCollection services, Type serviceType, Type implementationType = null)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.ImplementationType.ShouldBe(implementationType ?? serviceType);
            serviceDescriptor.ImplementationFactory.ShouldBeNull();
            serviceDescriptor.ImplementationInstance.ShouldBeNull();
            serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Transient);
        }

        public static void ShouldContainSingleton(this IServiceCollection services, Type serviceType, Type implementationType = null)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.ImplementationType.ShouldBe(implementationType ?? serviceType);
            serviceDescriptor.ImplementationFactory.ShouldBeNull();
            serviceDescriptor.ImplementationInstance.ShouldBeNull();
            serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Singleton);
        }

        public static void ShouldContainScoped(this IServiceCollection services, Type serviceType, Type implementationType = null)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.ImplementationType.ShouldBe(implementationType ?? serviceType);
            serviceDescriptor.ImplementationFactory.ShouldBeNull();
            serviceDescriptor.ImplementationInstance.ShouldBeNull();
            serviceDescriptor.Lifetime.ShouldBe(ServiceLifetime.Scoped);
        }

        public static void ShouldContain(this IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);

            serviceDescriptor.ShouldNotBeNull();
            serviceDescriptor.ImplementationType.ShouldBe(implementationType);
            serviceDescriptor.ImplementationFactory.ShouldBeNull();
            serviceDescriptor.ImplementationInstance.ShouldBeNull();
            serviceDescriptor.Lifetime.ShouldBe(lifetime);
        }

        public static void ShouldNotContainService(this IServiceCollection services, Type serviceType)
        {
            var serviceDescriptor = services.SingleOrDefault(s => s.ServiceType == serviceType);

            serviceDescriptor.ShouldBeNull();
        }
    }
}
