using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace UzunTec.WinUI.Utils
{
    public struct NCCALCSIZE_PARAMS
    {
        public RECT rcNewWindow;
        public RECT rcOldWindow;
        public RECT rcClient;
        public IntPtr lppos;
    }


    [StructLayout(LayoutKind.Sequential)]
    public struct RECT
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;

        private RECT(int left, int top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
        {
        }

        public RECT(RectangleF r) : this(r.ToRect(true))
        {
        }
    }
}
