using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public interface IModelCreatingContributor
    {
        /// <summary>
        /// 
        /// </summary>
        void Contributor(ModelCreatingContributionContext context);
    }
}
