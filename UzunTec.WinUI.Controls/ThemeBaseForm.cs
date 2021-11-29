using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeBaseForm : Form
    {
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public string TextTitle { get => this.lblTitle.Text; set => this.lblTitle.Text = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorDark { get => this.panelHeader.BackColor; set { this.panelHeader.BackColor = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorLight { get => this.panelTitle.BackColor; set { this.panelTitle.BackColor = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(int), "47")]
        public int HeaderPanelHeight { get => this.panelHeader.Height; set { this.panelHeader.Height = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(Padding), "3; 3; 3; 3;")]
        public new Padding Padding { get => this._internalPadding; set { this._internalPadding = value; this.Invalidate(); } }
        private Padding _internalPadding;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HeaderTextColor { get => this.lblTitle.ForeColor; set { this.lblTitle.ForeColor = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public new Font Font { get => this.lblTitle.Font; set { this.lblTitle.Font = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(Color), "True")]
        public bool ShowClose { get => btnCloseIcon.Visible; set => btnCloseIcon.Visible = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "True")]
        public bool ShowMaximize { get => btnMaximizeIcon.Visible; set => btnMaximizeIcon.Visible = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "True")]
        public bool ShowMinimize { get => btnMinimizeIcon.Visible; set => btnMinimizeIcon.Visible = value; }

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        public ThemeBaseForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(41)))), ((int)(((byte)(53)))));

            // Theme
            this.Font = this.ThemeScheme.FormTitleBarFont;
            this.panelTitle.BackColor = this.ThemeScheme.ControlTextLightColor;
            this.HeaderTextColor = this.ThemeScheme.ControlTextLightColor;
            this.panelTitle.BackColor = this.ThemeScheme.PrimaryColor;
            this.panelHeader.BackColor = this.ThemeScheme.SecondaryColor;
            this.BackColor = this.ThemeScheme.FormBackgroundColor;


            this.btnMinimizeIcon.Font = ThemeSchemeManager.Instance.GetFont("Segoe MDL2", 12);
            this.btnMaximizeIcon.Font = ThemeSchemeManager.Instance.GetFont("Segoe MDL2", 12);
            this.btnCloseIcon.Font = ThemeSchemeManager.Instance.GetFont("Segoe MDL2", 12);
            this.btnMinimizeIcon.Text = "\u2717";
            this.btnCloseIcon.Text = "\u2717";
            this.btnMaximizeIcon.Text = "\u2584";
            //this.btnMaximizeIcon.Text = "\u2718";
            //this.btnCloseIcon.Text = "\uEF2C";
        }

        protected override void OnCreateControl()
        {
            base.Padding = new Padding(0);
            base.OnCreateControl();
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        public extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        public extern static void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        public const int CB_SETCUEBANNER = 0x1703;
        public const int EM_SETCUEBANNER = 0x1501;
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)] string lParam);

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void panelMovable_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void btnMaximize_Click(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panelTitle_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (WindowState == FormWindowState.Normal)
            {
                this.WindowState = FormWindowState.Maximized;
            }
            else
            {
                this.WindowState = FormWindowState.Normal;
            }
        }
    }
}
