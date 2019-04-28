﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Blockquote
{
    /// <summary>
    /// 
    /// </summary>
    public class BlockquoteTagHelper:TagHelper
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public override Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("blockquote");
            return base.ProcessAsync(context, output);
        }
    }
}
