using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlWithPrefixSuffixProperties : ThemeControlProperties
    {
        internal ThemeControlWithPrefixSuffixProperties(IThemeControlWithPrefixSuffix control) : base(control) { }

        public string PrefixText
        {
            get => _prefixText;
            set { _prefixText = value; this.control.UpdateRects(); this.control.Invalidate(); }
        }
        private string _prefixText = string.Empty;

        public Font PrefixFont
        {
            get => _prefixFont;
            set { _prefixFont = value; this.control.UpdateRects(); this.control.Invalidate(); }
        }
        private Font _prefixFont;

        public string SuffixText
        {
            get => _suffixText;
            set { _suffixText = value; this.control.UpdateRects(); this.control.Invalidate(); }
        }
        private string _suffixText = string.Empty;

        public Font SuffixFont
        {
            get => _suffixFont;
            set { _suffixFont = value; this.control.UpdateRects(); this.control.Invalidate(); }
        }
        private Font _suffixFont;

        public Color PrefixTextColor 
        {
            get => _prefixTextColor;
            set { _prefixTextColor = value; this.control.Invalidate(); }
        }
        private Color _prefixTextColor;

        public Color PrefixTextHighlightColor
        {
            get => _prefixTextHighlightColor;
            set { _prefixTextHighlightColor = value; this.control.Invalidate(); }
        }
        private Color _prefixTextHighlightColor;

        public Color PrefixTextColorDisabled
        {
            get => _prefixTextColorDisabled;
            set { _prefixTextColorDisabled = value; this.control.Invalidate(); }
        }
        private Color _prefixTextColorDisabled;

        public FontClass PrefixFontClass
        {
            get => this._prefixFontClass;
            set
            {
                this._prefixFontClass = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _prefixFontClass;

        public ColorVariant PrefixTextColorVariant
        {
            get => this._prefixTextColorVariant;
            set { this._prefixTextColorVariant = value; this.control.Invalidate(); }
        }
        protected ColorVariant _prefixTextColorVariant;

        public ColorVariant PrefixTextColorHightlightVariant
        {
            get => this._prefixTextColorHightlightVariant;
            set { this._prefixTextColorHightlightVariant = value; this.control.Invalidate(); }
        }
        protected ColorVariant _prefixTextColorHightlightVariant;

        public Color SuffixTextColor
        {
            get => _suffixTextColor;
            set { _suffixTextColor = value; this.control.Invalidate(); }
        }
        private Color _suffixTextColor;


        public Color SuffixTextHighlightColor
        {
            get => _suffixTextHighlightColor;
            set { _suffixTextHighlightColor = value; this.control.Invalidate(); }
        }
        private Color _suffixTextHighlightColor;

        public Color SuffixTextColorDisabled
        {
            get => _suffixTextColorDisabled;
            set { _suffixTextColorDisabled = value; this.control.Invalidate(); }
        }
        private Color _suffixTextColorDisabled;

        public FontClass SuffixFontClass
        {
            get => this._suffixFontClass;
            set
            {
                this._suffixFontClass = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _suffixFontClass;

        public ColorVariant SuffixTextColorVariant
        {
            get => this._suffixTextColorVariant;
            set { this._suffixTextColorVariant = value; this.control.Invalidate(); }
        }
        protected ColorVariant _suffixTextColorVariant;

        public ColorVariant SuffixTextColorHightlightVariant
        {
            get => this._suffixTextColorHightlightVariant;
            set { this._suffixTextColorHightlightVariant = value; this.control.Invalidate(); }
        }
        protected ColorVariant _suffixTextColorHightlightVariant;
    }
}
