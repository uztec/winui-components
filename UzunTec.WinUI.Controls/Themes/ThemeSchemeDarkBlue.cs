using System.Collections.Generic;
using System.Drawing;

namespace UzunTec.WinUI.Controls.Themes
{
    public class ThemeSchemeDarkBlue : ThemeScheme
    {
        public ThemeSchemeDarkBlue()
        {
            this.Palette = Palettes.Aussie;

            this.FontClasses = new Dictionary<FontClass, Font>
            {
                {FontClass.H1, ThemeSchemeManager.Instance.GetFont("Segoe UI", 30, FontStyle.Italic) },
                {FontClass.H2, ThemeSchemeManager.Instance.GetFont("Segoe UI", 30, FontStyle.Italic) },
            };


            this.ControlTextColorDark = this.Palette.GetColor(ColorVariant.Dark);
            this.ControlTextColorLight = this.Palette.GetColor(ColorVariant.Light);

            this.ThemeHighlightColor = this.Palette.GetColor(ColorVariant.Info, false);

            this.ControlTextFont = new Font(FontClasses[FontClass.Body].FontFamily, 15, FontStyle.Bold);


            this.PrimaryColor = Color.FromArgb(17, 162, 170); //ok
            this.PrimaryLightColor = Color.FromArgb(176, 189, 199); //new

            this.SecondaryColor = Color.FromArgb(26, 30, 44); //new  ok

            this.FormBackgroundColor = Color.FromArgb(40, 45, 62); //ok
            this.FormBackgroundDarkColor = Color.FromArgb(45, 50, 69); //new ok

            this.ControlTextColorDark = Color.FromArgb(240, 240, 240); //ok
            this.ControlTextLightColor = Color.FromArgb(240, 240, 240); //new ok
            this.ControlTextColorDisabled = Color.FromArgb(73, 80, 93); //ok

            this.ControlHintTextColor = Color.FromArgb(73, 80, 93); //ok
            this.ThemeHighlightColor = Color.FromArgb(73, 80, 93); //ok
            this.ControlPlaceholderColor = Color.FromArgb(73, 80, 93); //ok

            this.ControlHighlightColor = Color.FromArgb(17, 162, 170); //ok

            this.ControlBackgroundColorDark = Color.FromArgb(25, 31, 41); //ok
            this.ControlBackgroundColorLight = Color.FromArgb(25, 31, 41); //ok
            this.DisabledControlBackgroundColorDark = Color.FromArgb(49, 57, 71); //ok
            this.DisabledControlBackgroundColorLight = Color.FromArgb(49, 57, 71); //ok

            this.ControlTextFont = new Font("Roboto Light", 15);
            this.ControlHintFont = new Font("Roboto Medium", 6.75f, FontStyle.Bold);
            this.ControlPlaceholderFont = new Font("Roboto Light", 15);
        }

        
    }
}
