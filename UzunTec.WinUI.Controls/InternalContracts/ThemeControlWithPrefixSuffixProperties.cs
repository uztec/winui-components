using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

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

        public Color SuffixTextColor
        {
            get => _suffixTextColor;
            set { _suffixTextColor = value; this.control.Invalidate(); }
        }
        private Color _suffixTextColor;

    }
}
