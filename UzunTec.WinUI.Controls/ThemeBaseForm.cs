using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeBaseForm : FormWithNc
    {
        private const int HEADER_HEIGHT = 25;
        private const int BUTTON_WIDTH = 25;

        #region Theme
        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color BorderColorDark { get => _borderColorDark; set { _borderColorDark = value; this.Invalidate(); } }
        private Color _borderColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color BorderColorLight { get => _borderColorLight; set { _borderColorLight = value; this.Invalidate(); } }
        private Color _borderColorLight;

        [Category("Theme"), DefaultValue(typeof(int), "47")]
        public int BorderWidth { get => _borderWidth; set { _borderWidth = value; this.UpdateNonClientArea(); } }
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
        public int HeaderPanelHeight { get => _headerPanelHeight; set { _headerPanelHeight = value; this.UpdateNonClientArea(); } }
        private int _headerPanelHeight;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font TitleTextFont { get => _titleTextFont; set { _titleTextFont = value; this.Invalidate(); } }
        private Font _titleTextFont;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TitleTextColor { get => _titleTextColor; set { _titleTextColor = value; this.Invalidate(); } }
        private Color _titleTextColor;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color ControlButtonHoverColor { get => _controlButtonHoverColor; set { _controlButtonHoverColor = value; this.Invalidate(); } }
        private Color _controlButtonHoverColor;
        #endregion

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Control")]
        public string TextTitle { get => this._textTitle; set { this._textTitle = value; this.Invalidate(); } }
        private string _textTitle;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Control")]
        public string TextHeader { get => this._textHeader; set { this._textHeader = value; this.Invalidate(); } }
        private string _textHeader;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Control")]
        public new Padding Padding { get => this._padding; set { this._padding = value; this.SetBasePadding(value); this.Invalidate(); } }

        private void SetBasePadding(Padding value)
        {
            base.Padding = value.AddPadding(new Padding(0, (int)this.HeaderPanelHeight, 0, 0));
        }

        private Padding _padding;


        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowTitlePanel { get => showTitlePanel; set { showTitlePanel = value; this.UpdateRects(); this.Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowClose { get => showClose; set { showClose = value; this.UpdateRects(); this.Invalidate(); } }
        private string close;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMaximize { get => showMaximize; set { showMaximize = value; this.UpdateRects(); this.Invalidate(); } }
        private string maximize;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMinimize { get => showMinimize; set { showMinimize = value; this.UpdateRects(); this.Invalidate(); } }
        private string minimize;

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image LogoImage { get => this.logoImageData.image; set { this.logoImageData.image = value; this.UpdateRects(); this.Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "BottomLeft")]
        public ContentAlignment TitleTextAlign { get => this._titleTextAlign; set { this._titleTextAlign = value; this.Invalidate(); } }
        private ContentAlignment _titleTextAlign;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment HeaderTextAlign { get => this._headerTextAlign; set { this._headerTextAlign = value; this.Invalidate(); } }
        private ContentAlignment _headerTextAlign;

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        private RectangleF textHeaderRect, textTitleRect, titleRect, headerRect, closeRect, maximizeRect, minimizeRect, logoTitleRect;
        private bool showTitlePanel, showClose, showMaximize, showMinimize, hasHeaderText, hasTitle;
        private readonly SideIconData logoImageData = new SideIconData();
        private readonly Dictionary<string, SideIconData> icons;
        public EventHandler<string> buttonClick;

        public ThemeBaseForm()
        {
            this.ControlBox = false;
            //this.MaximizedBounds = Screen.FromHandle(this.Handle).WorkingArea;
            //base.Padding = new Padding(5);
            _borderWidth = 1;
            _headerPanelHeight = 60;
            //showClose = true;
            //showMinimize = true;
            //showMaximize = true;
            //showTitlePanel = true;
            this._titleTextAlign = ContentAlignment.MiddleLeft;
            this._headerTextAlign = ContentAlignment.MiddleLeft;

            this.icons = new Dictionary<string, SideIconData>
            {
                { "close", new SideIconData{ text = "\u2716"} },
                { "maximize", new SideIconData{ text = "\u2B12"} },
                { "minimize", new SideIconData{ text = "\u2584"} },
            };

            this.AdjustNonClientArea(new Padding(-4, -6, -4, -4));

            this.SetBasePadding(this._padding);

            InitializeComponent();

            this.UpdateStylesFromTheme();
            this.UpdateRects();
        }

        protected override void OnCreateControl()
        {
            //base.Padding = new Padding(0);
            base.OnCreateControl();
            //this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            SizeChanged += (s,e) => this.UpdateRects();

            this.UpdateStylesFromTheme();
            this.UpdateRects();
        }

        private void UpdateStylesFromTheme()
        {
            // Theme
            this.HeaderColorDark = this.ThemeScheme.FormHeaderColorDark;
            this.HeaderColorLight = this.ThemeScheme.FormHeaderColorLight;
            this.HeaderTextColor = this.ThemeScheme.FormHeaderTextColor;
            this.HeaderTextFont = this.ThemeScheme.FormHeaderTextFont;

            this.TitlePanelColorDark = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
            this.TitlePanelColorLight = this.ThemeScheme.FormTitlePanelBackgroundColorLight;
            this.TitleTextColor = this.ThemeScheme.FormTitleTextColor;
            this.TitleTextFont = this.ThemeScheme.FormTitleFont;

            this.BackColor = this.ThemeScheme.FormBackgroundColor;
            //this.btnMaximizeIcon.Text = "\u2718";
            //this.btnCloseIcon.Text = "\uEF2C";
        }


        protected override void OnNcAreaChanged(EventArgs e)
        {
            this.UpdateNonClientArea();
        }
        protected void UpdateNonClientArea()
        {
            int ncHeight = HEADER_HEIGHT;
            int heightAdjust = ncHeight - this.NonClientArea.Top;

            int borderLeftAdjust = this._borderWidth - this.NonClientArea.Left;
            int borderRightAdjust = this._borderWidth - this.NonClientArea.Right;
            int borderBottomAdjust = this._borderWidth - this.NonClientArea.Bottom;

            // this.AdjustNonClientArea(new Padding(borderLeftAdjust, heightAdjust, borderRightAdjust, borderBottomAdjust));
            //this.UpdateRects();
            //this.Invalidate();
        }


        private void UpdateRects()
        {
            hasHeaderText = !string.IsNullOrEmpty(TextHeader);
            hasTitle = !string.IsNullOrEmpty(TextTitle);

            float buttonOffset = 0;

            this.headerRect = new RectangleF(0, 0, this.ClientRectangle.Width + (BorderWidth * 2), HEADER_HEIGHT);

            this.textHeaderRect = new RectangleF(0 + (BorderWidth * 2), 0, (this.ClientRectangle.Width + (BorderWidth * 2))/2, HEADER_HEIGHT);

            if (this.showTitlePanel)
            {
                this.titleRect = new RectangleF(0, 0, this.ClientRectangle.Width + (BorderWidth * 2), HeaderPanelHeight);

                if (this.logoImageData.image != null)
                {
                    float logoRectWidth = logoImageData.image.Width;
                    this.logoTitleRect = new RectangleF(this.ClientRectangle.Width - (BorderWidth * 2) - logoRectWidth, 0, logoRectWidth, HeaderPanelHeight);
                }

                this.textTitleRect = new RectangleF(0 + (BorderWidth * 2), 0, (this.ClientRectangle.Width + (BorderWidth * 2)) / 2, HeaderPanelHeight);
            }

            if (this.showClose)
            {
                this.icons["close"].rect = new RectangleF(this.ClientRectangle.Width - BUTTON_WIDTH - buttonOffset, 0, BUTTON_WIDTH + (BorderWidth * 2), HEADER_HEIGHT);
                buttonOffset += this.icons["close"].rect.Width;
            }

            if (this.showMaximize)
            {
                this.icons["maximize"].rect = new RectangleF(this.ClientRectangle.Width - BUTTON_WIDTH - buttonOffset, 0, BUTTON_WIDTH + (BorderWidth * 2), HEADER_HEIGHT);
                buttonOffset += this.icons["maximize"].rect.Width;
            }

            if (this.showMinimize)
            {
                this.icons["minimize"].rect = new RectangleF(this.ClientRectangle.Width - BUTTON_WIDTH - buttonOffset, 0, BUTTON_WIDTH + (BorderWidth * 2), HEADER_HEIGHT);
                buttonOffset += this.icons["minimize"].rect.Width;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (titleRect.Width > 0f && this.showTitlePanel)
            {
                Brush titleBrush = new LinearGradientBrush(titleRect, _titlePanelColorDark, _titlePanelColorLight, LinearGradientMode.ForwardDiagonal);
                g.FillRectangle(titleBrush, titleRect);
            }

            if (this.logoImageData.image != null)
            {
                g.DrawImage(logoImageData.image, logoTitleRect);
                //g.FillRectangle(Brushes.DarkMagenta, logoTitleRect);
            }

            Brush titleTextBrush = new SolidBrush(this.TitleTextColor);
            if (!string.IsNullOrWhiteSpace(this.TextTitle) && this.showTitlePanel)
            {
                g.Clip = new Region(this.textTitleRect);
                g.DrawText(this.TextTitle, this.TitleTextFont, titleTextBrush, this.textTitleRect, this.TitleTextAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.Firebrick, textTitleRect);
            }
        }

        protected override void OnNcPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Brush borderBrush = new LinearGradientBrush(ClientRectangle, _borderColorDark, _borderColorLight, LinearGradientMode.ForwardDiagonal);

            //g.Clear(TitlePanelColorDark);

            if (headerRect.Width > 0f)
            {
                Brush headerBrush = new LinearGradientBrush(headerRect, _headerColorDark, _headerColorLight, LinearGradientMode.ForwardDiagonal);
                g.FillRectangle(headerBrush, headerRect);
            }
            
            Brush headerTextBrush = new SolidBrush(this.HeaderTextColor);
            if (!string.IsNullOrWhiteSpace(this.TextHeader))
            {
                g.Clip = new Region(this.textHeaderRect);
                g.DrawText(this.TextHeader, this.HeaderTextFont, headerTextBrush, this.textHeaderRect, this.HeaderTextAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.LightGoldenrodYellow, textHeaderRect);
            }

            if (this.showClose)
            {
                g.Clip = new Region(this.icons["close"].rect);
                g.DrawText(this.icons["close"].text, ThemeSchemeManager.Instance.GetFont("Segoe UI", 15), headerTextBrush, this.closeRect);
                g.ResetClip();
                //g.FillRectangle(Brushes.DarkOrange, closeRect);
            }

            if (this.showMaximize)
            {
                g.Clip = new Region(this.icons["maximize"].rect);
                g.DrawText(this.icons["maximize"].text, ThemeSchemeManager.Instance.GetFont("Segoe UI", 15), headerTextBrush, this.maximizeRect);
                g.ResetClip();
                //g.FillRectangle(Brushes.DimGray, maximizeRect);
            }

            if (this.showMinimize)
            {
                g.Clip = new Region(this.icons["minimize"].rect);
                g.DrawText(this.icons["minimize"].text, ThemeSchemeManager.Instance.GetFont("Segoe UI", 15), headerTextBrush, this.minimizeRect);
                g.ResetClip();
                //g.FillRectangle(Brushes.LawnGreen, minimizeRect);
            }

            foreach (SideIconData iconData in icons.Values)
            {
                if (iconData.hovered)
                {
                    g.FillRectangle(Brushes.White, iconData.rect);
                }
                g.DrawText(iconData.text, ThemeSchemeManager.Instance.GetFont("Segoe UI", 15), headerTextBrush, iconData.rect);
            }

        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (headerRect.Contains(e.Location))
            {
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.hovered = iconData.rect.Contains(e.Location);
                }
            }
            else
            {
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.hovered = false;
                }
            }
            Invalidate();
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (headerRect.Contains(e.Location))
            {
                foreach (string key in icons.Keys)
                {
                    SideIconData iconData = icons[key];
                    if (iconData.rect.Contains(e.Location))
                    {
                        buttonClick?.Invoke(this, key);
                    }
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
