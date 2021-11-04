using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Utils
{
    public static class SizingExtensions
    {
        public static RectangleF ApplyPadding(this RectangleF rect, int padding)
        {
            return ApplyPadding(rect, padding, padding, padding, padding);
        }

        public static RectangleF ApplyPadding(this RectangleF rect, float paddingHoriz, float paddingVert)
        {
            return ApplyPadding(rect, paddingHoriz, paddingVert, paddingHoriz, paddingVert);
        }

        public static RectangleF ApplyPadding(this RectangleF rect, Padding padding)
        {
            return ApplyPadding(rect, padding.Left, padding.Top, padding.Right, padding.Bottom);
        }

        public static RectangleF ApplyPadding(this RectangleF rect, float paddingLeft, float paddingTop, float paddingRight, float paddingBottom)
        {
            RectangleF output = new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
            output.X += paddingLeft;
            output.Y += paddingTop;
            output.Width -= paddingLeft + paddingRight;
            output.Height -= paddingTop + paddingBottom;
            return output;
        }

        public static Point ApplyPadding(this Point point, Padding padding)
        {
            Point output = new Point(point.X, point.Y);
            output.X += padding.Left;
            output.Y += padding.Top;
            return output;
        }
    }
}
