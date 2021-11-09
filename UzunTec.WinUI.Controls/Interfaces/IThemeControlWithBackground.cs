using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzunTec.WinUI.Controls.Interfaces
{
    internal interface IThemeControlWithBackground : IThemeControl
    {
        Color BackgroundColorDark { get; }
        Color BackgroundColorLight { get; }
        Color DisabledBackgroundColorDark { get; }
        Color DisabledBackgroundColorLight { get; }
        Color FocusedBackgroundColorDark { get; }
        Color FocusedBackgroundColorLight { get; }

    }
}
