using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Auditing
{
    /// <summary>
    /// 
    /// </summary>
    public class AuditInfo
    {
        /// <summary>
        /// 
        /// </summary>
        public string CurrentUser { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public TimeSpan ExecutionDuration { get; set; }


    }
}
