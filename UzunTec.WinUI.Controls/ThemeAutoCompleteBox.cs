using System;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeAutoCompleteBox : ComboBox
    {
        public string Mask { get; set; }


        public ThemeAutoCompleteBox()
        {
            this.DropDownStyle = ComboBoxStyle.Simple;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            //this.DrawMode = DrawMode.OwnerDrawFixed;
            this.Height = 20;
        }


        public bool MyCondition { get; set; }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            if (this.Text.Length > 2)
            {
                this.DropDownStyle = ComboBoxStyle.DropDown;
                this.DroppedDown = true;
            }
            else
            {
                this.DropDownStyle = ComboBoxStyle.Simple;
            }
        }
       
        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
        //protected override void OnDropDown(EventArgs e)
        //{
        //    this.DropDownHeight = (MyCondition) ? 1 : 200;
        //    base.OnDropDown(e);
        //}

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            if (!MyCondition)
            {
                base.OnDrawItem(e);
                e.DrawBackground();
                e.Graphics.DrawString(this.Items[e.Index].ToString() + " - abc", this.Font, new SolidBrush(this.ForeColor), e.Bounds);
                e.DrawFocusRectangle();
            }
        }

        private const int WM_REFLECT = Win32ApiConstants.WM_REFLECT;
        private const int WM_COMMAND = Win32ApiConstants.WM_COMMAND;
        private const int CBN_DROPDOWN = Win32ApiConstants.CBN_DROPDOWN;
        private const int CB_GETDROPPEDSTATE = Win32ApiConstants.CB_GETDROPPEDSTATE;
        private const int CB_SHOWDROPDOWN = Win32ApiConstants.CB_SHOWDROPDOWN;

        //protected override void WndProc(ref Message m)
        //{
        //    List<int> messagesToIgnore = new List<int> { CBN_DROPDOWN, CB_GETDROPPEDSTATE, CB_SHOWDROPDOWN };

        //    if (m.Msg == (WM_REFLECT + WM_COMMAND))
        //    {
        //        if (messagesToIgnore.Contains(HIWORD((int)m.WParam)))
        //        {
        //            this.Text += "123";
        //            return;
        //        }
        //    }
        //    base.WndProc(ref m);
        //}

        public static int HIWORD(int n)
        {
            return (n >> 16) & 0xffff;
        }
    }
}
