using System.ComponentModel;
using System.Drawing.Drawing2D;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class SimpleThemeBaseForm : Form
    {
        private const int BUTTON_MARGIN = 8;
        private const int BUTTON_MARGIN_RIGHT = 15;
        private const int BUTTON_WIDTH = 15;
        private const int HEADER_HEIGHT = 25;
        private const int LEFT_TEXT_PADDING = 10;

        private const int BORDER_WIDTH = 4;
        // private readonly Panel headerPanel;

        private bool hasHeaderText;
        private RectangleF textHeaderRect, headerRect, borderRect, utilRect, logoImageRect;
        private readonly Dictionary<string, SideIconData> icons;




        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false), ReadOnly(true)]
        public new FormBorderStyle FormBorderStyle { get; }


        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image LogoImage { get => _logoImage; set { _logoImage = value; UpdateRects(); Invalidate(); } }
        private Image _logoImage;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleRight")]
        public ContentAlignment LogoImageAlign { get => _logoImageAlign; set { _logoImageAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _logoImageAlign;


        #region Border Properties
        [Category("Z-Custom"), DefaultValue(typeof(Color), "DarkGray")]
        public Color BorderColorDark { get => _borderColorDark; set { _borderColorDark = value; UpdateRects(); Invalidate(); } }
        private Color _borderColorDark;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Blue")]
        public Color BorderColorLight { get => _borderColorLight; set { _borderColorLight = value; UpdateRects(); Invalidate(); } }
        private Color _borderColorLight;

        [Category("Z-Custom"), DefaultValue(typeof(int), "5")]
        public float BorderWidth { get => _borderWidth; set { _borderWidth = value; SetBasePadding(_padding); UpdateRects(); Invalidate(); } }
        private float _borderWidth;

        #endregion

        #region Header Panel and Font Properties

        [Category("Z-Custom"), DefaultValue(typeof(Color), "White")]
        public Color HeaderTextColor { get => _headerTextColor; set { _headerTextColor = value; UpdateRects(); Invalidate(); } }
        private Color _headerTextColor;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "DarkGray")]
        public Color HeaderColorDark { get => _headerColorDark; set { _headerColorDark = value; UpdateRects(); Invalidate(); } }
        private Color _headerColorDark;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Blue")]
        public Color HeaderColorLight { get => _headerColorLight; set { _headerColorLight = value; UpdateRects(); Invalidate(); } }
        private Color _headerColorLight;

        [Category("Z-Custom"), DefaultValue(typeof(Font), "Segeo UI Light, 12")]
        public new Font Font { get => _textFont; set { _textFont = value; UpdateRects(); Invalidate(); } }
        private Font _textFont;

        [Category("Z-Custom"), DefaultValue("")]
        public new string Text { get => base.Text; set { base.Text = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Custom"), DefaultValue("70")]
        public int HeaderHeight { get => _headerHeight; set { _headerHeight = value; UpdateRects(); Invalidate(); } }
        private int _headerHeight;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment HeaderTextAlign { get => _headerTextAlign; set { _headerTextAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _headerTextAlign;

        #endregion

        #region Icon and Padding Properties

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowClose { get => icons["close"].visible; set { icons["close"].visible = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMaximize { get => icons["maximize"].visible; set { icons["maximize"].visible = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowMinimize { get => icons["minimize"].visible; set { icons["minimize"].visible = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "0;0;0;0")]
        public new Padding Padding { get => _padding; set { _padding = value; SetBasePadding(value); Invalidate(); } }
        private void SetBasePadding(Padding value)
        {
            //            base.Padding = value.AddPadding(new Padding((int)this._borderWidth, HEADER_HEIGHT, (int)this._borderWidth, (int)this._borderWidth));
            base.Padding = value.AddPadding(new Padding(0, _headerHeight, 0, 0));
        }
        private Padding _padding;

        #endregion


        public SimpleThemeBaseForm()
        {
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;


            BackColor = ThemeScheme.FormBackgroundColor;
            _textFont = ThemeScheme.FormTitleFont;
            _headerTextColor = ThemeScheme.FormTitleTextColor;
            _headerColorDark = ThemeScheme.FormHeaderColorDark;
            _headerColorLight = ThemeScheme.FormHeaderColorLight;
            _borderColorDark = ThemeScheme.FormTitlePanelBackgroundColorDark.Darken(30);
            _headerColorLight = ThemeScheme.FormTitlePanelBackgroundColorDark;

            _borderWidth = BORDER_WIDTH;
            _headerHeight = 70;
            _headerTextAlign = ContentAlignment.MiddleLeft;
            _logoImageAlign = ContentAlignment.MiddleLeft;

            icons = new Dictionary<string, SideIconData>
            {
                { "close", new SideIconData{ image = Properties.Resources.close_off_dark, imageHovered = Properties.Resources.close_on, visible = true } },
                { "maximize", new SideIconData{ image = Properties.Resources.maximize_off_dark, imageHovered = Properties.Resources.maximize_on, visible = true } },
                { "minimize", new SideIconData{ image = Properties.Resources.minimize_off_dark, imageHovered = Properties.Resources.minimize_on, visible = true } },
            };

            SetBasePadding(_padding);

            UpdateRects();

        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32ApiConstants.WM_NCHITTEST)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);

                if (!utilRect.Contains(pos))
                {
                    if (pos.X > utilRect.Right)
                    {
                        m.Result = (IntPtr)(pos.Y > utilRect.Bottom ? Win32ApiConstants.HTBOTTOMRIGHT : Win32ApiConstants.HTRIGHT);
                        return;
                    }
                    else if (pos.X < utilRect.Left)
                    {
                        m.Result = (IntPtr)(pos.Y > utilRect.Bottom ? Win32ApiConstants.HTBOTTOMLEFT : Win32ApiConstants.HTLEFT);
                        return;
                    }
                    else if (pos.Y > utilRect.Bottom)
                    {
                        m.Result = (IntPtr)Win32ApiConstants.HTBOTTOM;
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateRects();
            SizeChanged += (e, s) => { UpdateRects(); Invalidate(); };
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Win32ApiFunction.SetWindowTheme(Handle, string.Empty, string.Empty);
        }


        private void UpdateRects()
        {
            hasHeaderText = !string.IsNullOrEmpty(Text);
            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                headerRect = new RectangleF(0, 0, ClientRectangle.Width, _headerHeight);

                borderRect = new RectangleF(_borderWidth / 2, HEADER_HEIGHT - _borderWidth / 2, ClientRectangle.Width - _borderWidth, ClientRectangle.Height - HEADER_HEIGHT);
                borderRect = borderRect.ApplyPadding(_borderWidth / 4);
                utilRect = borderRect.ApplyPadding(1);

                float buttonOffset = BUTTON_MARGIN_RIGHT;
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.rect = new RectangleF(headerRect.Right - BUTTON_WIDTH - buttonOffset, headerRect.Top, BUTTON_WIDTH, BUTTON_WIDTH + BUTTON_MARGIN * 2);
                    iconData.rect = iconData.rect.ShrinkToSize(new SizeF(BUTTON_WIDTH, BUTTON_WIDTH), ContentAlignment.MiddleCenter);
                    buttonOffset += BUTTON_WIDTH + BUTTON_MARGIN;
                }

                Graphics g = CreateGraphics();
                SizeF headerTextSize = g.MeasureString(Text, _textFont);
                float rightOffet = (_headerTextAlign & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != 0 ? 10 : 10 + buttonOffset;
                textHeaderRect = headerRect.ApplyPadding(LEFT_TEXT_PADDING, 0, rightOffet, 0).ShrinkToSize(headerTextSize, _headerTextAlign);

                if (_logoImage != null)
                {
                    logoImageRect = headerRect.ApplyPadding(10, 5).ShrinkToSize(_logoImage.Size, _logoImageAlign);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            SolidBrush borderBrushLight = new SolidBrush(_borderColorLight);
            SolidBrush borderBrushDark = new SolidBrush(_borderColorDark);
            Pen borderPenLight = new Pen(borderBrushLight, _borderWidth / 2);
            Pen borderPenDark = new Pen(borderBrushDark, _borderWidth / 2);

            g.DrawRectangle(borderPenLight, borderRect.ToRect());
            g.DrawRectangle(borderPenDark, borderRect.ApplyPadding(-_borderWidth / 2).ToRect());

            LinearGradientBrush headerBrush = new LinearGradientBrush(headerRect, _headerColorDark, _headerColorLight, LinearGradientMode.Vertical);
            g.FillRectangle(headerBrush, headerRect);

            if (hasHeaderText)
            {
                SolidBrush headerTextBrush = new SolidBrush(_headerTextColor);
                g.DrawText(Text, _textFont, headerTextBrush, textHeaderRect);
            }
            if (_logoImage != null)
            {
                g.DrawImage(_logoImage, logoImageRect);
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (SideIconData iconData in icons.Values)
            {
                g.DrawImage(iconData.GetImage(), iconData.rect);
            }
            g.SmoothingMode = SmoothingMode.Default;


        }


        protected override void OnMouseMove(MouseEventArgs e)
        {
            foreach (SideIconData iconData in icons.Values)
            {
                if (!iconData.hovered && iconData.rect.Contains(e.Location))
                {
                    iconData.hovered = true;
                    Invalidate(iconData.rect.ToRect());
                }
                if (iconData.hovered && !iconData.rect.Contains(e.Location))
                {
                    iconData.hovered = false;
                    Invalidate(iconData.rect.ToRect());
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
                    Close();
                }
                else if (icons["maximize"].rect.Contains(e.Location))
                {
                    MaximizeOrRestore();
                }
                else if (icons["minimize"].rect.Contains(e.Location))
                {
                    WindowState = FormWindowState.Minimized;
                }
                else if (e.Clicks == 1 && e.Button == MouseButtons.Left)
                {
                    Win32ApiFunction.ReleaseCapture();
                    Win32ApiFunction.SendMessage(Handle, Win32ApiConstants.WM_NCLBUTTONDOWN, Win32ApiConstants.HT_CAPTION, 0);
                }
                else if (e.Clicks == 2)
                {
                    MaximizeOrRestore();
                }
            }
        }
        private void MaximizeOrRestore()
        {
            if (WindowState == FormWindowState.Normal)
            {
                WindowState = FormWindowState.Maximized;
            }
            else
            {
                WindowState = FormWindowState.Normal;
            }
        }



    }
}
