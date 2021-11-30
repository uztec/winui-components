using System;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlProperties
    {
        protected readonly IThemeControl control;
        protected bool updatingTheme;
        internal Action Invalidate { get; set; }
        internal Action UpdateRects { get; set; }
        internal Action UpdateDataFromTheme { get; set; }

        internal ThemeControlProperties(IThemeControl control)
        {
            this.control = control;
            control.HandleCreated += Control_HandleCreated;

            this.Invalidate = delegate () { };
            this.UpdateRects = delegate () { };
            this.UpdateDataFromTheme = delegate () { };

            ThemeSchemeManager.Instance.Changed += (s, t) =>
            {
                if (this._useThemeColors)
                {
                    this.DoUpdate();
                    this.UpdateRects();
                    this.Invalidate();
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
            this.UpdateDataFromTheme();
            this.updatingTheme = false;
        }

        public bool UseThemeColors
        {
            get => this._useThemeColors;
            set
            {
                if (value && !this._useThemeColors)
                {
                    this.DoUpdate();
                    this.Invalidate();
                }
                this._useThemeColors = value;
            }
        }
        protected bool _useThemeColors;


        public Color HighlightColor
        {
            get => this._highlightColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._highlightColor = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _highlightColor;

        public Color DisabledTextColor
        {
            get => this._disabledTextColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._disabledTextColor = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _disabledTextColor;

        public Color TextColor
        {
            get => this._textColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._textColor = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _textColor;


        public Padding InternalPadding
        {
            get => this._internalPadding;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._internalPadding = value;
                    this.UpdateRects();
                    this.Invalidate();
                }
            }
        }
        protected Padding _internalPadding;

    }
}
