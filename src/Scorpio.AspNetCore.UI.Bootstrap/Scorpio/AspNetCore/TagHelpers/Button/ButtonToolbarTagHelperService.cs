using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    public class ButtonToolbarTagHelperService : TagHelperService<ButtonToolbarTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "div";
            output.AddClass("btn-toolbar");
            output.Attributes.Add("role","toolbar");
        }
    }
}