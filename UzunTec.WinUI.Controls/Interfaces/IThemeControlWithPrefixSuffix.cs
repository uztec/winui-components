using System.Drawing;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControlWithPrefixSuffix : IThemeControl
    {
        string PrefixText { get; }
        Color PrefixTextColor { get; }
        Font PrefixFont { get; }
        Color PrefixTextHighlightColor { get; }
        Color PrefixTextColorDisabled { get; }
        FontClass PrefixFontClass { get; }
        ColorVariant PrefixTextColorVariant { get; }
        ColorVariant PrefixTextColorHightlightVariant { get; }
        string SuffixText { get; }
        Color SuffixTextColor { get; }
        Font SuffixFont { get; }
        Color SuffixTextHighlightColor { get; }
        Color SuffixTextColorDisabled { get; }
        FontClass SufixFontClass { get; }
        ColorVariant SuffixTextColorVariant { get; }
        ColorVariant SuffixTextColorHightlightVariant { get; }
      
    }
}
