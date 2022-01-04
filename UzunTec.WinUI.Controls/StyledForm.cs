using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class StyledForm : Form
    {
        private const int BUTTON_MARGIN = 8;
        private const int BUTTON_MARGIN_RIGHT = 15;
        private const int BUTTON_WIDTH = 15;
        private const int HEADER_HEIGHT = 25;
        private const int LEFT_TEXT_PADDING = 10;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "DarkGray")]
        public Color BorderColorDark { get => _borderColorDark; set { _borderColorDark = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _borderColorDark;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Blue")]
        public Color BorderColorLight { get => _borderColorLight; set { _borderColorLight = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _borderColorLight;

        [Category("Z-Custom"), DefaultValue(typeof(int), "5")]
        public float BorderWidth { get => _borderWidth; set { _borderWidth = value; this.SetBasePadding(_padding); this.UpdateRects(); this.Invalidate(); } }
        private float _borderWidth;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "White")]
        public Color HeaderTextColor { get => _headerTextColor; set { _headerTextColor = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _headerTextColor;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "DarkGray")]
        public Color HeaderColorDark { get => _headerColorDark; set { _headerColorDark = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _headerColorDark;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Blue")]
        public Color HeaderColorLight { get => _headerColorLight; set { _headerColorLight = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _headerColorLight;

        [Category("Z-Custom"), DefaultValue(typeof(Font), "Segeo UI Light, 12")]
        public new Font Font { get => _textFont; set { _textFont = value; this.UpdateRects(); this.Invalidate(); } }
        private Font _textFont;

        [Category("Z-Custom"), DefaultValue("")]
        public new string Text { get => base.Text; set { base.Text = value; this.UpdateRects(); this.Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment HeaderTextAlign { get => this._headerTextAlign; set { this._headerTextAlign = value; this.UpdateRects(); this.Invalidate(); } }
        private ContentAlignment _headerTextAlign;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowClose { get => this.icons["close"].visible; set { this.icons["close"].visible = value; this.UpdateRects(); this.Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMaximize { get => this.icons["maximize"].visible; set { this.icons["maximize"].visible = value; this.UpdateRects(); this.Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMinimize { get => this.icons["minimize"].visible; set { this.icons["minimize"].visible = value; this.UpdateRects(); this.Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "0;0;0;0")]
        public new Padding Padding { get => this._padding; set { this._padding = value; this.SetBasePadding(value); this.Invalidate(); } }
        private void SetBasePadding(Padding value)
        {
            base.Padding = value.AddPadding(new Padding((int)this._borderWidth, HEADER_HEIGHT, (int)this._borderWidth, (int)this._borderWidth));
        }
        private Padding _padding;


        private bool hasHeaderText;
        private RectangleF textHeaderRect, headerRect, borderRect, utilRect;
        private readonly Dictionary<string, SideIconData> icons;
        private Brush borderBrushLight, borderBrushDark, headerBrush, headerTextBrush;
        private Pen borderPenLight, borderPenDark;

        public StyledForm()
        {
            this._borderWidth = 5;
            this._headerTextAlign = ContentAlignment.MiddleLeft;
            this._borderColorDark = Color.DarkGray;
            this._borderColorDark = Color.DarkGray;
            this._headerColorDark = Color.DarkGray;
            this._headerColorLight = Color.Blue;
            this._headerTextColor = Color.White;
            this._textFont = new Font(FontFamily.GenericSansSerif, 10);

            this.icons = new Dictionary<string, SideIconData>
            {
                { "close", new SideIconData{ image = Properties.Resources.close_off_dark, imageHovered = Properties.Resources.close_on, visible = true } },
                { "maximize", new SideIconData{ image = Properties.Resources.maximize_off_dark, imageHovered = Properties.Resources.maximize_on, visible = true } },
                { "minimize", new SideIconData{ image = Properties.Resources.minimize_off_dark, imageHovered = Properties.Resources.minimize_on, visible = true } },
            };

            UpdateRects();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateRects();
            SizeChanged += (e, s) => { UpdateRects(); };
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32ApiConstants.WM_NCHITTEST)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = this.PointToClient(pos);

                if (!this.utilRect.Contains(pos))
                {
                    if (pos.X > this.utilRect.Right)
                    {
                        m.Result = (IntPtr)((pos.Y > this.utilRect.Bottom) ? Win32ApiConstants.HTBOTTOMRIGHT : Win32ApiConstants.HTRIGHT);
                        return;
                    }
                    else if (pos.X < this.utilRect.Left)
                    {
                        m.Result = (IntPtr)((pos.Y > this.utilRect.Bottom) ? Win32ApiConstants.HTBOTTOMLEFT : Win32ApiConstants.HTLEFT);
                        return;
                    }
                    else if (pos.Y > this.utilRect.Bottom)
                    {
                        m.Result = (IntPtr) Win32ApiConstants.HTBOTTOM;
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Win32ApiFunction.SetWindowTheme(this.Handle, string.Empty, string.Empty);
        }

        private void UpdateRects()
        {
            hasHeaderText = !string.IsNullOrEmpty(this.Text);

            this.headerRect = new RectangleF(0, 0, this.ClientRectangle.Width, HEADER_HEIGHT);
            this.borderRect = new RectangleF(_borderWidth / 2, HEADER_HEIGHT - (_borderWidth / 2), this.ClientRectangle.Width - (_borderWidth), this.ClientRectangle.Height - HEADER_HEIGHT);
            this.borderRect = this.borderRect.ApplyPadding(_borderWidth / 4);
            this.utilRect = this.borderRect.ApplyPadding(1);

            float buttonOffset = BUTTON_MARGIN_RIGHT;
            foreach (SideIconData iconData in this.icons.Values)
            {
                iconData.rect = new RectangleF(this.headerRect.Right - BUTTON_WIDTH - buttonOffset, this.headerRect.Top, BUTTON_WIDTH, this.headerRect.Height);
                iconData.rect = iconData.rect.ShrinkToSize(new SizeF(BUTTON_WIDTH, BUTTON_WIDTH), ContentAlignment.MiddleCenter);
                buttonOffset += BUTTON_WIDTH + BUTTON_MARGIN;
            }

            Graphics g = CreateGraphics();
            SizeF headerTextSize = g.MeasureString(this.Text, this._textFont);

            float rightOffet = ((int)(this._headerTextAlign & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != 0) ? 10 : 10 + buttonOffset;
            this.textHeaderRect = headerRect.ApplyPadding(LEFT_TEXT_PADDING, 0, rightOffet, 0).ShrinkToSize(headerTextSize, this.HeaderTextAlign);

            this.borderBrushLight = new SolidBrush(this._borderColorLight);
            this.borderBrushDark = new SolidBrush(this._borderColorDark);
            this.borderPenLight = new Pen(borderBrushLight, this._borderWidth / 2);
            this.borderPenDark = new Pen(borderBrushDark, this._borderWidth / 2);
            this.headerBrush = new LinearGradientBrush(headerRect, _headerColorDark, _headerColorLight, LinearGradientMode.Vertical);
            this.headerTextBrush = new SolidBrush(this._headerTextColor);

        }


        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;
            g.DrawRectangle(this.borderPenLight, this.borderRect.ToRect());
            g.DrawRectangle(this.borderPenDark, this.borderRect.ApplyPadding(-this._borderWidth / 2).ToRect());
            g.FillRectangle(headerBrush, headerRect);

            if (hasHeaderText)
            {
                g.DrawText(this.Text, this._textFont, headerTextBrush, this.textHeaderRect);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (SideIconData iconData in icons.Values)
            {
                g.DrawImage(iconData.GetImage(), iconData.rect);
            }
            g.SmoothingMode = SmoothingMode.Default;

            //g.DrawRectangle(new Pen(Brushes.Orange, 0.5f), this.fullNcRect);



        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            foreach (SideIconData iconData in icons.Values)
            {
                if (!iconData.hovered && iconData.rect.Contains(e.Location))
                {
                    iconData.hovered = true;
                    this.Invalidate(iconData.rect.ToRect());
                }
                if (iconData.hovered && !iconData.rect.Contains(e.Location))
                {
                    iconData.hovered = false;
                    this.Invalidate(iconData.rect.ToRect());
                }
            }
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (headerRect.Contains(e.Location))
            {
                if (icons["close"].rect.Contains(e.Location))
                {
                    this.Close();
                }
                else if (icons["maximize"].rect.Contains(e.Location))
                {
                    this.MaximizeOrRestore();
                }
                else if (icons["minimize"].rect.Contains(e.Location))
                {
                    this.WindowState = FormWindowState.Minimized;
                }
                else if (e.Clicks == 1 && e.Button == MouseButtons.Left)
                {
                    Win32ApiFunction.ReleaseCapture();
                    Win32ApiFunction.SendMessage(Handle, Win32ApiConstants.WM_NCLBUTTONDOWN, Win32ApiConstants.HT_CAPTION, 0);
                }
                else if (e.Clicks == 2)
                {
                    this.MaximizeOrRestore();
                }
            }
        }
        private void MaximizeOrRestore()
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

