using System.Drawing;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControlWithHint : IThemeControlWithTextBackground
    {
        Color HintColor { get; }
        Font HintFont { get; }
        Color HintDisabledColor { get; }
        Color HintHighlightColor { get; }
        FontClass HintFontClass { get; }
        bool ShowHint { get; }
        string PlaceholderHintText { get; }
    }
}
