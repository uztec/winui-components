using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeBaseForm : Form
    {
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public string TextTitle { get => this.lblTitle.Text; set => this.lblTitle.Text = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color BorderColorDark { get => _borderColorDark; set { _borderColorDark = value; this.Invalidate(); } }
        private Color _borderColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color BorderColorLight { get => _borderColorLight; set { _borderColorLight = value; this.Invalidate(); } }
        private Color _borderColorLight;

        [Category("Theme"), DefaultValue(typeof(int), "47")]
        public int BorderWidth { get => _borderWidth; set { _borderWidth = value; this.Invalidate(); } }
        private int _borderWidth;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HeaderTextColor { get => this.lblTitle.ForeColor; set { this.lblTitle.ForeColor = value; this.Invalidate(); } }
        private Color _headerTextColor;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font HeaderTextFont { get => _headerTextFont; set { _headerTextFont = value; this.Invalidate(); } }
        private Font _headerTextFont;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorDark { get => this.panelHeader.BackColor; set { this.panelHeader.BackColor = value; this.Invalidate(); } }
        private Color _headerColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorLight { get => this.panelTitle.BackColor; set { this.panelTitle.BackColor = value; this.Invalidate(); } }
        private Color _headerColorLight;

        [Category("Theme"), DefaultValue(typeof(int), "47")]
        public int HeaderPanelHeight { get => this.panelHeader.Height; set { this.panelHeader.Height = value; this.Invalidate(); } }
        private int _headerPanelHeight;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font TitleTextFont { get => _titleTextFont; set { _titleTextFont = value; this.Invalidate(); } }
        private Font _titleTextFont;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TitleTextColor { get => _titleTextColor; set { _titleTextColor = value; this.Invalidate(); } }
        private Color _titleTextColor;

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        public new Padding Padding { get => _internalPadding; set { SetPadding(value); Invalidate(); } }
        private Padding _internalPadding;

        private void SetPadding(Padding value)
        {
            _internalPadding = value;
            //base.Padding = Padding.Add(value, new Padding(_borderWidth));
            //base.Padding = new Padding(base.Padding.Left, 0, base.Padding.Right, base.Padding.Bottom);
        }

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color ControlButtonHoverColor { get => _controlButtonHoverColor; set { _controlButtonHoverColor = value; this.Invalidate(); } }
        private Color _controlButtonHoverColor;

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
            base.Padding = new Padding(5);
            BorderWidth = 3;

            // Theme
            this.Font = this.ThemeScheme.FormHeaderTextFont;
            this.HeaderTextColor = this.ThemeScheme.FormHeaderTextColor;
            this.panelTitle.BackColor = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
            this.panelHeader.BackColor = this.ThemeScheme.FormHeaderColorDark;
            this.BackColor = this.ThemeScheme.FormBackgroundColor;


            this.btnMinimizeIcon.Font = ThemeSchemeManager.Instance.GetFont("Segoe MDL2", 12);
            this.btnMaximizeIcon.Font = ThemeSchemeManager.Instance.GetFont("Segoe MDL2", 12);
            this.btnCloseIcon.Font = ThemeSchemeManager.Instance.GetFont("Segoe MDL2", 12);
            this.btnMinimizeIcon.Text = "\u2584";
            this.btnCloseIcon.Text = "\u2716";
            this.btnMaximizeIcon.Text = "\u2B12";
            //this.btnMaximizeIcon.Text = "\u2718";
            //this.btnCloseIcon.Text = "\uEF2C";
        }

        protected override void OnCreateControl()
        {
            base.Padding = new Padding(0);
            base.OnCreateControl();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_borderWidth > 0)
            {
                Graphics g = e.Graphics;
                Brush borderBrush = new LinearGradientBrush(ClientRectangle, _borderColorDark, _borderColorLight, LinearGradientMode.ForwardDiagonal);

                Region borderRegion = new Region(this.ClientRectangle);
                borderRegion.Exclude(this.ClientRectangle.ToRectF().ApplyPadding(new Padding(this.BorderWidth)));
                g.FillRegion(borderBrush, borderRegion);
            }
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
