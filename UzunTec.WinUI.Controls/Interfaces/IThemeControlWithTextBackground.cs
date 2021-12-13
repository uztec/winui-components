using System.Drawing;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControlWithTextBackground : IThemeControl
    {
        Color TextColor { get; }
        Color TextColorDisabled { get; }
        Color BackgroundColorDark { get; }
        Color BackgroundColorLight { get; }
        Color BackgroundColorFocusedDark { get; }
        Color BackgroundColorFocusedLight { get; }
        Color BackgroundColorDisabledDark { get; }
        Color BackgroundColorDisabledLight { get; }
    }
}
