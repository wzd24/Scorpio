using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataFilterDescriptorExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DataFilterDescriptor Enable<TFilter>(this DataFilterDescriptor<TFilter> descriptor)
        {
            descriptor.IsEnabled = true;
            return descriptor;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DataFilterDescriptor Disable<TFilter>(this DataFilterDescriptor<TFilter> descriptor)
        {
            descriptor.IsEnabled = false;
            return descriptor;
        }

    }
}
