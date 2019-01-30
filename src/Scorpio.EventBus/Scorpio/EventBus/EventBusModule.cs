﻿using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.Modularity;
using Scorpio.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class EventBusModule: ScorpioModule
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void PreConfigureServices(ConfigureServicesContext context)
        {
            context.Services.AddExcludeServiceOfRegisterAssemblyByConvention<IEventHandler>();
            context.Services.AddConventionalRegistrar<EventBusConventionalRegistrar>();
            base.PreConfigureServices(context);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public override void ConfigureServices(ConfigureServicesContext context)
        {
            context.Services.RegisterAssemblyByConvention();
            context.Services.TryAddSingleton<IEventBus, LocalEventBus>();
        }
    }
}
