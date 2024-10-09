using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class ThemeModalBase : CustomBorderForm
    {
        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false), ReadOnly(true)]
        public new bool Sizable { get; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Warning")]
        public ColorVariant BorderColorVariant { get => _borderColorVariant; set { _borderColorVariant = value; UpdateBorderColor(); Invalidate(); } }
        private ColorVariant _borderColorVariant;
                
        public ThemeModalBase()
        {
            base.Sizable = false;
            this.BorderWidth = 3;
            this.BorderColorVariant = ColorVariant.Dark;

            // Theme
            BackColor = ThemeScheme.FormBackgroundColor;
        }


        private void UpdateBorderColor()
        {
            BorderColorDark = ThemeScheme.GetPaletteColor(_borderColorVariant, true);
            BorderColorLight = ThemeScheme.GetPaletteColor(_borderColorVariant, false);
        }

    }
}
