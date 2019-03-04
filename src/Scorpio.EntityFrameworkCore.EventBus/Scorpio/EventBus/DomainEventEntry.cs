using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class DomainEventEntry
    {
        /// <summary>
        /// 
        /// </summary>
        public object SourceEntity { get; }

        /// <summary>
        /// 
        /// </summary>
        public object EventData { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sourceEntity"></param>
        /// <param name="eventData"></param>
        public DomainEventEntry(object sourceEntity, object eventData)
        {
            SourceEntity = sourceEntity;
            EventData = eventData;
        }
    }
}
