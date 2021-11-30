using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls
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
            this.selectedThemeScheme = new ThemeSchemeLightBlue();
            this.Changed?.Invoke(this, this.selectedThemeScheme);
        }

        public ThemeScheme GetTheme()
        {
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


        internal Brush GetFocusedBackgroundBrush(IThemeControlWithBackground ctrl)
        {
            return new LinearGradientBrush(ctrl.ClientRectangle, ctrl.FocusedBackgroundColorDark, ctrl.FocusedBackgroundColorLight, LinearGradientMode.Vertical);
        }

        internal Brush GetBackgroundBrush(IThemeControlWithBackground ctrl)
        {
            return new LinearGradientBrush(ctrl.ClientRectangle, ctrl.BackgroundColorDark, ctrl.BackgroundColorLight, LinearGradientMode.Vertical);
        }

        internal Brush GetDisabledBackgroundBrush(IThemeControlWithBackground ctrl)
        {
            return new LinearGradientBrush(ctrl.ClientRectangle, ctrl.DisabledBackgroundColorDark, ctrl.DisabledBackgroundColorLight, LinearGradientMode.Vertical);
        }

        internal Brush GetHighlightBrush(IThemeControl ctrl)
        {
            return new SolidBrush(ctrl.HighlightColor);
        }

        internal Brush GetTextBrush(IThemeControl ctrl)
        {
            return ctrl.Enabled ? new SolidBrush(ctrl.TextColor) : new SolidBrush(ctrl.DisabledTextColor);
        }

        internal Brush GetHintBrush(IThemeControlWithHint ctrl)
        {
            return ctrl.Enabled ?
                ctrl.Focused ? this.GetHighlightBrush(ctrl) : new SolidBrush(ctrl.HintColor)
                : new SolidBrush(ctrl.DisabledHintColor);
        }


    }
}
