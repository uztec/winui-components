using System.Drawing;

namespace UzunTec.WinUI.Controls
{
    public class ThemeScheme
    {
        public Color PrimaryColor { get; set; }
        public Color PrimaryLightColor { get; set; }
        public Color SecondaryColor { get; set; }
        public Color FormBackgroundColor { get; set; }
        public Color FormBackgroundDarkColor { get; set; }
        public Color ControlTextColor { get; set; }
        public Color PrefixSuffixTextColor { get; set; }
        public Color ControlTextLightColor { get; set; }
        public Color ControlBackgroundColorLight { get; set; }
        public Color ControlBackgroundColorDark { get; set; }
        public Color ControlHighlightColor { get; set; }
        public Color ControlHintTextColor { get; set; }
        public Color ControlPlaceholderColor { get; set; }
        public Color FormTitleBarColor { get; set; }

        #region Fonts
        public Font ControlTextFont { get; set; }
        public Font ControlHintFont { get; set; }
        public Font ControlPlaceholderFont { get; set; }
        public Font FormTitleBarFont { get; set; }
        #endregion

        #region Control if disabled
        public Color DisabledControlTextColor { get; set; }
        public Color DisabledControlBackgroundColorLight { get; set; }
        public Color DisabledControlBackgroundColorDark { get; set; }
        public Color DisableControlHintTextColor { get; set; }
        #endregion

        public ThemeScheme()
        {
            this.PrimaryColor = Color.FromArgb(240, 240, 240);
            this.PrimaryLightColor = Color.FromArgb(240, 240, 240); //new

            this.SecondaryColor = Color.FromArgb(240, 240, 240); //new

            this.FormBackgroundColor = Color.FromArgb(240, 240, 240);
            this.FormBackgroundDarkColor = Color.FromArgb(240, 240, 240); //new

            this.ControlTextColor = Color.Black;
            this.PrefixSuffixTextColor = Color.Black; //new
            this.ControlTextLightColor = Color.Black; //new
            this.DisabledControlTextColor = Color.DarkGray;

            this.ControlHintTextColor = Color.DarkGray;
            this.DisableControlHintTextColor = Color.Gray;
            this.ControlPlaceholderColor = Color.FromArgb(200, Color.DarkGray);

            this.ControlHighlightColor = Color.Purple;

            this.ControlBackgroundColorDark = Color.FromArgb(220, 220, 220);
            this.ControlBackgroundColorLight = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorDark = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorLight = Color.FromArgb(250, 250, 250);

            this.FormTitleBarFont = new Font("Segoe UI Semibold", 17.25F, FontStyle.Bold);
            this.ControlTextFont = new Font("Segoe UI", 15);
            this.ControlHintFont = new Font("Segoe UI", 7);
            this.ControlPlaceholderFont = new Font("Segoe UI", 15);
        }

        /*
         * Background - Tela = f0f0f0
            Cor do Texto Branco = f0f0f0
            Cor de Texto Enabled = 2f3542
            Cor de Texto Disabled = 858f9a
            Cor de Hint = 858f9a
            Cor de Accent 1 = 74ad50
            Cor de Accent 2 = 205a4f
        */
    }
}
