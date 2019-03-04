using System;
using System.Collections.Generic;
using System.Text;
using Scorpio.EventBus;
namespace Scorpio.EventBus.Conventional
{
    /// <summary>
    /// 
    /// </summary>
    public interface IEventHandlerActivationTypeSelector
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerType"></param>
        /// <returns></returns>
        EventHandlerActivationType Select(Type  handlerType);
    }
}
