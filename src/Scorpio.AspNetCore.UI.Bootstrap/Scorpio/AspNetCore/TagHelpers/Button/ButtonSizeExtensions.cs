﻿using System;

namespace Scorpio.AspNetCore.TagHelpers.Button
{
    public static class ButtonSizeExtensions
    {
        public static string ToClassName(this ButtonSize size)
        {
            switch (size)
            {
                case ButtonSize.Small:
                    return "btn-sm";
                case ButtonSize.Medium:
                    return "btn-md";
                case ButtonSize.Large:
                    return "btn-lg";
                case ButtonSize.Block:
                    return "btn-block";
                case ButtonSize.Block_Small:
                    return "btn-sm  btn-block";
                case ButtonSize.Block_Medium:
                    return "btn-md  btn-block";
                case ButtonSize.Block_Large:
                    return "btn-lg  btn-block";
                case ButtonSize.Default:
                    return "";
                default:
                    throw new Exception($"Unknown {nameof(ButtonSize)}: {size}");
            }
        }
    }
}