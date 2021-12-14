using System.Drawing;
using UzunTec.WinUI.Controls.Interfaces;

namespace UzunTec.WinUI.Controls.InternalContracts
{
    internal class ThemeControlWithPrefixSuffixProperties : ThemeControlProperties
    {
        internal ThemeControlWithPrefixSuffixProperties(IThemeControlWithPrefixSuffix control) : base(control) { }

        public string Prefix
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

        public string Suffix
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

        public Color PrefixSuffixTextColor
        {
            get => _prefixSuffixTextColor;
            set { _prefixSuffixTextColor = value; this.control.Invalidate(); }
        }
        private Color _prefixSuffixTextColor;
    }
}
