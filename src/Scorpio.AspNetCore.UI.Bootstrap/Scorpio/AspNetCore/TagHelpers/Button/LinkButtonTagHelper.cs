using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    [HtmlTargetElement("a", Attributes = "button", TagStructure = TagStructure.NormalOrSelfClosing)]
    [HtmlTargetElement("input", Attributes = "button", TagStructure = TagStructure.WithoutEndTag)]
    public class LinkButtonTagHelper : TagHelper<LinkButtonTagHelper, LinkButtonTagHelperService>, IButtonTagHelperBase
    {
        [HtmlAttributeName("button")]
        public ButtonType ButtonType { get; set; }

        public ButtonSize Size { get; set; } = ButtonSize.Default;

        /// <summary>
        /// 
        /// </summary>
        public bool OutLine { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }

        public bool? Disabled { get; set; }

        public FontIconType IconType { get; } = FontIconType.FontAwesome;

        public LinkButtonTagHelper(LinkButtonTagHelperService service) 
            : base(service)
        {

        }
    }
}
