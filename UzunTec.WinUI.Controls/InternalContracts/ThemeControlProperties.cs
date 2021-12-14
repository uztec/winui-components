using System;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlProperties
    {
        protected readonly IThemeControl control;
        protected bool updatingTheme;

        internal ThemeControlProperties(IThemeControl control)
        {
            this.control = control;
            control.HandleCreated += Control_HandleCreated;

            ThemeSchemeManager.Instance.Changed += (s, t) =>
            {
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    control.UpdateRects();
                    control.Invalidate();
                }
            };

            this._useThemeColors = true;
            this.updatingTheme = true;
        }

        private void Control_HandleCreated(object sender, EventArgs e)
        {
            this.updatingTheme = true;
            if (this._useThemeColors)
            {
                this.DoUpdateStylesFromTheme();
            }
        }

        protected void DoUpdateStylesFromTheme()
        {
            this.updatingTheme = true;
            this.control.UpdateStylesFromTheme();
            this.updatingTheme = false;
        }


        // ********************  Properties ********************/

        public bool UseThemeColors
        {
            get => this._useThemeColors;
            set
            {
                if (value && !this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
                this._useThemeColors = value;
            }
        }
        protected bool _useThemeColors;
        public Padding InternalPadding
        {
            get => this._internalPadding;
            set
            {
                this._internalPadding = value;
                this.control.UpdateRects();
                this.control.Invalidate();
            }
        }
        protected Padding _internalPadding;
    }
}
