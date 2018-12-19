using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class DataFilterOptionsExtensions
    {


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="options"></param>
        /// <param name="descriptor"></param>
        /// <returns></returns>
        public static DataFilterDescriptor<TFilter> RegiesterFilter<TFilter>(this DataFilterOptions options, DataFilterDescriptor<TFilter> descriptor)
        {
            return options.Descriptors.GetOrAdd(typeof(TFilter), t => descriptor) as DataFilterDescriptor<TFilter>;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <typeparam name="TFilterDescriptor"></typeparam>
        /// <param name="options"></param>
        /// <returns></returns>
        public static DataFilterDescriptor<TFilter> RegiesterFilter<TFilter, TFilterDescriptor>(this DataFilterOptions options)
            where TFilterDescriptor : DataFilterDescriptor<TFilter>
        {
            return options.Descriptors.GetOrAdd(typeof(TFilter), f => Activator.CreateInstance<TFilterDescriptor>()) as TFilterDescriptor;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TFilter"></typeparam>
        /// <param name="options"></param>
        /// <param name="configureAction"></param>
        /// <returns></returns>
        public static DataFilterOptions ConfigureFilter<TFilter>(this DataFilterOptions options, Action<DataFilterDescriptor<TFilter>> configureAction)

        {
            var descriptor = options.Descriptors.GetOrDefault(typeof(TFilter)) as DataFilterDescriptor<TFilter>;
            configureAction(descriptor);
            return options;
        }

    }
}
