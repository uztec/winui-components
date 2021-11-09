using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzunTec.WinUI.Controls
{
    class ThemeSchemeDarkBlue : ThemeScheme
    {
        public ThemeSchemeDarkBlue()
        {
            this.PrimaryColor = Color.FromArgb(17, 162, 170); //ok
            this.PrimaryLightColor = Color.FromArgb(176, 189, 199); //new

            this.SecondaryColor = Color.FromArgb(26, 30, 44); //new  ok

            this.FormBackgroundColor = Color.FromArgb(40, 45, 62); //ok
            this.FormBackgroundDarkColor = Color.FromArgb(45, 50, 69); //new ok

            this.ControlTextColor = Color.FromArgb(240, 240, 240); //ok
            this.ControlTextLightColor = Color.FromArgb(240, 240, 240); //new ok
            this.DisabledControlTextColor = Color.FromArgb(73, 80, 93); //ok

            this.ControlHintTextColor = Color.FromArgb(73, 80, 93); //ok
            this.DisableControlHintTextColor = Color.FromArgb(73, 80, 93); //ok
            this.ControlPlaceholderColor = Color.FromArgb(73, 80, 93); //ok

            this.ControlHighlightColor = Color.FromArgb(17, 162, 170); //ok

            this.ControlBackgroundColorDark = Color.FromArgb(25, 31, 41); //ok
            this.ControlBackgroundColorLight = Color.FromArgb(25, 31, 41); //ok
            this.DisabledControlBackgroundColorDark = Color.FromArgb(49, 57, 71); //ok
            this.DisabledControlBackgroundColorLight = Color.FromArgb(49, 57, 71); //ok

            this.ControlTextFont = new Font("Roboto Light", 15);
            this.ControlHintFont = new Font("Roboto Medium", 6.75f, FontStyle.Bold);
            this.ControlPlaceholderFont = new Font("Roboto Light", 15);
        }
    }
}
