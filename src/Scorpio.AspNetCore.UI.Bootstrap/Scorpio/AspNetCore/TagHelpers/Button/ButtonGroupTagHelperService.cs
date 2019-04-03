using System;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public class ButtonGroupTagHelperService : TagHelperService<ButtonGroupTagHelper>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            AddButtonGroupClass(context, output);
            AddSizeClass(context, output);
            AddAttributes(context, output);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddSizeClass(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Size)
            {
                case ButtonGroupSize.Default:
                    break;
                case ButtonGroupSize.Small:
                    output.AddClass("btn-group-sm");
                    break;
                case ButtonGroupSize.Medium:
                    output.AddClass("btn-group-md");
                    break;
                case ButtonGroupSize.Large:
                    output.AddClass("btn-group-lg");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddButtonGroupClass(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.Direction)
            {
                case ButtonGroupDirection.Horizontal:
                    output.AddClass("btn-group");
                    break;
                case ButtonGroupDirection.Vertical:
                    output.AddClass("btn-group-vertical");
                    break;
                default:
                    output.AddClass("btn-group");
                    break;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddAttributes(TagHelperContext context, TagHelperOutput output)
        {
            output.Attributes.Add("role", "group");
        }
    }
}