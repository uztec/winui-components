using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControl
    {
        ThemeScheme ThemeScheme { get; }
        bool MouseHovered { get; }

        Color HighlightColor { get; }
        Color DisabledTextColor { get; }
        Color TextColor { get; }
        Padding InternalPadding { get; }

        // Control Base Properties
        Rectangle ClientRectangle { get; }
        bool Focused { get; }
        bool Enabled { get; }
    }
}