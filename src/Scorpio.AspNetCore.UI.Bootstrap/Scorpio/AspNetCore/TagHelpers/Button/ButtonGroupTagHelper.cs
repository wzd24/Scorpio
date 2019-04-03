namespace Scorpio.AspNetCore.TagHelpers.Button
{
    public class ButtonGroupTagHelper : TagHelper<ButtonGroupTagHelper, ButtonGroupTagHelperService>
    {
        public ButtonGroupDirection Direction { get; set; } = ButtonGroupDirection.Horizontal;

        public ButtonGroupSize Size { get; set; } = ButtonGroupSize.Default;

        public ButtonGroupTagHelper(ButtonGroupTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
