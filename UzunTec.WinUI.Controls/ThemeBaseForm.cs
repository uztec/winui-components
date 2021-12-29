using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
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
        public int HeaderPanelHeight { get => _headerPanelHeight; set { _headerPanelHeight = value; this.SetBasePadding(this._padding);  this.UpdateNonClientArea(); } }
        private int _headerPanelHeight;


        [Category("Theme"), DefaultValue(typeof(Font), "Segeo UI Light, 12")]
        public new Font Font { get => _textFont; set { _textFont = value; this.UpdateNcRects(); this.Invalidate(); } }
        private Font _textFont;


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
        public string Title { get => this._titleText; set { this._titleText = value; this.Invalidate(); } }
        private string _titleText;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowTitlePanel { get => _showTitlePanel; set { _showTitlePanel = value; this.UpdateRects(); this.Invalidate(); } }
        private bool _showTitlePanel;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowClose { get => _showClose; set { _showClose = value; this.UpdateRects(); this.Invalidate(); } }
        private bool _showClose;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMaximize { get => _showMaximize; set { _showMaximize = value; this.UpdateRects(); this.Invalidate(); } }
        private bool _showMaximize;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMinimize { get => _showMinimize; set { _showMinimize = value; this.UpdateRects(); this.Invalidate(); } }
        private bool _showMinimize;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "BottomLeft")]
        public ContentAlignment TitleTextAlign { get => this._titleTextAlign; set { this._titleTextAlign = value; this.Invalidate(); } }
        private ContentAlignment _titleTextAlign;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment HeaderTextAlign { get => this._headerTextAlign; set { this._headerTextAlign = value; this.UpdateNcRects(); this.InvalidateNc(); } }
        private ContentAlignment _headerTextAlign;

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image LogoImage { get => this.logoImageData.image; set { this.logoImageData.image = value; this.UpdateRects(); this.Invalidate(); } }


        [Category("Z-Custom"), DefaultValue(typeof(Color), "Control")]
        public new Padding Padding { get => this._padding; set { this._padding = value; this.SetBasePadding(value); this.Invalidate(); } }

        private void SetBasePadding(Padding value)
        {
            base.Padding = value.AddPadding(new Padding(0, (int)this._headerPanelHeight, 0, 0));
        }
        private Padding _padding;

        private RectangleF butttonsRect, textTitleRect, titleRect, textHeaderRect, headerRect, logoTitleRect;
        private bool hasHeaderText, hasTitle;
        private readonly SideIconData logoImageData = new SideIconData();
        private readonly Dictionary<string, SideIconData> icons;


        public ThemeBaseForm()
        {
            this.ControlBox = false;
            this._borderWidth = 10;
            this._headerPanelHeight = 60;
            this._showClose = true;
            this._showMinimize = true;
            this._showMaximize = true;
            this._showTitlePanel = true;

            this._titleTextAlign = ContentAlignment.MiddleLeft;
            this._headerTextAlign = ContentAlignment.MiddleLeft;

            this.icons = new Dictionary<string, SideIconData>
            {
                { "close", new SideIconData{ text = "\u2716"} },
                { "maximize", new SideIconData{ text = "\u2B12"} },
                { "minimize", new SideIconData{ text = "\u2584"} },
            };

            this.UpdateStylesFromTheme();
            this.UpdateRects();

            this.SetBasePadding(this._padding);
            this.AdjustNonClientArea(new Padding(-4, -6, -4, -4));
            InitializeComponent();
        }

        protected override void OnCreateControl()
        {
            //base.Padding = new Padding(0);
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            SizeChanged += (s, e) => this.UpdateRects();

            this.UpdateStylesFromTheme();
            this.UpdateRects();
        }

        private void UpdateStylesFromTheme()
        {
            // Theme
            this._headerColorDark = this.ThemeScheme.FormHeaderColorDark;
            this._headerColorLight = this.ThemeScheme.FormHeaderColorLight;
            this._headerTextColor = this.ThemeScheme.FormHeaderTextColor;
            this._textFont = this.ThemeScheme.FormHeaderTextFont;

            this._titlePanelColorDark = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
            this._titlePanelColorLight = this.ThemeScheme.FormTitlePanelBackgroundColorLight;
            this._titleTextColor = this.ThemeScheme.FormTitleTextColor;
            this._titleTextFont = this.ThemeScheme.FormTitleFont;

            this.BackColor = this.ThemeScheme.FormBackgroundColor;
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
            this.UpdateRects();
            this.Invalidate();
        }


        private void UpdateRects()
        {
            UpdateNcRects();
            UpdateClientRects();
        }

        private void UpdateNcRects()
        {
            hasHeaderText = !string.IsNullOrEmpty(this.Text);

            float buttonOffset = 0;

            this.headerRect = new RectangleF(0, 0, this.Width + (BorderWidth * 2), HEADER_HEIGHT);

            if (this._showClose)
            {
                this.icons["close"].rect = new RectangleF(this.Width - BUTTON_WIDTH - buttonOffset, 0, BUTTON_WIDTH + (BorderWidth * 2), HEADER_HEIGHT);
                buttonOffset += this.icons["close"].rect.Width;
            }

            if (this._showMaximize)
            {
                this.icons["maximize"].rect = new RectangleF(this.Width - BUTTON_WIDTH - buttonOffset, 0, BUTTON_WIDTH + (BorderWidth * 2), HEADER_HEIGHT);
                buttonOffset += this.icons["maximize"].rect.Width;
            }

            if (this._showMinimize)
            {
                this.icons["minimize"].rect = new RectangleF(this.Width - BUTTON_WIDTH - buttonOffset, 0, BUTTON_WIDTH + (BorderWidth * 2), HEADER_HEIGHT);
                buttonOffset += this.icons["minimize"].rect.Width;
            }

            this.butttonsRect = new RectangleF(this.Width - buttonOffset, 0, buttonOffset, HEADER_HEIGHT);

            Graphics g = CreateGraphics();
            SizeF headerTextSize = g.MeasureString(this.Text, this._textFont);

            float rightOffet = ((int)(this._headerTextAlign & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != 0) ? 10 : 10 + buttonOffset;
            this.textHeaderRect = headerRect.ApplyPadding(10, 0, rightOffet, 0).ShrinkToSize(headerTextSize, this.HeaderTextAlign);

        }


        private void UpdateClientRects()
        {
            hasTitle = !string.IsNullOrEmpty(_titleText);

            if (this._showTitlePanel)
            {
                this.titleRect = new RectangleF(0, 0, this.ClientRectangle.Width + (BorderWidth * 2), HeaderPanelHeight);

                if (this.logoImageData.image != null)
                {
                    float logoRectWidth = logoImageData.image.Width;
                    this.logoTitleRect = new RectangleF(this.ClientRectangle.Width - (BorderWidth * 2) - logoRectWidth, 0, logoRectWidth, HeaderPanelHeight);
                }

                this.textTitleRect = new RectangleF(0 + (BorderWidth * 2), 0, (this.ClientRectangle.Width + (BorderWidth * 2)) / 2, HeaderPanelHeight);
            }


        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            if (titleRect.Width > 0f && this._showTitlePanel)
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
            if (hasTitle && this._showTitlePanel)
            {
                g.Clip = new Region(this.textTitleRect);
                g.DrawText(this.Title, this.TitleTextFont, titleTextBrush, this.textTitleRect, this.TitleTextAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.Firebrick, textTitleRect);
            }
        }

        protected override void OnNcPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            Brush borderBrush = new SolidBrush(_titlePanelColorDark);
            Pen borderPen = new Pen(borderBrush, this.BorderWidth);
            g.DrawRectangle(borderPen, e.ClipRectangle);

            Brush headerBrush = new LinearGradientBrush(headerRect, _headerColorDark, _headerColorLight, LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(headerBrush, headerRect);

            Brush headerTextBrush = new SolidBrush(this.HeaderTextColor);
            if (hasHeaderText)
            {
                g.DrawText(this.Text, this._textFont, headerTextBrush, this.textHeaderRect);
            }

            //foreach (SideIconData iconData in icons.Values)
            //{
            //    if (iconData.hovered)
            //    {
            //        g.FillRectangle(Brushes.White, iconData.rect);
            //    }
            //    g.DrawText(iconData.text, ThemeSchemeManager.Instance.GetFont("Segoe UI", 15), headerTextBrush, iconData.rect);
            //}

            Font buttonsFont = ThemeSchemeManager.Instance.GetFont("Segoe UI", 15);

            if (this._showClose)
            {
                g.DrawText(this.icons["close"].text, buttonsFont, headerTextBrush, this.icons["close"].rect);
            }

            if (this._showMaximize)
            {
                g.DrawText(this.icons["maximize"].text, buttonsFont, headerTextBrush, this.icons["maximize"].rect);
            }

            if (this._showMinimize)
            {
                g.DrawText(this.icons["minimize"].text, buttonsFont, headerTextBrush, this.icons["minimize"].rect);
            }

            //  g.FillRectangle(Brushes.DeepSkyBlue, this.butttonsRect);
        }

        protected override void OnNcMouseMove(MouseEventArgs e)
        {
            base.OnNcMouseMove(e);

            if (this.butttonsRect.Contains(e.Location))
            {
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.hovered = iconData.rect.Contains(e.Location);
                }
                //InvalidateNc();
            }
            else
            {
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.hovered = false;
                }
            }
        }

        protected override void OnNcMouseDown(MouseEventArgs e)
        {
            base.OnNcMouseDown(e);
            if (headerRect.Contains(e.Location))
            {
                foreach (string key in icons.Keys)
                {
                    SideIconData iconData = icons[key];
                    if (iconData.rect.Contains(e.Location))
                    {
                        //MessageBox.Show(key);
                        if (key == "close")
                        {
                            this.Close();
                        }
                        if (key == "maximize")
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
                        if (key == "minimize")
                        {
                            this.WindowState = FormWindowState.Minimized;
                        }
                    }
                }
            }
        }
    }
}
