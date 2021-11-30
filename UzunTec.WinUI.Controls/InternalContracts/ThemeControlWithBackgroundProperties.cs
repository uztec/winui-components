using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    class ThemeControlWithBackgroundProperties : ThemeControlProperties
    {
        internal ThemeControlWithBackgroundProperties(IThemeControlWithBackground control) : base(control) { }

        public Color BackgroundColorDark
        {
            get => this._backgroundColorDark;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._backgroundColorDark = value;
                    this.Invalidate();
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
                    this.Invalidate();
                }
            }
        }
        protected Color _backgroundColorLight;

        public Color DisabledBackgroundColorDark
        {
            get => this._disabledBackgroundColorDark;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._disabledBackgroundColorDark = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _disabledBackgroundColorDark;

        public Color DisabledBackgroundColorLight
        {
            get => this._dsabledBackgroundColorLight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._dsabledBackgroundColorLight = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _dsabledBackgroundColorLight;

        public Color FocusedBackgroundColorDark
        {
            get => this._focusedBackgroundColorDark;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._focusedBackgroundColorDark = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _focusedBackgroundColorDark;

        public Color FocusedBackgroundColorLight
        {
            get => this._focusedBackgroundColorLight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._focusedBackgroundColorLight = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _focusedBackgroundColorLight;
    }
}
