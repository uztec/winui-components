using System;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.Interfaces
{
    public interface IThemeControl
    {
        string Name { get; set; }
        string Text { get; set; }
        bool AutoSize { get; set; }
        Size MaximumSize { get; set; }
        Size MinimumSize { get; set; }

        ThemeScheme ThemeScheme { get; }
        bool MouseHovered { get; }
        bool UseThemeColors { get; }
        bool UpdatingTheme { get; set; }

        Padding InternalPadding { get; }

        void UpdateRects();
        void UpdateStylesFromTheme();


        // Control Base Properties
        Rectangle ClientRectangle { get; }
        bool Focused { get; }
        bool Enabled { get; }

        event EventHandler HandleCreated;
        void Invalidate();
    }
}