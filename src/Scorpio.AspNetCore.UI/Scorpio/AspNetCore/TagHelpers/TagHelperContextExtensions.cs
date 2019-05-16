using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class TagHelperContextExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static T GetValue<T>(this TagHelperContext context, string key)
        {
            if (!context.Items.ContainsKey(key))
            {
                return default;
            }

            return (T)context.Items[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void SetValue(this TagHelperContext context, string key, object value)
        {
            context.Items[key] = value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="key"></param>
        public static T InitValue<T>(this TagHelperContext context, string key)
            where T : class
        {
            var value = Activator.CreateInstance<T>();
            context.SetValue(key, value);
            return value;
        }
    }
}
