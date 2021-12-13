using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.Themes
{
    public class ThemeSchemeManager
    {
        public event EventHandler<ThemeScheme> Changed;

        public static ThemeSchemeManager Instance = new ThemeSchemeManager();
        
        private readonly FontFamilyManager fontFamilyManager;
        private ThemeScheme selectedThemeScheme;

        private ThemeSchemeManager()
        {
            this.fontFamilyManager = new FontFamilyManager();
            this.selectedThemeScheme = null;
            this.Changed?.Invoke(this, this.selectedThemeScheme);
        }

        private ThemeScheme GetFirstTheme()
        {
            return new ThemeSchemeLightBlue();
        }

        public ThemeScheme GetTheme()
        {
            if (this.selectedThemeScheme == null)
            {
                this.selectedThemeScheme = this.GetFirstTheme();
            }
            return this.selectedThemeScheme;

        }

        public void ChangeTheme(ThemeScheme theme)
        {
            this.selectedThemeScheme = theme ?? throw new NullReferenceException();
            this.Changed?.Invoke(this, this.selectedThemeScheme);

        }

        public Font GetFont(string familyName, float size, FontStyle fontStyle, GraphicsUnit unit = GraphicsUnit.Point)
        {
            return new Font(this.fontFamilyManager.GetFamily(familyName), size, fontStyle, unit);
        }

        public Font GetFont(string familyName, float size, GraphicsUnit unit = GraphicsUnit.Point)
        {
            return new Font(this.fontFamilyManager.GetFamily(familyName), size, unit);
        }


        internal Brush GetFocusedBackgroundBrush(IThemeControlWithTextBackground ctrl)
        {
            return new LinearGradientBrush(ctrl.ClientRectangle, ctrl.BackgroundColorFocusedDark, ctrl.BackgroundColorFocusedLight, LinearGradientMode.Vertical);
        }

        internal Brush GetBackgroundBrush(IThemeControlWithTextBackground ctrl)
        {
            return new LinearGradientBrush(ctrl.ClientRectangle, ctrl.BackgroundColorDark, ctrl.BackgroundColorLight, LinearGradientMode.Vertical);
        }

        internal Brush GetDisabledBackgroundBrush(IThemeControlWithTextBackground ctrl)
        {
            return new LinearGradientBrush(ctrl.ClientRectangle, ctrl.BackgroundColorDisabledDark, ctrl.BackgroundColorDisabledLight, LinearGradientMode.Vertical);
        }

        internal Brush GetHighlightBrush(IThemeControl ctrl)
        {
            return new SolidBrush(ctrl.HighlightColor);
        }

        internal Brush GetTextBrush(IThemeControl ctrl)
        {
            return ctrl.Enabled ? new SolidBrush(ctrl.TextColor) : new SolidBrush(ctrl.TextColorDisabled);
        }

        internal Brush GetHintBrush(IThemeControlWithHint ctrl)
        {
            return ctrl.Enabled ?
                ctrl.Focused ? this.GetHighlightBrush(ctrl) : new SolidBrush(ctrl.HintColor)
                : new SolidBrush(ctrl.HintDisabledColor);
        }


    }
}
