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
    public partial class ThemeBaseForm : FormWithNc
    {
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public string TextTitle { get => this._textTitle; set { this._textTitle = value; this.Invalidate(); } }
        private string _textTitle;

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public string TextHeader { get => this._textHeader; set { this._textHeader = value; this.Invalidate(); } }
        private string _textHeader;

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
        public Color HeaderTextColor { get => _headerTextColor; set { _headerTextColor = value; this.Invalidate(); } }
        private Color _headerTextColor;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font HeaderTextFont { get => _headerTextFont; set { _headerTextFont = value; this.Invalidate(); } }
        private Font _headerTextFont;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorDark { get => _headerColorDark; set { _headerColorDark = value; this.Invalidate(); } }
        private Color _headerColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorLight { get => _headerColorLight; set { _headerColorLight = value; this.Invalidate(); } }
        private Color _headerColorLight;

        public Color TitlePanelColorDark { get => _titlePanelColorDark; set { _titlePanelColorDark = value; this.Invalidate(); } }
        private Color _titlePanelColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color TitlePanelColorLight { get => _titlePanelColorLight; set { _titlePanelColorLight = value; this.Invalidate(); } }
        private Color _titlePanelColorLight;

        [Category("Theme"), DefaultValue(typeof(int), "47")]
        public int HeaderPanelHeight { get => _headerPanelHeight; set { _headerPanelHeight = value; this.Invalidate(); } }
        private int _headerPanelHeight;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font TitleTextFont { get => _titleTextFont; set { _titleTextFont = value; this.Invalidate(); } }
        private Font _titleTextFont;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TitleTextColor { get => _titleTextColor; set { _titleTextColor = value; this.Invalidate(); } }
        private Color _titleTextColor;

        //[Category("Z-Custom"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        //public new Padding Padding { get => _internalPadding; set { SetPadding(value); Invalidate(); } }
        //private Padding _internalPadding;

        private RectangleF textHeaderRect, textTitleRect, titleRect, headerRect;
        private bool showHeader, showClose, showMaximize, showMinimize, hasHeaderText, hasTitle;

        //private void SetPadding(Padding value)
        //{
        //    _internalPadding = value;
        //    //base.Padding = Padding.Add(value, new Padding(_borderWidth));
        //    //base.Padding = new Padding(base.Padding.Left, 0, base.Padding.Right, base.Padding.Bottom);
        //}

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color ControlButtonHoverColor { get => _controlButtonHoverColor; set { _controlButtonHoverColor = value; this.Invalidate(); } }
        private Color _controlButtonHoverColor;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowClose { get => showClose; set { showClose = value; this.Invalidate(); } }
        private string close;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMaximize { get => showMaximize; set { showMaximize = value; this.Invalidate(); } }
        private string maximize;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMinimize { get => showMinimize; set { showMinimize = value; this.Invalidate(); } }
        private string minimize;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowHeader { get => showHeader; set { showHeader = value; this.Invalidate(); } }

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        public ThemeBaseForm()
        {
            InitializeComponent();
            this.ControlBox = false;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //base.Padding = new Padding(5);
            BorderWidth = 1;
            HeaderPanelHeight = 60;

            this.UpdateStylesFromTheme();
            this.UpdateRects();
        }

        protected override void OnCreateControl()
        {
            //base.Padding = new Padding(0);
            base.OnCreateControl();

            this.UpdateStylesFromTheme();
            this.UpdateRects();
        }

        private void ThemeBaseForm_SizeChanged(object sender, EventArgs e)
        {
            this.UpdateRects();
        }

        private void UpdateStylesFromTheme()
        {
            // Theme
            this.HeaderColorDark = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
            this.HeaderColorLight = this.ThemeScheme.FormTitlePanelBackgroundColorLight;
            this.HeaderTextColor = this.ThemeScheme.FormTitleTextColor;
            this.HeaderTextFont = this.ThemeScheme.FormTitleFont;

            this.TitlePanelColorDark = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
            this.TitlePanelColorLight = this.ThemeScheme.FormTitlePanelBackgroundColorLight;
            this.TitleTextColor = this.ThemeScheme.FormTitleTextColor;
            this.TitleTextFont = this.ThemeScheme.FormTitleFont;

            this.BackColor = this.ThemeScheme.FormBackgroundColor;

            this.minimize = "\u2584";
            this.close = "\u2716";
            this.maximize = "\u2B12";
            //this.btnMaximizeIcon.Text = "\u2718";
            //this.btnCloseIcon.Text = "\uEF2C";
        }

        private void UpdateRects()
        {
            hasHeaderText = !string.IsNullOrEmpty(TextHeader);
            hasTitle = !string.IsNullOrEmpty(TextTitle);

            const float HEADER_HEIGHT = 25;

            this.headerRect = new RectangleF(0, 0, this.ClientRectangle.Width + (BorderWidth * 2), HEADER_HEIGHT);

            this.titleRect = new RectangleF(0, headerRect.Height, this.ClientRectangle.Width + (BorderWidth * 2), HeaderPanelHeight);

            float ncHeaderAdjust = this.headerRect.Height + this.titleRect.Height - this.NcHeight;

            this.NonClientAreaAdjust = new Padding(BorderWidth-8, (int)ncHeaderAdjust, BorderWidth-8, BorderWidth-8);

            this.Invalidate();
        }

        protected override void OnNcPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Brush borderBrush = new LinearGradientBrush(ClientRectangle, _borderColorDark, _borderColorLight, LinearGradientMode.ForwardDiagonal);

            g.Clear(Color.DarkCyan);

            //g.FillRectangle(Brushes.DarkRed, new Rectangle(5, 0, 5, this.NcHeight - 5));

            g.FillRectangle(Brushes.BurlyWood, headerRect);

            g.FillRectangle(Brushes.Blue, titleRect);
        }

        //protected override void OnPaint(PaintEventArgs e)
        //{
        //    base.OnPaint(e);

        //    if (_borderWidth > 0)
        //    {
        //        Graphics g = e.Graphics;
        //        Brush borderBrush = new LinearGradientBrush(ClientRectangle, _borderColorDark, _borderColorLight, LinearGradientMode.ForwardDiagonal);

        //        Region borderRegion = new Region(this.ClientRectangle);
        //        borderRegion.Exclude(this.ClientRectangle.ToRectF().ApplyPadding(new Padding(this.BorderWidth)));
        //        g.FillRegion(borderBrush, borderRegion);
        //    }
        //}

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
