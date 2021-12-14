using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using UzunTec.Utils.Common;

namespace UzunTec.WinUI.Controls.Themes
{
    public class Palette
    {
        private readonly SortedList<ColorVariant, Color[]> colors;
        private readonly SortedList<ColorVariant, Brush> brushes;

        public Palette()
        {
            this.colors = new SortedList<ColorVariant, Color[]>();
            this.brushes = new SortedList<ColorVariant, Brush>();
        }

        public Color GetColor(ColorVariant variant, bool dark = true)
        {
            if (this.colors.TryGetValue(variant, out Color[] colorPair))
            {
                if (colorPair != null && colorPair.Length > 0)
                {
                    return (colorPair.Length == 1) ? colorPair[0]
                        : (dark) ? colorPair[0] : colorPair[1];
                }
            }
            return default;
        }

        public Brush GetBrush(ColorVariant variant)
        {
            if (!this.brushes.TryGetValue(variant, out Brush output))
            {
                if (this.colors.TryGetValue(variant, out Color[] colorPair))
                {
                    if (colorPair != null && colorPair.Length > 0)
                    {
                        output = (colorPair.Length == 1) ? new SolidBrush(colorPair[0])
                                    : (Brush)new LinearGradientBrush(ThemeConstants.DefaultControlRect, colorPair[0], colorPair[1], LinearGradientMode.Vertical);

                        this.brushes.Add(variant, output);
                    }
                }
            }
            return output;
        }

        public void SetColor(ColorVariant variant, params Color[] colors)
        {
            this.colors.AddOrUpdate(variant, colors);
        }
    }
}
