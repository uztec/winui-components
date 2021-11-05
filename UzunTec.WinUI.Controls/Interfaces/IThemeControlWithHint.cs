using System.Drawing;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControlWithHint : IThemeControlWithBackground
    {
        Color HintColor { get; }
        Font HintFont { get; }
        Color PlaceholderColor { get; }
        Font PlaceholderFont { get; }
        Color DisabledHintColor { get; }
        string PlaceholderHintText { get; }
        bool ShowHint { get; }
    }
}
