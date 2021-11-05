using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls
{
    public class ThemeSchemeManager
    {
        public static ThemeSchemeManager Instance = new ThemeSchemeManager();
        private readonly ThemeScheme themeScheme;

        private ThemeSchemeManager()
        {
            this.themeScheme = new ThemeSchemeLightBlue();
        }

        public ThemeScheme GetTheme()
        {
            return this.themeScheme;
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
            return ctrl.Enabled? 
                ctrl.Focused ? this.GetHighlightBrush(ctrl) : new SolidBrush(ctrl.HintColor)
                : new SolidBrush(ctrl.DisabledHintColor);
        }
    }
}
