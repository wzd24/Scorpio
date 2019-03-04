using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConnectionStringResolver
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionStringName"></param>
        /// <returns></returns>
        [NotNull]
        string Resolve(string connectionStringName = null);
    }
}
