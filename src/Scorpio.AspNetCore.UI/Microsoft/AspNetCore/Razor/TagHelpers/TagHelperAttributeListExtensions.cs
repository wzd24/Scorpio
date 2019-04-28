using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.TagHelpers;
namespace Microsoft.AspNetCore.Razor.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    public static class TagHelperAttributeListExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="className"></param>
        public static void AddClass(this TagHelperOutput  output, string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return;
            }
            output.AddClass(className, NullHtmlEncoder.Default);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="output"></param>
        /// <param name="className"></param>
        public static void RemoveClass(this TagHelperOutput output, string className)
        {
            if (string.IsNullOrWhiteSpace(className))
            {
                return;
            }
            output.RemoveClass(className, NullHtmlEncoder.Default);
        }

    }
}
