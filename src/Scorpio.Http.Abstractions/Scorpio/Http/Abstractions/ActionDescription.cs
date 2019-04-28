using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Http.Abstractions
{
    /// <summary>
    /// 
    /// </summary>
    public class ActionDescription
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
        public string HttpMethod { get; }

        /// <summary>
        /// 
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// 
        /// </summary>
        public Type ReturnType { get; }

        /// <summary>
        /// 
        /// </summary>
        public IList<ParameterDescription> Parameters { get; }


        /// <summary>
        /// 
        /// </summary>
        public ControllerDescription Controller { get; }
    }
}
