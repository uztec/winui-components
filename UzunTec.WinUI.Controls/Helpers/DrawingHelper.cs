using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.Helpers
{
    internal static class DrawingHelper
    {
        private const int LINE_BOTTOM_HEIGHT = 1;
        private const int FOCUSED_LINE_BOTTOM_HEIGHT = 2;
        private const int LINE_BOTTOM_PADDING = 1;

        private static readonly ThemeSchemeManager themeManager = ThemeSchemeManager.Instance;

        internal static void FillBackground(this Graphics g, IThemeControlWithBackground ctrl)
        {

            Brush backgroundBrush = ctrl.Enabled ?
                            (ctrl.Focused || ctrl.MouseHovered) ? themeManager.GetFocusedBackgroundBrush(ctrl)
                            : themeManager.GetBackgroundBrush(ctrl)
                            : themeManager.GetDisabledBackgroundBrush(ctrl);

            RectangleF bgRect = ctrl.ClientRectangle;
            bgRect.Height -= LINE_BOTTOM_PADDING;
            g.FillRectangle(backgroundBrush, bgRect);
        }

        internal static void DrawBottomLine(this Graphics g, IThemeControl ctrl)
        {
            Brush lineBrush = ctrl.Focused ? themeManager.GetHighlightBrush(ctrl) : themeManager.GetTextBrush(ctrl);
            g.FillRectangle(lineBrush, GetBottomLineRect(ctrl));
        }

        internal static RectangleF GetBottomLineRect(this IThemeControl ctrl)
        {
            float lineHeight = ctrl.Focused ? FOCUSED_LINE_BOTTOM_HEIGHT : LINE_BOTTOM_HEIGHT;
            float lineY = ctrl.ClientRectangle.Bottom - lineHeight - (ctrl.Focused ? 0 : LINE_BOTTOM_PADDING);
            return new RectangleF(0, lineY, ctrl.ClientRectangle.Width, lineHeight);
        }

        internal static void DrawHint(this Graphics g, IThemeControlWithHint ctrl)
        {
            Brush hintBrush = themeManager.GetHintBrush(ctrl);
            RectangleF hintRect = GetHintRect(ctrl, g);
            g.DrawString(ctrl.PlaceholderHintText, ctrl.HintFont, hintBrush, hintRect);
        }

        internal static RectangleF GetHintRect(this IThemeControlWithHint ctrl, Graphics g)
        {
            SizeF hintSize = g.MeasureString(ctrl.PlaceholderHintText, ctrl.HintFont, ctrl.ClientRectangle.Width);
            return new RectangleF(ctrl.InternalPadding.Left, ctrl.InternalPadding.Top, hintSize.Width, hintSize.Height);
        }
    }
}
