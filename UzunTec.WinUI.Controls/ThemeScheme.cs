﻿using System.Drawing;

namespace UzunTec.WinUI.Controls
{
    public class ThemeScheme
    {
        public Color FormBackgroundColor { get; set; }
        public Color ControlTextColor { get; set; }
        public Color ControlBackgroundColorLight { get; set; }
        public Color ControlBackgroundColorDark { get; set; }
        public Color ControlHighlightColor { get; set; }
        public Color ControlHintTextColor { get; set; }
        public Color ControlPlaceholderColor { get; set; }

        #region Fonts
        public Font ControlTextFont { get; set; }
        public Font ControlHintFont { get; set; }
        public Font ControlPlaceholderFont { get; set; }
        #endregion

        #region Control if disabled
        public Color DisabledControlTextColor { get; set; }
        public Color DisabledControlBackgroundColorLight { get; set; }
        public Color DisabledControlBackgroundColorDark { get; set; }
        public Color DisableControlHintTextColor { get; set; }
        #endregion

        public ThemeScheme()
        {
            this.FormBackgroundColor = Color.FromArgb(240, 240, 240);

            this.ControlTextColor = Color.FromArgb(255 * 0x100000 + 0x2f3542);
            this.DisabledControlTextColor = Color.FromArgb(255 * 0x100000 + 0x858f9a);

            this.ControlHintTextColor = Color.FromArgb(255 * 0x100000 + 0x858f9a);
            this.DisableControlHintTextColor = Color.FromArgb(255 * 0x100000 + 0x858f9a);
            this.ControlPlaceholderColor = Color.FromArgb(255 * 0x100000 + 0x858f9a);

            this.ControlHighlightColor = Color.FromArgb(255 * 0x100000 +  0x74ad50);

            this.ControlBackgroundColorDark = Color.FromArgb(220, 220, 220);
            this.ControlBackgroundColorLight = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorDark = Color.FromArgb(240, 240, 240);
            this.DisabledControlBackgroundColorLight = Color.FromArgb(250, 250, 250);

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
