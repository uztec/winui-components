using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzunTec.WinUI.Controls
{
    public class ThemeSchemeManager
    {
        public static ThemeSchemeManager Instance = new ThemeSchemeManager();
        private readonly ThemeScheme themeScheme;

        private ThemeSchemeManager()
        {
            this.themeScheme = new ThemeScheme();
        }

        public ThemeScheme GetTheme()
        {
            return this.themeScheme;
        }

    }
}
