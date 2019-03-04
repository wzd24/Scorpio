using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class EntityChangeEntry
    {
        /// <summary>
        /// 
        /// </summary>
        public object Entity { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public EntityChangeType ChangeType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="changeType"></param>
        public EntityChangeEntry(object entity, EntityChangeType changeType)
        {
            Entity = entity;
            ChangeType = changeType;
        }
    }
}
