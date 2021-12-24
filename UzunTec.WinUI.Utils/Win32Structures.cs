using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UzunTec.WinUI.Utils
{
    public struct RECT { public int Left, Top, Right, Bottom; }
    public struct NCCALCSIZE_PARAMS
    {
        public RECT rcNewWindow;
        public RECT rcOldWindow;
        public RECT rcClient;
        IntPtr lppos;
    }
}
