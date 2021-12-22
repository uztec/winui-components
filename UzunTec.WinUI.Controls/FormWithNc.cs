using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls
{
    public class FormWithNc : Form
    {
        /*
         * Constants
         */
        //Paramaters to EnableMenuItem Win32 function
        private const int SC_CLOSE = 0xF060; //The Close Box identifier
        private const int MF_ENABLED = 0x0;  //Enabled Value
        private const int MF_DISABLED = 0x2; //Disabled Value

        //Windows Messages
        private const int WM_NCPAINT = 0x85;//Paint non client area message
        private const int WM_PAINT = 0xF;//Paint client area message
        private const int WM_SIZE = 0x5;//Resize the form message
        private const int WM_IME_NOTIFY = 0x282;//Notify IME Window message
        private const int WM_SETFOCUS = 0x0007;//Form.Activate message
        private const int WM_SYSCOMMAND = 0x112; //SysCommand message
        private const int WM_SIZING = 0x214; //Resize Message
        private const int WM_NCLBUTTONDOWN = 0xA1; //Left Mouse Button on Non-Client Area is Down
        private const int WM_NCACTIVATE = 0x86; //Message sent to the window when it's activated or deactivated
        private const int WM_NCCALCSIZE = 0x0083;

        //WM_SIZING WParams that stands for Hit Tests in the direction the form is resizing
        private const int HHT_ONHEADER = 0x0002;
        private const int HT_TOPLEFT = 0XD;
        private const int HT_TOP = 0XC;
        private const int HT_TOPRIGHT = 0XE;
        private const int HT_RIGHT = 0XB;
        private const int HT_BOTTOMRIGHT = 0X11;
        private const int HT_BOTTOM = 0XF;
        private const int HT_BOTTOMLEFT = 0X10;
        private const int HT_LEFT = 0XA;

        //WM_SYSCOMMAND WParams that stands for which operation is beeing done
        private const int SC_DRAGMOVE = 0xF012; //SysCommand Dragmove parameter
        private const int SC_MOVE = 0xF010; //SysCommand Move with keyboard command

        private Graphics ncGrapichs = null;
        private IntPtr wndHdc;

        struct RECT { public int Left, Top, Right, Bottom; }
        struct NCCALCSIZE_PARAMS
        {
            public RECT rcNewWindow;
            public RECT rcOldWindow;
            public RECT rcClient;
            IntPtr lppos;
        }
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public const int CB_SETCUEBANNER = 0x1703;
        public const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        /// <summary>
        /// Occurs when the frame area (including Title Bar, excluding the client area) is redrawn."
        /// </summary>
        [Description("Occurs when The frame area (including Title Bar, excluding the client area) needs repainting."), Category("Appearance")]
        public event PaintEventHandler NcPaint;


        //Get Desktop Window Handle
        [DllImport("user32.dll")]
        private static extern IntPtr GetDesktopWindow();


        //Find any Window in the OS. We will look for the parent of where the desktop is (ProgMan)
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        //Get the device component of the window to allow drawing on the title bar and frame
        [DllImport("User32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        //Releases the Device Component after it's been used
        [DllImport("User32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);


        public Padding NonClientArea { get => this._nonClientArea; set { this._nonClientArea = value; this.InvalidateAll(); } }
        private Padding _nonClientArea;

        public FormWithNc()
        {
            // this.ControlBox = false;
            this.SizeChanged += (s, e) => InvalidateAll();
        }

        private void InvalidateAll()
        {
            this.Invalidate();
            SendMessage(this.Handle, WM_NCPAINT, 1, 0);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

        }


        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);

            // Handles painting of the Non Client Area
            if (m.Msg == WM_NCPAINT || m.Msg == WM_IME_NOTIFY || m.Msg == WM_SIZE || m.Msg == WM_NCACTIVATE)
            {
                // To avoid unnecessary graphics recreation and thus improving performance
                if (DesignMode || ncGrapichs == null || m.Msg == WM_SIZE)
                {
                    ReleaseDC(this.Handle, wndHdc);
                    wndHdc = GetWindowDC(this.Handle);
                    ncGrapichs = Graphics.FromHdc(wndHdc);

                    if (!DesignMode)
                    {
                        Rectangle clientRecToScreen = new Rectangle(this.PointToScreen(new Point(this.ClientRectangle.X, this.ClientRectangle.Y)), new Size(this.ClientRectangle.Width, this.ClientRectangle.Height));
                        Rectangle clientRectangle = new Rectangle(clientRecToScreen.X - this.Location.X, clientRecToScreen.Y - this.Location.Y, clientRecToScreen.Width, clientRecToScreen.Height);

                        ncGrapichs.ExcludeClip(clientRectangle);
                    }
                }

                RectangleF recF = ncGrapichs.VisibleClipBounds;

                PaintEventArgs ncPaintEventArgs = new PaintEventArgs(ncGrapichs, new Rectangle((int)recF.X, (int)recF.Y, (int)recF.Width, (int)recF.Height));
                OnNcPaint(ncPaintEventArgs);
                this.Refresh();

            }
            else if (m.Msg == WM_NCCALCSIZE)
            {
                if (m.WParam != IntPtr.Zero)
                {
                    NCCALCSIZE_PARAMS rcsize = (NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(NCCALCSIZE_PARAMS));
                    AdjustClientRect(ref rcsize.rcNewWindow);
                    Marshal.StructureToPtr(rcsize, m.LParam, false);
                }
                else
                {
                    RECT rcsize = (RECT)Marshal.PtrToStructure(m.LParam, typeof(RECT));
                    AdjustClientRect(ref rcsize);
                    Marshal.StructureToPtr(rcsize, m.LParam, false);
                }
                m.Result = new IntPtr(1);
                return;
            }
        }


        private void AdjustClientRect(ref RECT rcClient)
        {
            rcClient.Left += this._nonClientArea.Left;
            rcClient.Top += this._nonClientArea.Top;
            rcClient.Right -= this._nonClientArea.Right;
            rcClient.Bottom -= this._nonClientArea.Bottom;
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void OnNcPaint(PaintEventArgs e)
        {
            NcPaint?.Invoke(this, e);
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            NcPaint = null;
            ReleaseDC(this.Handle, wndHdc);
        }
    }
}


