using System.Collections.Generic;
using System.Drawing;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Themes
{
    public class ThemeScheme
    {
        public bool DarkTheme { get; set; }

        public Palette Palette { get; set; }
        public IDictionary<FontClass, Font> FontClasses { get; protected set; }

        public Color ThemeHighlightColor { get; set; }
        public Color ControlTextColorDark { get; set; }
        public Color ControlTextColorLight { get; set; }
        public Color ControlTextColorDisabled { get; set; }
        public Font ControlTextFont { get; set; }
        public Color ControlBackgroundColorDark { get; set; }
        public Color ControlBackgroundColorLight { get; set; }
        public Color ControlBackgroundColorFocusedDark { get; set; }
        public Color ControlBackgroundColorFocusedLight { get; set; }
        public Color ControlBackgroundColorDisabledDark { get; set; }
        public Color ControlBackgroundColorDisabledLight { get; set; }
        public Color ControlHintTextColor { get; set; }
        public Color ControlHintTextColorDisabled { get; set; }
        public Color ControlPlaceholderTextColor { get; set; }
        public Font ControlPlaceholderFont { get; set; }
        public Color ThemeSelectionColorDark { get; set; }
        public Color ThemeSelectionColorLight { get; set; }
        public Color ThemeSelectionColorExtraLight { get; set; }
        public Color FormBackgroundColor { get; set; }
        public Color FormHeaderTextColor { get; set; }
        public Font FormHeaderTextFont { get; set; }
        public Color FormHeaderColorDark { get; set; }
        public Color FormHeaderColorLight { get; set; }
        public Font FormTitleFont { get; set; }
        public Color FormTitleTextColor { get; set; }
        public Color FormTitlePanelBackgroundColorDark { get; set; }
        public Color FormTitlePanelBackgroundColorLight { get; set; }
        public Color FormControlButtonHoverColor { get; set; }
        public Color CellBackgroundColor { get; set; }
        public Font GridFont { get; set; }
        public Font GridHeaderFont { get; set; }

        public ThemeScheme()
        {
            this.Palette = Palettes.SGLight;

            this.FontClasses = new Dictionary<FontClass, Font>
            {
                {FontClass.H1, ThemeSchemeManager.Instance.GetFont("Segoe UI SemiBold", 25) },
                {FontClass.H2, ThemeSchemeManager.Instance.GetFont("Segoe UI Semibold", 20) },
                {FontClass.H3, ThemeSchemeManager.Instance.GetFont("Segoe UI", 18, FontStyle.Italic) },
                {FontClass.H4, ThemeSchemeManager.Instance.GetFont("Segoe UI Light", 17, FontStyle.Italic) },
                {FontClass.H5, ThemeSchemeManager.Instance.GetFont("Segoe UI Semibold", 10) },
                {FontClass.H6, ThemeSchemeManager.Instance.GetFont("Segoe UI Light", 10) },
                {FontClass.Body, ThemeSchemeManager.Instance.GetFont("Segoe UI", 12) },
                {FontClass.Styled, ThemeSchemeManager.Instance.GetFont("Roboto Thin", 14, FontStyle.Italic) },
                {FontClass.Small, ThemeSchemeManager.Instance.GetFont("Segoe UI", 8) },
                {FontClass.Tiny, ThemeSchemeManager.Instance.GetFont("Segoe UI", 8) },
            };

            this.DarkTheme = false;

            // Fonts
            this.ControlTextFont = this.FontClasses[FontClass.Body];
            this.FormTitleFont = this.FontClasses[FontClass.H1];
            this.FormHeaderTextFont = this.FontClasses[FontClass.Body];
            this.ControlPlaceholderFont = this.FontClasses[FontClass.Body];
            this.GridFont = this.FontClasses[FontClass.Body];
            this.GridHeaderFont = this.FontClasses[FontClass.H4];



            this.ThemeHighlightColor = this.Palette.GetColor(ColorVariant.Primary, false); //
            this.ControlTextColorDark = this.Palette.GetColor(ColorVariant.Dark); //
            this.ControlTextColorLight = this.Palette.GetColor(ColorVariant.Light); //
            this.ControlTextColorDisabled = this.Palette.GetColor(ColorVariant.Secondary);
            this.ControlBackgroundColorDark = Color.FromArgb(25, 31, 41);
            this.ControlBackgroundColorLight = Color.FromArgb(25, 31, 41);
            this.ControlBackgroundColorFocusedDark = this.Palette.GetColor(ColorVariant.Secondary, true);
            this.ControlBackgroundColorFocusedLight = this.Palette.GetColor(ColorVariant.Secondary, false);
            this.ControlBackgroundColorDisabledDark = Color.FromArgb(49, 57, 71);
            this.ControlBackgroundColorDisabledLight = Color.FromArgb(49, 57, 71);

            this.ControlHintTextColor = this.Palette.GetColor(ColorVariant.Dark);
            this.ControlHintTextColorDisabled = this.Palette.GetColor(ColorVariant.Secondary);
            this.ControlPlaceholderTextColor = this.Palette.GetColor(ColorVariant.Secondary);
            this.ThemeSelectionColorDark = this.Palette.GetColor(ColorVariant.Primary, true);
            this.ThemeSelectionColorLight = this.Palette.GetColor(ColorVariant.Primary, false);
            this.ThemeSelectionColorExtraLight = this.Palette.GetColor(ColorVariant.Primary, false).Lighten(50);

            this.FormBackgroundColor = this.Palette.GetColor(ColorVariant.Light);
            this.FormHeaderTextColor = this.Palette.GetColor(ColorVariant.Light);
            this.FormHeaderColorDark = this.Palette.GetColor(ColorVariant.Dark);
            this.FormHeaderColorLight = this.Palette.GetColor(ColorVariant.Dark);
            this.FormTitleTextColor = this.Palette.GetColor(ColorVariant.Light);
            this.FormTitlePanelBackgroundColorDark = this.Palette.GetColor(ColorVariant.Primary, true);
            this.FormTitlePanelBackgroundColorLight = this.Palette.GetColor(ColorVariant.Primary, false);
            this.FormControlButtonHoverColor = this.Palette.GetColor(ColorVariant.Light);
            this.CellBackgroundColor = this.Palette.GetColor(ColorVariant.Primary, false).Lighten(50);
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
