using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeButtonProperties : ThemeControlProperties
    {
        internal ThemeButtonProperties(IThemeControl control) : base(control) { }

        public Color TextColorHighlight
        {
            get => this._textColorHighlight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._textColorHighlight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _textColorHighlight;

        public Color BorderColor
        {
            get => this._borderColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._borderColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _borderColor;


        public Color BorderColorDisabled
        {
            get => this._borderColorDisabled;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._borderColorDisabled = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _borderColorDisabled;


        public Color BorderColorHighlight
        {
            get => this._borderColorHighlight;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._borderColorHighlight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _borderColorHighlight;



        public ColorVariant TextColorVariant
        {
            get => this._textColorVariant;
            set
            {
                this._textColorVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _textColorVariant;





        public ColorVariant TextColorHightlightVariant
        {
            get => this._textColorHightlightVariant;
            set
            {
                this._textColorHightlightVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _textColorHightlightVariant;

        public ColorVariant TextColorDisabledVariant
        {
            get => this._textColorDisabledVariant;
            set
            {
                this._textColorDisabledVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _textColorDisabledVariant;


        public ColorVariant BackgroundColorVariant
        {
            get => this._backgroundColorVariant;
            set
            {
                this._backgroundColorVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _backgroundColorVariant;


        public ColorVariant BackgroundColorHighlightVariant
        {
            get => this._backgroundColorHightlightVariant;
            set
            {
                this._backgroundColorHightlightVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _backgroundColorHightlightVariant;

        public ColorVariant BackgroundColorDisabledVariant
        {
            get => this._backgroundColorDisabledVariant;
            set
            {
                this._backgroundColorDisabledVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _backgroundColorDisabledVariant;

        public ColorVariant BorderColorVariant
        {
            get => this._borderColorVariant;
            set
            {
                this._borderColorVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _borderColorVariant;


        public ColorVariant BorderColorHighlightVariant
        {
            get => this._borderColorHightlightVariant;
            set
            {
                this._borderColorHightlightVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _borderColorHightlightVariant;

        public ColorVariant BorderColorDisabledVariant
        {
            get => this._borderColorDisabledVariant;
            set
            {
                this._borderColorDisabledVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _borderColorDisabledVariant;

        public int BorderWidth { get => _borderWidth; set { _borderWidth = value; this.control.Invalidate(); } }
        private int _borderWidth;

        public bool Transparent { get => _transparent; set { _transparent = value; this.control.Invalidate(); } }
        private bool _transparent;


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

        public FontClass TextFontClass
        {
            get => this._textFontClass;
            set
            {
                this._textFontClass = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _textFontClass;
    }
}
