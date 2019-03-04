using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.EventBus.Conventional;
using Scorpio.DependencyInjection.Conventional;
namespace Scorpio.EventBus
{
    internal class EventBusConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.Services.RegisterAssembly(context.Assembly, config =>
             {
                 config.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsAssignableTo<ISingletonDependency>()).AsSelf().Lifetime(ServiceLifetime.Singleton);
                 config.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsAssignableTo<ITransientDependency>()).AsSelf().Lifetime(ServiceLifetime.Transient);
                 config.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsAssignableTo<IScopedDependency>()).AsSelf().Lifetime(ServiceLifetime.Scoped);
             });
            context.Services.RegisterEventHandler(context.Assembly, c =>
            {
                c.Where(t => t.IsAssignableTo<IEventHandler>()).AutoActivation();
            });
        }
    }
}
