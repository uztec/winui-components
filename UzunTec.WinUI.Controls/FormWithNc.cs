using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class FormWithNc : Form
    {

        private Graphics ncGrapichs = null;
        private IntPtr wndHdc;

        public event PaintEventHandler NcPaint;
        public event EventHandler NcAreaChanged;
        public event MouseEventHandler NcMouseMove;
        public event MouseEventHandler NcMouseLeave;
        public event MouseEventHandler NcMouseDown;
        public event MouseEventHandler NcMouseUp;

        public Padding NonClientArea { get; private set; }
        public Rectangle RectClient2 { get; private set; }
        public Rectangle RectNewWindow { get; private set; }
        public Rectangle RectOldWindow { get; private set; }
        public Rectangle RectClient { get; private set; }

        private Padding _nonClientAreaAdjust = new Padding(0);


        public void AdjustNonClientArea(Padding padding)
        {
            this._nonClientAreaAdjust = _nonClientAreaAdjust.AddPadding(padding);
            //this.NonClientArea = this.NonClientArea.AddPadding(padding);
            this.InvalidateAll();
        }

        public FormWithNc()
        {
            // this.ControlBox = false;
            this.SizeChanged += (s, e) => InvalidateAll();
        }

        private void InvalidateAll()
        {
            this.Invalidate();
            Win32ApiFunction.SendMessage(this.Handle, Win32ApiConstants.WM_NCPAINT, 1, 0); ;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Win32ApiFunction.SetWindowTheme(this.Handle, string.Empty, string.Empty);
        }

        private readonly List<int> NcMouseWindowsMessages = new List<int> {
            Win32ApiConstants.WM_NCMOUSEMOVE,Win32ApiConstants.WM_NCMOUSELEAVE,
            Win32ApiConstants.WM_NCLBUTTONDOWN, Win32ApiConstants.WM_NCLBUTTONUP, Win32ApiConstants.WM_NCLBUTTONDBLCLK,
            Win32ApiConstants.WM_NCRBUTTONDOWN, Win32ApiConstants.WM_NCRBUTTONUP, Win32ApiConstants.WM_NCRBUTTONDBLCLK,
            Win32ApiConstants.WM_NCMBUTTONDOWN, Win32ApiConstants.WM_NCMBUTTONUP, Win32ApiConstants.WM_NCMBUTTONDBLCLK };


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Handles painting of the Non Client Area
            if (m.Msg == Win32ApiConstants.WM_NCPAINT || m.Msg == Win32ApiConstants.WM_IME_NOTIFY || m.Msg == Win32ApiConstants.WM_SIZE || m.Msg == Win32ApiConstants.WM_NCACTIVATE)
            {
                this.NcPaintMsgHandler(ref m);
            }
            else if (m.Msg == Win32ApiConstants.WM_NCCALCSIZE)
            {
                this.NcCalcSizeMsgHandler(ref m);
            }
            else if (NcMouseWindowsMessages.Contains(m.Msg))
            {
                this.NcMouseMsgHandler(ref m);
            }
        }


        private void NcCalcSizeMsgHandler(ref Message m)
        {
            if (m.WParam != IntPtr.Zero)
            {
                NCCALCSIZE_PARAMS rcsize = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                AdjustClientRect(ref rcsize.rcNewWindow);
                Marshal.StructureToPtr(rcsize, m.LParam, false);

                this.RectNewWindow = rcsize.rcNewWindow.ToRectangle();
                this.RectOldWindow = rcsize.rcOldWindow.ToRectangle();
                this.RectClient = rcsize.rcClient.ToRectangle();

                this.CheckNCArea(new Padding(rcsize.rcClient.Left - rcsize.rcOldWindow.Left,
                                                rcsize.rcClient.Top - rcsize.rcOldWindow.Top,
                                                rcsize.rcOldWindow.Right - rcsize.rcClient.Right,
                                                rcsize.rcOldWindow.Bottom - rcsize.rcClient.Bottom));

            }
            else
            {
                RECT rcsize = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
                AdjustClientRect(ref rcsize);
                Marshal.StructureToPtr(rcsize, m.LParam, false);
                this.RectClient2 = rcsize.ToRectangle();

            }
            m.Result = new IntPtr(1);
            return;
        }

        private void NcPaintMsgHandler(ref Message m)
        {
            // To avoid unnecessary graphics recreation and thus improving performance
            if (DesignMode || ncGrapichs == null || m.Msg == Win32ApiConstants.WM_SIZE)
            {
                Win32ApiFunction.ReleaseDC(this.Handle, wndHdc);
                wndHdc = Win32ApiFunction.GetWindowDC(this.Handle);
                ncGrapichs = Graphics.FromHdc(wndHdc);

                if (!DesignMode)
                {
                    Rectangle clientRecToScreen = new Rectangle(this.PointToScreen(new Point(this.ClientRectangle.X, this.ClientRectangle.Y)), new Size(this.ClientRectangle.Width, this.ClientRectangle.Height));
                    Rectangle clientRectangle = new Rectangle(clientRecToScreen.X - this.Location.X, clientRecToScreen.Y - this.Location.Y, clientRecToScreen.Width, clientRecToScreen.Height);

                    ncGrapichs.ExcludeClip(clientRectangle);
                }
            }

            RectangleF recF = ncGrapichs.VisibleClipBounds;

            if (!this.Disposing)
            {
                PaintEventArgs ncPaintEventArgs = new PaintEventArgs(ncGrapichs, new Rectangle((int)recF.X, (int)recF.Y, (int)recF.Width, (int)recF.Height));
                OnNcPaint(ncPaintEventArgs);
            }
            this.Refresh();
        }

        private void NcMouseMsgHandler(ref Message m)
        {
            Point ptScreen = Cursor.Position;
            Point ptClient = PointToClient(ptScreen);

            int x = ptClient.X + this.NonClientArea.Left;
            int y = ptClient.Y + this.NonClientArea.Top;
            MouseEventArgs e = new MouseEventArgs(Control.MouseButtons, 0, x, y, 0);

            switch (m.Msg)
            {
                case Win32ApiConstants.WM_NCMOUSEMOVE:
                    this.OnNcMouseMove(e);
                    break;

                case Win32ApiConstants.WM_NCMOUSELEAVE:
                    this.OnNcMouseLeave(e);
                    break;

                case Win32ApiConstants.WM_NCLBUTTONDOWN:
                case Win32ApiConstants.WM_NCRBUTTONDOWN:
                case Win32ApiConstants.WM_NCMBUTTONDOWN:
                    this.OnNcMouseDown(e);
                    break;

                case Win32ApiConstants.WM_NCLBUTTONUP:
                case Win32ApiConstants.WM_NCRBUTTONUP:
                case Win32ApiConstants.WM_NCMBUTTONUP:
                    this.OnNcMouseUp(e);
                    break;
            }
        }

     

        private void AdjustClientRect(ref RECT rcClient)
        {
            rcClient.Left += this._nonClientAreaAdjust.Left;
            rcClient.Top += this._nonClientAreaAdjust.Top;
            rcClient.Right -= this._nonClientAreaAdjust.Right;
            rcClient.Bottom -= this._nonClientAreaAdjust.Bottom;
        }

        private void CheckNCArea(Padding newNcArea)
        {
            bool equals = newNcArea.Left == this.NonClientArea.Left
                            && newNcArea.Top == this.NonClientArea.Top
                            && newNcArea.Right == this.NonClientArea.Right
                            && newNcArea.Bottom == this.NonClientArea.Bottom;

            this.NonClientArea = new Padding(newNcArea.Left, newNcArea.Top, newNcArea.Right, newNcArea.Bottom);

            if (!equals)
            {
                this.OnNcAreaChanged(EventArgs.Empty);
            }
        }

        protected virtual void OnNcAreaChanged(EventArgs e)
        {
            NcAreaChanged?.Invoke(this, e);
        }

        protected virtual void OnNcPaint(PaintEventArgs e)
        {
            NcPaint?.Invoke(this, e);
        }

        protected virtual void OnNcMouseMove(MouseEventArgs e)
        {
            NcMouseMove?.Invoke(this, e);
        }

        protected virtual void OnNcMouseLeave(MouseEventArgs e)
        {
            NcMouseLeave?.Invoke(this, e);
        }

        protected virtual void OnNcMouseDown(MouseEventArgs e)
        {
            NcMouseDown?.Invoke(this, e);
        }

        protected virtual void OnNcMouseUp(MouseEventArgs e)
        {
            NcMouseUp?.Invoke(this, e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            NcPaint = null;
            Win32ApiFunction.ReleaseDC(this.Handle, wndHdc);
        }
    }
}


