using System.Drawing;

namespace UzunTec.WinUI.Controls.Interfaces
{
    public interface IThemeControlWithHintPlaceholder : IThemeControlWithHint
    {
        Color PlaceholderColor { get; }
        Font PlaceholderFont { get; }
    }
}
