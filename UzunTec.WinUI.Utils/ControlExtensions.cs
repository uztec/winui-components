using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UzunTec.WinUI.Utils
{
    public static class ControlExtensions
    {
        public static Color GetParentColor(this Control ctrl)
        {
            Control parent = ctrl.Parent;
            while (parent.BackColor.A == 0 && parent.Parent != null)
            {
                parent = parent.Parent;
            }
            return parent.BackColor;
        }
    }
}
