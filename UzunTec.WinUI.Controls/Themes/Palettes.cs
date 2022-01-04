using System.Drawing;

namespace UzunTec.WinUI.Controls.Themes
{
    public static class Palettes
    {
        public static Palette Flat = new Palette();
        public static Palette Aussie = new Palette();
        public static Palette SGLight = new Palette();

        static Palettes()
        {
            Flat.SetColor(ColorVariant.Primary, Color.FromArgb(41, 128, 185), Color.FromArgb(52, 152, 219));
            Flat.SetColor(ColorVariant.Secondary, Color.FromArgb(189, 195, 199), Color.FromArgb(236, 240, 241));
            Flat.SetColor(ColorVariant.Info, Color.FromArgb(41, 128, 185), Color.FromArgb(52, 152, 219));
            Flat.SetColor(ColorVariant.Success, Color.FromArgb(39, 174, 96), Color.FromArgb(46, 204, 113));
            Flat.SetColor(ColorVariant.Warning, Color.FromArgb(230, 126, 34), Color.FromArgb(241, 196, 15));
            Flat.SetColor(ColorVariant.Danger, Color.FromArgb(192, 57, 43), Color.FromArgb(192, 57, 43));
            Flat.SetColor(ColorVariant.Light, Color.FromArgb(236, 240, 241));
            Flat.SetColor(ColorVariant.Dark, Color.FromArgb(44, 62, 80));

            Aussie.SetColor(ColorVariant.Primary, Color.FromArgb(19, 15, 64), Color.FromArgb(48, 51, 107));
            Aussie.SetColor(ColorVariant.Secondary, Color.FromArgb(199, 236, 238), Color.FromArgb(223, 249, 251));
            Aussie.SetColor(ColorVariant.Info, Color.FromArgb(34, 166, 179), Color.FromArgb(126, 214, 223));
            Aussie.SetColor(ColorVariant.Success, Color.FromArgb(106, 176, 76), Color.FromArgb(186, 220, 88));
            Aussie.SetColor(ColorVariant.Warning, Color.FromArgb(249, 202, 36), Color.FromArgb(246, 229, 141));
            Aussie.SetColor(ColorVariant.Danger, Color.FromArgb(235, 77, 75), Color.FromArgb(255, 121, 121));
            Aussie.SetColor(ColorVariant.Light, Color.FromArgb(236, 240, 241));
            Aussie.SetColor(ColorVariant.Dark, Color.FromArgb(0, 0, 0));

            SGLight.SetColor(ColorVariant.Primary, Color.FromArgb(19, 15, 64), Color.FromArgb(48, 51, 107));
            SGLight.SetColor(ColorVariant.Secondary, Color.FromArgb(199, 236, 238), Color.FromArgb(223, 249, 251));
            SGLight.SetColor(ColorVariant.Info, Color.FromArgb(34, 166, 179), Color.FromArgb(126, 214, 223));
            SGLight.SetColor(ColorVariant.Success, Color.FromArgb(106, 176, 76), Color.FromArgb(186, 220, 88));
            SGLight.SetColor(ColorVariant.Warning, Color.FromArgb(249, 202, 36), Color.FromArgb(246, 229, 141));
            //SGLight.SetColor(ColorVariant.Danger, Color.FromArgb(235, 77, 75), Color.FromArgb(255, 121, 121)); OLD
            SGLight.SetColor(ColorVariant.Danger, Color.FromArgb(181, 30, 29), Color.FromArgb(235, 77, 75));
            SGLight.SetColor(ColorVariant.Light, Color.FromArgb(236, 240, 241));
            SGLight.SetColor(ColorVariant.Dark, Color.FromArgb(14, 25, 36));
        }
    }
}
