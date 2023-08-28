using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.Themes;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemePostitProperties : ThemeControlProperties
    {
        internal ThemePostitProperties(IThemeControl control) : base(control) { }

        public Color HeaderColorDark
        {
            get => this._headerColorDark;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._headerColorDark = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _headerColorDark;

        public Color HeaderColorLight
        {
            get => this._headerColorLight;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._headerColorLight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _headerColorLight;

        public ColorVariant HeaderColorVariant
        {
            get => this._headerColorVariant;
            set
            {
                this._headerColorVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _headerColorVariant;

        public Color BodyColorDark
        {
            get => this._bodyColorDark;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._bodyColorDark = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _bodyColorDark;


        public Color BodyColorLight
        {
            get => this._bodyColorLight;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._bodyColorLight = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _bodyColorLight;

        public ColorVariant PostitBackgroundColorVariant
        {
            get => this._postitBackgroundColorVariant;
            set
            {
                this._postitBackgroundColorVariant = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected ColorVariant _postitBackgroundColorVariant;

        public Color HeaderTextColor
        {
            get => this._headerTextColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._headerTextColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _headerTextColor;

        public Color BodyTextColor
        {
            get => this._bodyTextColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._bodyTextColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _bodyTextColor;

        public Color DateTextColor
        {
            get => this._dateTextColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._dateTextColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _dateTextColor;

        public Font HeaderFont
        {
            get => this._headerFont;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._headerFont = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Font _headerFont;

        public FontClass HeaderFontClass
        {
            get => this._headerFontClass;
            set
            {
                this._headerFontClass = value;
                if (this._useThemeColors)
                {
                    this.control.UpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _headerFontClass;

        public Font DateFont
        {
            get => this._dateFont;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._dateFont = value;
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected Font _dateFont;

        public FontClass DateFontClass
        {
            get => this._dateFontClass;
            set
            {
                this._dateFontClass = value;
                if (this._useThemeColors)
                {
                    this.control.UpdateStylesFromTheme();
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _dateFontClass;

        public Font BodyFont
        {
            get => this._bodyFont;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._bodyFont = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Font _bodyFont;

        public FontClass BodyFontClass
        {
            get => this._bodyFontClass;
            set
            {
                this._bodyFontClass = value;
                if (this._useThemeColors)
                {
                    this.control.UpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected FontClass _bodyFontClass;

        public Color BorderColor
        {
            get => this._borderColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._borderColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _borderColor;

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

        public int BorderWidth
        {
            get => this._borderWidth;
            set
            {
                this._borderWidth = value;
                if (this._useThemeColors)
                {
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        private int _borderWidth;

        public bool PostitFormat
        {
            get => this._postitFormat;
            set
            {
                this._postitFormat = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected bool _postitFormat;

        public int Lighten
        {
            get => this._lighten;
            set
            {
                this._lighten = value;
                if (this._useThemeColors)
                {
                    this.DoUpdateStylesFromTheme();
                    this.control.Invalidate();
                }
            }
        }
        protected int _lighten;
    }
}
