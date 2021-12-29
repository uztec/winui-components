using System.Drawing;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControlWithHintPlaceholder : IThemeControlWithHint
    {
        Color PlaceholderColor { get; }
        Font PlaceholderFont { get; }
    }
}
