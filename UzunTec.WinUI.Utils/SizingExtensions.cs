using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Utils
{
    public static class SizingExtensions
    {
        public static Rectangle ApplyPadding(this Rectangle rect, int padding)
        {
            return ApplyPadding(rect, padding, padding, padding, padding);
        }

        public static Rectangle ApplyPadding(this Rectangle rect, int paddingHoriz, int paddingVert)
        {
            return ApplyPadding(rect, paddingHoriz, paddingVert, paddingHoriz, paddingVert);
        }

        public static Rectangle ApplyPadding(this Rectangle rect, Padding padding)
        {
            return ApplyPadding(rect, padding.Left, padding.Top, padding.Right, padding.Bottom);
        }

        public static Rectangle ApplyPadding(this Rectangle rect, int paddingLeft, int paddingTop, int paddingRight, int paddingBottom)
        {
            Rectangle output = new Rectangle(rect.X, rect.Y, rect.Width, rect.Height);
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
