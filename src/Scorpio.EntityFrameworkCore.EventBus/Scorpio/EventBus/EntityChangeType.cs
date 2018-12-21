using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EventBus
{
    /// <summary>
    /// 
    /// </summary>
    public enum EntityChangeType : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Created = 0,

        /// <summary>
        /// 
        /// </summary>
        Updated = 1,

        /// <summary>
        /// 
        /// </summary>
        Deleted = 2
    }
}
