using Microsoft.AspNetCore.Razor.TagHelpers;
using System;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    public abstract class ButtonTagHelperServiceBase<TTagHelper> : TagHelperService<TTagHelper>
        where TTagHelper : TagHelper, IButtonTagHelperBase
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            NormalizeTagMode(context, output);
            AddClasses(context, output);
            AddIcon(context, output);
            AddText(context, output);
            AddDisabled(context, output);
        }

        protected virtual void NormalizeTagMode(TagHelperContext context, TagHelperOutput output)
        {
            output.TagMode = TagMode.StartTagAndEndTag;
        }

        protected virtual void AddClasses(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("btn");

            if (TagHelper.ButtonType != ButtonType.Default)
            {
                output.AddClass($"btn-{(TagHelper.OutLine&&TagHelper.ButtonType!= ButtonType.Link?"outline-":"")}{ TagHelper.ButtonType.ToClassName()}");
            }

            if (TagHelper.Size != ButtonSize.Default)
            {
                output.AddClass(TagHelper.Size.ToClassName());
            }
        }

        protected virtual void AddIcon(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.Icon.IsNullOrWhiteSpace())
            {
                return;
            }

            output.Content.AppendHtml($"<i class=\"{GetIconClass(context, output)}\"></i> ");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        /// <returns></returns>
        protected virtual string GetIconClass(TagHelperContext context, TagHelperOutput output)
        {
            switch (TagHelper.IconType)
            {
                case FontIconType.FontAwesome:
                    return "fa fa-" + TagHelper.Icon;
                default:
                    return TagHelper.Icon;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddText(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.Text.IsNullOrWhiteSpace())
            {
                return;
            }

            output.Content.AppendHtml($"<span>{TagHelper.Text}</span>");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        /// <param name="output"></param>
        protected virtual void AddDisabled(TagHelperContext context, TagHelperOutput output)
        {
            if (TagHelper.Disabled ?? false)
            {
                output.Attributes.Add("disabled", "disabled");
            }
        }
    }
}