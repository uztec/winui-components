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

        public static RectangleF ToRectF(this Rectangle rect)
        {
            return new RectangleF(rect.X, rect.Y, rect.Width, rect.Height);
        }

        public static Rectangle ToRect(this RectangleF rect, bool round = false)
        {
            return (round) ? new Rectangle(Point.Round(rect.Location), Size.Round(rect.Size))
                : new Rectangle(Point.Ceiling(rect.Location), Size.Ceiling(rect.Size));
        }


        public static PointF GetAlignmentPoint(this RectangleF rect, SizeF objSize, ContentAlignment alignment)
        {
            float offsetX = rect.Width - objSize.Width;
            float offsetY = rect.Height - objSize.Height;
            switch (alignment)
            {
                case ContentAlignment.TopLeft: return rect.Location;     // Default
                case ContentAlignment.TopCenter: return rect.ApplyPadding(offsetX / 2, 0).Location;
                case ContentAlignment.TopRight: return rect.ApplyPadding(offsetX, 0).Location;

                case ContentAlignment.MiddleLeft: return rect.ApplyPadding(0, offsetY / 2).Location;
                case ContentAlignment.MiddleCenter: return rect.ApplyPadding(offsetX / 2, offsetY / 2).Location;
                case ContentAlignment.MiddleRight: return rect.ApplyPadding(offsetX, offsetY / 2).Location;

                case ContentAlignment.BottomLeft: return rect.ApplyPadding(0, offsetY).Location;
                case ContentAlignment.BottomCenter: return rect.ApplyPadding(offsetX / 2, offsetY).Location;
                case ContentAlignment.BottomRight: return rect.ApplyPadding(offsetX, offsetY).Location;
            }

            return rect.Location;
        }
    }
}