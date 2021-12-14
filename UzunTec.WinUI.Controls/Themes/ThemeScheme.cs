using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls.Themes
{
    public class ThemeScheme
    {
        public Palette Palette { get; set; }
        public IDictionary<FontClass, Font> FontClasses { get; protected set; }

        public Color PrimaryColor { get; set; }
        public Color PrimaryLightColor { get; set; }
        public Color SecondaryColor { get; set; }
        public Color FormBackgroundColor { get; set; }
        public Color FormBackgroundDarkColor { get; set; }
        public Color ControlTextColorDark { get; set; }
        public Color PrefixSuffixTextColor { get; set; }
        public Color ControlTextLightColor { get; set; }
        public Color ControlBackgroundColorLight { get; set; }
        public Color ControlBackgroundColorDark { get; set; }
        public Color ControlHighlightColor { get; set; }
        public Color ControlHintTextColor { get; set; }
        public Color ControlPlaceholderColor { get; set; }
        public Color FormTitleBarColor { get; set; }

        #region Fonts
        public Font ControlTextFont { get; set; }
        public Font ControlHintFont { get; set; }
        public Font ControlPlaceholderFont { get; set; }
        public Font FormTitleBarFont { get; set; }
        public Padding HintControlInternalPadding { get; set; }
        #endregion

        #region Control if disabled
        public Color ControlTextColorDisabled { get; set; }
        public Color DisabledControlBackgroundColorLight { get; set; }
        public Color DisabledControlBackgroundColorDark { get; set; }
        public Color ThemeHighlightColor { get; set; }
        #endregion

        public ThemeScheme()
        {
            this.PrimaryColor = Color.FromArgb(240, 240, 240);
            this.PrimaryLightColor = Color.FromArgb(240, 240, 240); //new

            this.SecondaryColor = Color.FromArgb(240, 240, 240); //new

            this.FormBackgroundColor = Color.FromArgb(240, 240, 240);
            this.FormBackgroundDarkColor = Color.FromArgb(240, 240, 240); //new

            this.ControlTextColorDark = Color.Black;
            this.PrefixSuffixTextColor = Color.Black; //new
            this.ControlTextLightColor = Color.Black; //new
            this.ControlTextColorDisabled = Color.DarkGray;

            this.ControlHintTextColor = Color.DarkGray;
            this.ThemeHighlightColor = Color.Gray;
            this.ControlPlaceholderColor = Color.FromArgb(200, Color.DarkGray);

            this.ControlHighlightColor = Color.Purple;

            this.ControlBackgroundColorDark = Color.FromArgb(220, 220, 220);
            this.ControlBackgroundColorLight = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorDark = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorLight = Color.FromArgb(250, 250, 250);

            this.FormTitleBarFont = ThemeSchemeManager.Instance.GetFont("Segoe UI SemiBold", 17.25F, FontStyle.Bold);
            this.ControlTextFont = ThemeSchemeManager.Instance.GetFont("Segoe UI", 12);
            this.ControlHintFont = ThemeSchemeManager.Instance.GetFont("Segoe UI", 7);
            this.ControlPlaceholderFont = ThemeSchemeManager.Instance.GetFont("Segoe UI", 15);
            this.HintControlInternalPadding = new Padding(4);
        }

        public Font GetFontFromClass(FontClass fontClass)
        {
            return this.FontClasses[fontClass];
        }

        public Color GetPaletteColor(ColorVariant variant, bool dark = true)
        {
            return this.Palette.GetColor(variant, dark);
        }

        /*
         * Background - Tela = f0f0f0
            Cor do Texto Branco = f0f0f0
            Cor de Texto Enabled = 2f3542
            Cor de Texto Disabled = 858f9a
            Cor de Hint = 858f9a
            Cor de Accent 1 = 74ad50
            Cor de Accent 2 = 205a4f
        */
    }
}
