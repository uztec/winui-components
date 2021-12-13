using System;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControl
    {
        ThemeScheme ThemeScheme { get; }
        bool MouseHovered { get; }
        bool UseThemeColors { get; }

        Padding InternalPadding { get; }

        void UpdateRects();
        void UpdateStylesFromTheme()


        // Control Base Properties
        Rectangle ClientRectangle { get; }
        bool Focused { get; }
        bool Enabled { get; }

        event EventHandler HandleCreated;
        void Invalidate();
    }
}