using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public class DataFilterOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<Type, DataFilterState> DefaultStates { get; }

        /// <summary>
        /// 
        /// </summary>
        public DataFilterOptions()
        {
            DefaultStates = new Dictionary<Type, DataFilterState>();
        }
    }
}
