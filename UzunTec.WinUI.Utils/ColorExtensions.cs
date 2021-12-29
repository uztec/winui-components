using System.Drawing;

namespace UzunTec.WinUI.Utils
{
    public static class ColorExtensions
    {
        public static Color Lighten(this Color c, int lightness)
        {
            int R = c.R + lightness;
            int G = c.G + lightness;
            int B = c.B + lightness;
            return Color.FromArgb(c.A, R > 255 ? 255 : R, G > 255 ? 255 : G, B > 255 ? 255 : B);
        }

        public static Color Darken(this Color c, int darkness)
        {
            int R = c.R - darkness;
            int G = c.G - darkness;
            int B = c.B - darkness;
            return Color.FromArgb(c.A, R < 0 ? 0 : R, G < 0 ? 0 : G, B < 0 ? 0 : B);
        }
    }
}
