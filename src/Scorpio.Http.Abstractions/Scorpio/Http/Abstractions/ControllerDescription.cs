using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Http.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public class ControllerDescription
    {
        /// <summary>
        /// 
        /// </summary>
        public string Id { get; }

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
        public Type ServiceType { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<ActionDescription> Actions { get; }
    }
}
