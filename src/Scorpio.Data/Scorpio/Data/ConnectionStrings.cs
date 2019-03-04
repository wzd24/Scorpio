using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class ConnectionStrings : Dictionary<string, string>
    {
        /// <summary>
        /// 
        /// </summary>
        public const string DefaultConnectionStringName = "Default";

        /// <summary>
        /// 
        /// </summary>
        public string Default
        {
            get => this.GetOrDefault(DefaultConnectionStringName);
            set => this[DefaultConnectionStringName] = value;
        }
    }
}
