using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.Extensions.DependencyInjection;
using Scorpio.Conventional;
using Scorpio.EventBus;

namespace Scorpio.EventBus.Conventional
{
    class EventHandlerConventionalAction : ConventionalActionBase
    {
        private readonly List<Type> _types;

        public EventHandlerConventionalAction(IConventionalConfiguration configuration,Assembly assembly) : base(configuration)
        {
            _types = assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && !t.IsGenericTypeDefinition).ToList();
        }

        protected override void Action(IConventionalContext context)
        {
            _types.FindAll(context.GetTypePredicate().Compile()).ForEach(
                        t => context.Services.Configure<EventBusOptions>(options => options.Handlers.Add(EventHandlerDescriptor.Describe(t, context.Get<IEventHandlerActivationTypeSelector>("HandlerActivationTypeSelector").Select(t)))));
        }
    }
}
