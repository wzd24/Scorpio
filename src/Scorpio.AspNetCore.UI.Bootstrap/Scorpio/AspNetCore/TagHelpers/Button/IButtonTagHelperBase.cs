namespace Scorpio.AspNetCore.TagHelpers.Button
{
    /// <summary>
    /// 
    /// </summary>
    public interface IButtonTagHelperBase
    {
        /// <summary>
        /// 
        /// </summary>
        ButtonType ButtonType { get; }

        /// <summary>
        /// 
        /// </summary>
        bool OutLine { get; }

        ButtonSize Size { get; }

        string Text { get; }

        string Icon { get; }

        bool? Disabled { get; }

        FontIconType IconType { get; }
    }
}