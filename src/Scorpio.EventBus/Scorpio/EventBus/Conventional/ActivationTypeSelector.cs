﻿using Scorpio.DependencyInjection;
using Scorpio.EventBus;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Scorpio.EventBus.Conventional
{
    internal class ActivationTypeSelector : IEventHandlerActivationTypeSelector
    {
        private readonly EventHandlerActivationType _activationType;

        public ActivationTypeSelector(EventHandlerActivationType  activationType)
        {
            _activationType = activationType;
        }

        EventHandlerActivationType IEventHandlerActivationTypeSelector.Select(Type handlerType)
        {
            return _activationType;
        }
    }

    internal class ExposeActivationTypeSelector : IEventHandlerActivationTypeSelector
    {

        EventHandlerActivationType IEventHandlerActivationTypeSelector.Select(Type handlerType)
        {
            if (handlerType.IsAssignableTo<IDependency>())
            {
                return EventHandlerActivationType.ByServiceProvider;
            }
            return EventHandlerActivationType.Transient;
        }
    }
}
