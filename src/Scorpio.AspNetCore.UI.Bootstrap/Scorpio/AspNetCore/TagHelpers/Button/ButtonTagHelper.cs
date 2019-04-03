using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    [HtmlTargetElement("button", TagStructure = TagStructure.NormalOrSelfClosing)]
    public class ButtonTagHelper : TagHelper<ButtonTagHelper, ButtonTagHelperService>, IButtonTagHelperBase
    {
        public ButtonType ButtonType { get; set; } = ButtonType.Default;

        public bool OutLine { get; set; }

        public ButtonSize Size { get; set; } = ButtonSize.Default;

        public string BusyText { get; set; }

        public string Text { get; set; }

        public string Icon { get; set; }

        public bool? Disabled { get; set; }

        public FontIconType IconType { get; set; } = FontIconType.FontAwesome;

        public ButtonTagHelper(ButtonTagHelperService service) 
            : base(service)
        {

        }
    }
}

