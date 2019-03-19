using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TTagHelper"></typeparam>
    public abstract class TagHelperService<TTagHelper> : ITagHelperService<TTagHelper>
                 where TTagHelper : TagHelper

    {
        /// <summary>
        /// 
        /// </summary>
        public virtual int Order { get; }

        /// <summary>
        /// 
        /// </summary>
        public TTagHelper TagHelper { get ; internal set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public virtual void Init(TagHelperContext context)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public virtual void Process(TagHelperContext context, TagHelperOutput output)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        public virtual Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            Process(context, output);
            return Task.CompletedTask;
        }
    }
}
