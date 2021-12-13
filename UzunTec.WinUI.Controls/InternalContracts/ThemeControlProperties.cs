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
        internal Action UpdateStylesFromTheme { get; set; }

        internal ThemeControlProperties(IThemeControl control)
        {
            this.control = control;
            control.HandleCreated += Control_HandleCreated;
            this.UpdateStylesFromTheme = delegate () { };

            ThemeSchemeManager.Instance.Changed += (s, t) =>
            {
                if (this._useThemeColors)
                {
                    this.DoUpdate();
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
                this.DoUpdate();
            }
        }

        private void DoUpdate()
        {
            this.updatingTheme = true;
            this.UpdateStylesFromTheme();
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
                    this.DoUpdate();
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
