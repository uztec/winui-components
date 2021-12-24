using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzunTec.WinUI.Utils
{
    public static class Win32ApiExtensions
    {
        public static Rectangle ToRectangle(this RECT rect)
        {
            return new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
        }

        public static RECT ToRectangle(this Rectangle rect)
        {
            return new RECT { Left = rect.Left, Top = rect.Top, Right = rect.Right, Bottom = rect.Bottom };
        }
    }
}
