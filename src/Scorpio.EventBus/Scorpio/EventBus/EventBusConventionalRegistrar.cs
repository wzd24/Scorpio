using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Scorpio.Conventional;
using Scorpio.EventBus.Conventional;

namespace Scorpio.EventBus
{
    internal class EventBusConventionalRegistrar : IConventionalRegistrar
    {
        public void Register(IConventionalRegistrationContext context)
        {
            context.Services.RegisterEventHandler(context.Assembly, c =>
            {
                c.Where(t => t.IsAssignableTo<IEventHandler>() && t.IsClass && !t.IsAbstract).AutoActivation();
            });
        }
    }
}
