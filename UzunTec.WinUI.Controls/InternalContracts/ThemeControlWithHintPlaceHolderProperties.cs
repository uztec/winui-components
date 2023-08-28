using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlWithHintPlaceHolderProperties : ThemeControlWithHintProperties
    {
        internal ThemeControlWithHintPlaceHolderProperties(IThemeControlWithHintPlaceholder control) : base(control) { }

        public Color PlaceholderColor
        {
            get => this._placeholderColor;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._placeholderColor = value;
                    this.control.Invalidate();
                }
            }
        }
        protected Color _placeholderColor;

        public Font PlaceholderFont
        {
            get => this._placeholderFont;
            set
            {
                if (!this._useThemeColors || this.control.UpdatingTheme)
                {
                    this._placeholderFont = value;
                    this.control.UpdateRects();
                    this.control.Invalidate();
                }
            }
        }
        protected Font _placeholderFont;
    }
}
