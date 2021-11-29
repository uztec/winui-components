using System.Drawing;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls
{
    public class ThemeSchemeLightBlue : ThemeScheme
    {

        public ThemeSchemeLightBlue()
        {
            this.PrimaryColor = Color.FromArgb(26, 41, 53);
            this.PrimaryLightColor = Color.FromArgb(176, 189, 199); //new

            this.SecondaryColor = Color.FromArgb(14, 25, 36); //new

            this.FormBackgroundColor = Color.GhostWhite;
            this.FormBackgroundDarkColor = Color.FromArgb(240, 240, 240); //new

            this.ControlTextColor = Color.FromArgb(47, 53, 66);
            this.PrefixSuffixTextColor = Color.FromArgb(133, 143, 154); //new
            this.ControlTextLightColor = Color.FromArgb(240, 240, 240); //new
            this.DisabledControlTextColor = Color.FromArgb(133, 143, 154);

            this.ControlHintTextColor = Color.FromArgb(133, 143, 154);
            this.DisableControlHintTextColor = Color.FromArgb(133, 143, 154);
            this.ControlPlaceholderColor = Color.FromArgb(133, 143, 154);

            this.ControlHighlightColor = Color.FromArgb(116, 173, 80);

            this.ControlBackgroundColorDark = Color.FromArgb(224, 224, 230);
            this.ControlBackgroundColorLight = Color.FromArgb(234, 234, 238);
            this.DisabledControlBackgroundColorDark = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorLight = Color.FromArgb(250, 250, 250);

            this.FormTitleBarFont = new Font("Segoe UI Semibold", 17.25F, FontStyle.Bold); //new

            this.ControlTextFont = new Font("Segoe UI", 13);
            this.ControlHintFont = new Font("Segoe UI", 8f);
            this.ControlPlaceholderFont = new Font("Segoe UI Semilight", 13);
            this.HintControlInternalPadding = new Padding(4);
        }
    }
}
