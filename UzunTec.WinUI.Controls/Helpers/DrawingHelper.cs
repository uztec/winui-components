using System.Drawing;
using System.Drawing.Drawing2D;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Utils;

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
            FillBackground(g, ctrl, ctrl.ClientRectangle);
        }
        internal static void FillBackground(this Graphics g, IThemeControlWithBackground ctrl, bool lineBottom)
        {
            FillBackground(g, ctrl, ctrl.ClientRectangle, lineBottom);
        }
        internal static void FillBackground(this Graphics g, IThemeControlWithBackground ctrl, RectangleF bgRect, bool lineBottom = true)
        {

            Brush backgroundBrush = ctrl.Enabled ?
                            (ctrl.Focused || ctrl.MouseHovered) ? themeManager.GetFocusedBackgroundBrush(ctrl)
                            : themeManager.GetBackgroundBrush(ctrl)
                            : themeManager.GetDisabledBackgroundBrush(ctrl);

            if (lineBottom)
            {
                bgRect.Height -= LINE_BOTTOM_PADDING;
            }
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
            RectangleF hintRect = GetHintRect(ctrl, g);
            DrawHint(g, ctrl, hintRect);
        }

        internal static void DrawHint(this Graphics g, IThemeControlWithHint ctrl, RectangleF hintRect)
        {
            Brush hintBrush = themeManager.GetHintBrush(ctrl);
            g.DrawString(ctrl.PlaceholderHintText, ctrl.HintFont, hintBrush, hintRect);
        }

        internal static RectangleF GetHintRect(this IThemeControlWithHint ctrl, Graphics g)
        {
            SizeF hintSize = g.MeasureString(ctrl.PlaceholderHintText, ctrl.HintFont, ctrl.ClientRectangle.Width);
            return new RectangleF(ctrl.InternalPadding.Left, ctrl.InternalPadding.Top, hintSize.Width, hintSize.Height);
        }

        internal static void DrawText(this Graphics g, string text, Font font, Brush textBrush, RectangleF rect, ContentAlignment alignment = ContentAlignment.TopLeft)
        {
            SizeF textSize = g.MeasureString(text, font, rect.Size);
            DrawText(g, text, font, textBrush, rect, textSize, alignment);
        }

        internal static void DrawText(this Graphics g, string text, Font font, Brush textBrush, RectangleF rect, SizeF textSize, ContentAlignment alignment = ContentAlignment.TopLeft)
        {
            g.Clip = new Region(rect);
            if (alignment != ContentAlignment.TopLeft)
            {
                PointF textPoint = rect.GetAlignmentPoint(textSize, alignment);
                g.DrawString(text, font, textBrush, textPoint);
            }
            else
            {
                g.DrawString(text, font, textBrush, rect);
            }
            g.ResetClip();
        }

        internal static void DrawTriangle(this Graphics g, IThemeControlWithHint ctrl)
        {
            // Create and Draw the arrow
            GraphicsPath pth = new GraphicsPath();
            PointF TopRight = new PointF(ctrl.ClientRectangle.Width - 0.5f - 14, (ctrl.ClientRectangle.Height >> 1) - 2.5f);
            PointF MidBottom = new PointF(ctrl.ClientRectangle.Width - 4.5f - 14, (ctrl.ClientRectangle.Height >> 1) + 2.5f);
            PointF TopLeft = new PointF(ctrl.ClientRectangle.Width - 8.5f - 14, (ctrl.ClientRectangle.Height >> 1) - 2.5f);
            pth.AddLine(TopLeft, TopRight);
            pth.AddLine(TopRight, MidBottom);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush triangleBrush = ctrl.Enabled ? ctrl.Focused ? new SolidBrush(ctrl.HighlightColor)
                  : new SolidBrush(ctrl.HintColor)
                  : new SolidBrush(ctrl.DisabledHintColor);
            g.FillPath(triangleBrush, pth);
            g.SmoothingMode = SmoothingMode.None;
        }
    }
}
