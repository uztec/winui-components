using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    class ThemeControlWithTextBackgroundProperties : ThemeControlProperties
    {
        internal ThemeControlWithTextBackgroundProperties(IThemeControlWithTextBackground control) : base(control) { }

        public Font TextFont
        {
            get => this._textFont;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._textFont = value;
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected Font _textFont;


        public Color TextColor
        {
            get => this._textColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._textColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _textColor;

        public Color TextColorDisabled
        {
            get => this._textColorDisabled;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._textColorDisabled = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _textColorDisabled;

     
        public Color BackgroundColorDark
        {
            get => this._backgroundColorDark;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorDark = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _backgroundColorDark;
        public Color BackgroundColorLight
        {
            get => this._backgroundColorLight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorLight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _backgroundColorLight;

        public Color BackgroundColorDisabledDark
        {
            get => this._backgroundColorDisabledDark;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorDisabledDark = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _backgroundColorDisabledDark;

        public Color BackgroundColorDisabledLight
        {
            get => this._backgroundColorDisabledLight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorDisabledLight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _backgroundColorDisabledLight;

        public Color BackgroundColorFocusedDark
        {
            get => this._backgroundColorFocusedDark;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorFocusedDark = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _backgroundColorFocusedDark;

        public Color BackgroundColorFocusedLight
        {
            get => this._backgroundColorFocusedLight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorFocusedLight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _backgroundColorFocusedLight;
    }
}
