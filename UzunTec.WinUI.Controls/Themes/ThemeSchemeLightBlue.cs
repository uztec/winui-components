using System.Drawing;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Themes
{
    public class ThemeSchemeLightBlue : ThemeScheme
    {
        public ThemeSchemeLightBlue()
        {
            this.ControlTextFont = this.FontClasses[FontClass.Body];
            this.FormTitleFont = this.FontClasses[FontClass.H1];
            this.FormHeaderTextFont = this.FontClasses[FontClass.H5];
            this.ControlPlaceholderFont = this.FontClasses[FontClass.Body];
            this.GridFont = this.FontClasses[FontClass.H6];
            this.GridHeaderFont = this.FontClasses[FontClass.H5];

            this.ThemeHighlightColor = Color.FromArgb(116, 173, 80);
            this.ControlTextColorDark = Color.FromArgb(47, 53, 66);
            this.ControlTextColorLight = Color.FromArgb(240, 240, 240);
            this.ControlTextColorDisabled = Color.FromArgb(133, 143, 154);
            this.ControlBackgroundColorDark = Color.FromArgb(224, 224, 230);
            this.ControlBackgroundColorLight = Color.FromArgb(234, 234, 238);
            this.ControlBackgroundColorFocusedDark = Color.FromArgb(236, 236, 238);
            this.ControlBackgroundColorFocusedLight = Color.FromArgb(236, 237, 241);
            this.ControlBackgroundColorDisabledDark = Color.FromArgb(240, 240, 240);
            this.ControlBackgroundColorDisabledLight = Color.FromArgb(250, 250, 250);

            this.ControlHintTextColor = Color.FromArgb(133, 143, 154);
            this.ControlHintTextColorDisabled = Color.FromArgb(133, 143, 154);
            this.ControlPlaceholderTextColor = Color.FromArgb(133, 143, 154);
            this.ThemeSelectionColorDark = Color.FromArgb(34, 50, 63);
            this.ThemeSelectionColorLight = Color.FromArgb(176, 189, 199);
            this.ThemeSelectionColorExtraLight = Color.FromArgb(235, 235, 235);

            this.FormBackgroundColor = Color.GhostWhite;
            this.FormHeaderTextColor = Color.FromArgb(240, 240, 240);
            this.FormHeaderColorDark = Color.FromArgb(26, 41, 53);
            this.FormHeaderColorLight = Color.FromArgb(26, 41, 53);
            this.FormTitleTextColor = Color.FromArgb(240, 240, 240);
            this.FormTitlePanelBackgroundColorDark = Color.FromArgb(14, 25, 36);
            this.FormTitlePanelBackgroundColorLight = Color.FromArgb(14, 25, 36).Lighten(60);
            this.FormControlButtonHoverColor = Color.FromArgb(62, 88, 115);

            this.CellBackgroundColor = Color.FromArgb(240, 240, 240);

            //this.FormTitleBarFont = ThemeSchemeManager.Instance.GetFont("Segoe UI SemiBold", 17.25F, FontStyle.Bold);
            //this.ControlTextFont = ThemeSchemeManager.Instance.GetFont("Segoe UI", 13);
            //this.ControlPlaceholderFont = ThemeSchemeManager.Instance.GetFont("Segoe UI SemiLight", 13);
        }
    }
}
