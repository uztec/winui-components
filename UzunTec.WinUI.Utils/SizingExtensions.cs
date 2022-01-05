using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Utils
{
    public static class SizingExtensions
    {
        public static RectangleF ApplyPadding(this RectangleF rect, float padding)
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



        public static Padding AddPadding(this Padding padding, params Padding[] paddings)
        {
            int left = padding.Left;
            int top = padding.Top;
            int right = padding.Right;
            int bottom = padding.Bottom;

            foreach (Padding pd in paddings)
            {
                left += pd.Left;
                top += pd.Top;
                right += pd.Right;
                bottom += pd.Bottom;
            }
            return new Padding(left, top, right, bottom);
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


        public static RectangleF ShrinkToSize(this RectangleF rect, SizeF objSize, ContentAlignment alignment)
        {
            float offsetX = rect.Width - objSize.Width;
            float offsetY = rect.Height - objSize.Height;
            switch (alignment)
            {
                case ContentAlignment.TopLeft: return rect.ApplyPadding(0, 0, offsetX, offsetY);     // Default
                case ContentAlignment.TopCenter: return rect.ApplyPadding(offsetX / 2, 0);
                case ContentAlignment.TopRight: return rect.ApplyPadding(offsetX, 0, 0, 0);

                case ContentAlignment.MiddleLeft: return rect.ApplyPadding(0, offsetY / 2);
                case ContentAlignment.MiddleCenter: return rect.ApplyPadding(offsetX / 2, offsetY / 2);
                case ContentAlignment.MiddleRight: return rect.ApplyPadding(offsetX, offsetY / 2, 0, offsetY / 2);

                case ContentAlignment.BottomLeft: return rect.ApplyPadding(0, offsetY, offsetX, 0);
                case ContentAlignment.BottomCenter: return rect.ApplyPadding(offsetX / 2, offsetY, 0, 0);
                case ContentAlignment.BottomRight: return rect.ApplyPadding(offsetX, offsetY, 0, 0);
            }
            return rect;
        }

        public static PointF GetCenterPoint(this RectangleF rect)
        {
            return new PointF(rect.X + rect.Width / 2f, rect.Y + rect.Height / 2f);
        }

        public static Point GetCenterPoint(this Rectangle rect)
        {
            return new Point(rect.X + ((int)(rect.Width / 2f)), rect.Y + ((int)(rect.Height / 2f)));
        }

    }
}