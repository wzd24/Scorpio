using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Dropdown
{
    public class DropdownMenuItemTagHelperService:TagHelperService<DropdownMenuItemTagHelper>
    {
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.AddClass("dropdown-item");
            base.Process(context, output);
        }
    }
}