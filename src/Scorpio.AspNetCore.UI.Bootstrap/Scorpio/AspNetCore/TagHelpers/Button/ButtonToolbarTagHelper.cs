namespace Scorpio.AspNetCore.TagHelpers.Button
{
    public class ButtonToolbarTagHelper : TagHelper<ButtonToolbarTagHelper, ButtonToolbarTagHelperService>
    {
        public ButtonToolbarTagHelper(ButtonToolbarTagHelperService tagHelperService)
            : base(tagHelperService)
        {

        }
    }
}
