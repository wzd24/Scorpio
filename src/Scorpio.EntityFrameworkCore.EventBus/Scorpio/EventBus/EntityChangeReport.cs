using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public class EntityChangeReport
    {
        /// <summary>
        /// 
        /// </summary>
        public List<EntityChangeEntry> ChangedEntities { get; }

        /// <summary>
        /// 
        /// </summary>
        public List<DomainEventEntry> DomainEvents { get; }

        /// <summary>
        /// 
        /// </summary>
        public EntityChangeReport()
        {
            ChangedEntities = new List<EntityChangeEntry>();
            DomainEvents = new List<DomainEventEntry>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool IsEmpty()
        {
            return ChangedEntities.Count <= 0 &&
                   DomainEvents.Count <= 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[EntityChangeReport] ChangedEntities: {ChangedEntities.Count}, DomainEvents: {DomainEvents.Count}";
        }
    }
}
