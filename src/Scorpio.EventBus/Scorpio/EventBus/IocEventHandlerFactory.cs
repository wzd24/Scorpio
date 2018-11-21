using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// This <see cref="IEventHandlerFactory"/> implementation is used to get/release
    /// handlers using Ioc.
    /// </summary>
    public class IocEventHandlerFactory : IEventHandlerFactory
    {
        /// <summary>
        /// 
        /// </summary>
        public Type HandlerType { get; }

        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="serviceProvider"></param>
        /// <param name="handlerType"></param>
        public IocEventHandlerFactory(IServiceProvider serviceProvider, Type handlerType)
        {
            _serviceProvider = serviceProvider;
            HandlerType = handlerType;
        }

        /// <summary>
        /// Resolves handler object from Ioc container.
        /// </summary>
        /// <returns>Resolved handler object</returns>
        public IEventHandlerDisposeWrapper GetHandler()
        {
            var scope = _serviceProvider.CreateScope();
            return new EventHandlerDisposeWrapper(
                (IEventHandler)scope.ServiceProvider.GetRequiredService(HandlerType),
                () => scope.Dispose()
            );
        }
    }
}
