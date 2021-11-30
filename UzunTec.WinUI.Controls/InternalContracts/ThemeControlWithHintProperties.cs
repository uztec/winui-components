using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlWithHintProperties : ThemeControlWithBackgroundProperties
    {
        internal ThemeControlWithHintProperties(IThemeControlWithHint control) : base(control) { }

        public Color HintColor
        {
            get => this._hintColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._hintColor = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _hintColor;

        public Font HintFont
        {
            get => this._hintFont;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._hintFont = value;
                    this.UpdateRects();
                    this.Invalidate();
                }
            }
        }
        protected Font _hintFont;
        public Color PlaceholderColor
        {
            get => this._placeholderColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._placeholderColor = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _placeholderColor;

        public Font PlaceholderFont
        {
            get => this._placeholderFont;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._placeholderFont = value;
                    this.UpdateRects();
                    this.Invalidate();
                }
            }
        }
        protected Font _placeholderFont;
        public Color DisabledHintColor
        {
            get => this._disabledHintColor;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._disabledHintColor = value;
                    this.Invalidate();
                }
            }
        }
        protected Color _disabledHintColor;

        public string PlaceholderHintText
        {
            get => this._placeholderHintText;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._placeholderHintText = value;
                    this.UpdateRects();
                    this.Invalidate();
                }
            }
        }
        protected string _placeholderHintText;
        public bool ShowHint
        {
            get => this._showHint;
            set
            {
                if (!this._useThemeColors || this.updatingTheme)
                {
                    this._showHint = value;
                    this.UpdateRects();
                    this.Invalidate();
                }
            }
        }
        protected bool _showHint;
    }
}
