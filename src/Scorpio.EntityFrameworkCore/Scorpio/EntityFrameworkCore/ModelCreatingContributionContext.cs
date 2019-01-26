using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.EntityFrameworkCore
{
    /// <summary>
    /// 
    /// </summary>
    public sealed class ModelCreatingContributionContext
    {
        /// <summary>
        /// 
        /// </summary>
        public ModelBuilder ModelBuilder { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        public ModelCreatingContributionContext(ModelBuilder modelBuilder)
        {
            ModelBuilder = modelBuilder;
        }
    }
}
