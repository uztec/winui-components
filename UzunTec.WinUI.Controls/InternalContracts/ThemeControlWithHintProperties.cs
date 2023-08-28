using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlWithHintProperties : ThemeControlWithTextBackgroundProperties
    {
        internal ThemeControlWithHintProperties(IThemeControlWithHint control) : base(control) { }

        public Color HintColor
        {
            get => this._hintColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._hintColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _hintColor;

        public Font HintFont
        {
            get => this._hintFont;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._hintFont = value;
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected Font _hintFont;

        public Color HintDisabledColor
        {
            get => this._hintDisabledColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._hintDisabledColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _hintDisabledColor;


        public Color HintHighlightColor
        {
            get => this._hintHighlightColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._hintHighlightColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _hintHighlightColor;

        public FontClass HintFontClass
        {
            get => this._hintFontClass;
            set
            {
                this._hintFontClass = value;
                if (this._useThemeColors)
                {
                    this.control.UpdateStylesFromTheme();
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _hintFontClass;

        public string PlaceholderHintText
        {
            get => this._placeholderHintText;
            set
            {
                this._placeholderHintText = value;
                this.control.UpdateRects();
                this.control.Invalidate();
            }
        }
        protected string _placeholderHintText;
        public bool ShowHint
        {
            get => this._showHint;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._showHint = value;
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected bool _showHint = true;
    }
}
