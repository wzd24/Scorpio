using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public class EventBusOptions
    {
        /// <summary>
        /// 
        /// </summary>
        internal ICollection<EventHandlerDescriptor> Handlers { get; }

        /// <summary>
        /// 
        /// </summary>
        public EventBusOptions()
        {
            Handlers = new HashSet<EventHandlerDescriptor>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="handlerDescriptor"></param>
        public void AddHandler(EventHandlerDescriptor handlerDescriptor)
        {
            Handlers.Add(handlerDescriptor);
        }

    }
}
