using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Http.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public class ModuleDescription
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
        public string RootPath { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<ControllerDescription> Controllers { get; }
    }
}
