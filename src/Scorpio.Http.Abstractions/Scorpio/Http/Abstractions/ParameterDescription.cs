using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Http.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public class ParameterDescription
    {
        /// <summary>
        /// 
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool IsOptional { get; }

        /// <summary>
        /// 
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// 
        /// </summary>
        public ActionDescription Action { get; }
    }
}
