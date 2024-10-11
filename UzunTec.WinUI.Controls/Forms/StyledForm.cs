using System.ComponentModel;
using System.Drawing.Drawing2D;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class StyledForm : CustomBorderForm
    {
        private const int BUTTON_MARGIN = 8;
        private const int BUTTON_MARGIN_RIGHT = 15;
        private const int BUTTON_WIDTH = 15;
        private const int LEFT_TEXT_PADDING = 10;


        #region Header Panel and Font Properties

        [Category("Z-Header"), DefaultValue(typeof(Color), "White")]
        public Color HeaderTextColor { get => _headerTextColor; set { _headerTextColor = value; UpdateRects(); Invalidate(); } }
        private Color _headerTextColor;

        [Category("Z-Header"), DefaultValue(typeof(Color), "DarkGray")]
        public Color HeaderColorDark { get => _headerColorDark; set { _headerColorDark = value; UpdateRects(); Invalidate(); } }
        private Color _headerColorDark;

        [Category("Z-Header"), DefaultValue(typeof(Color), "Blue")]
        public Color HeaderColorLight { get => _headerColorLight; set { _headerColorLight = value; UpdateRects(); Invalidate(); } }
        private Color _headerColorLight;

        [Category("Z-Header"), DefaultValue(typeof(Font), "Segeo UI Light, 12")]
        public new Font Font { get => _textFont; set { _textFont = value; UpdateRects(); Invalidate(); } }
        private Font _textFont;

        [Category("Z-Header"), DefaultValue("")]
        public new string Text { get => base.Text; set { base.Text = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Header"), DefaultValue("25")]
        public int HeaderHeight { get => _headerHeight; set { _headerHeight = value; SetBasePadding(_padding);  UpdateRects(); Invalidate(); } }
        private int _headerHeight;

        [Category("Z-Header"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment HeaderTextAlign { get => _headerTextAlign; set { _headerTextAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _headerTextAlign;

        [Category("Z-Header"), DefaultValue(true)]
        public bool ShowClose { get => _icons["close"].visible; set { _icons["close"].visible = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Header"), DefaultValue(true)]
        public bool ShowMaximize { get => _icons["maximize"].visible; set { _icons["maximize"].visible = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Header"), DefaultValue(true)]
        public bool ShowMinimize { get => _icons["minimize"].visible; set { _icons["minimize"].visible = value; UpdateRects(); Invalidate(); } }

        [Category("Z-Header"), DefaultValue(typeof(ContentAlignment), "TopRight")]
        public ContentAlignment ControlBoxAlign { get => _controlBoxAlign; set { _controlBoxAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _controlBoxAlign;

        #endregion

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "0;0;0;0")]
        public new Padding Padding { get => _padding; set { _padding = value; SetBasePadding(value); Invalidate(); } }
        private void SetBasePadding(Padding value)
        {
            base.Padding = value.AddPadding(new Padding(0, _headerHeight, 0, 0));
        }
        private Padding _padding;

        // Internal private fields

        private bool _hasHeaderText;
        private RectangleF _textHeaderRect, _headerRect, _utilRect;
        private readonly Dictionary<string, SideIconData> _icons;
        
        public StyledForm()
        {
            base.borderOnTop = false;

            _headerHeight = 25;
            _headerTextAlign = ContentAlignment.MiddleLeft;
            _headerColorDark = Color.DarkGray;
            _headerColorLight = Color.Blue;
            _headerTextColor = Color.White;
            _textFont = new Font(FontFamily.GenericSansSerif, 10);
            _controlBoxAlign = ContentAlignment.TopRight;

            _icons = new Dictionary<string, SideIconData>
            {
                { "close", new SideIconData{ image = Properties.Resources.close_off_dark, imageHovered = Properties.Resources.close_on, visible = true } },
                { "maximize", new SideIconData{ image = Properties.Resources.maximize_off_dark, imageHovered = Properties.Resources.maximize_on, visible = true } },
                { "minimize", new SideIconData{ image = Properties.Resources.minimize_off_dark, imageHovered = Properties.Resources.minimize_on, visible = true } },
            };

            SetBasePadding(_padding);
            UpdateRects();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            UpdateRects();
            SizeChanged += (e, s) => { UpdateRects(); };
        }
        private void UpdateRects()
        {
            _hasHeaderText = !string.IsNullOrEmpty(Text);
            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                _headerRect = new RectangleF(0, 0, ClientRectangle.Width, _headerHeight);
                _utilRect = new RectangleF(0, _headerHeight, ClientRectangle.Width, ClientRectangle.Height - _headerHeight);

                float buttonsRectWidth = 2 * BUTTON_MARGIN_RIGHT + 3 * (BUTTON_MARGIN + BUTTON_WIDTH);
                float buttonsRectHeight = BUTTON_WIDTH + 2 * BUTTON_MARGIN;
                RectangleF buttonsRect = _headerRect.ShrinkToSize(new SizeF(buttonsRectWidth, buttonsRectHeight ), _controlBoxAlign);

                float buttonOffset = BUTTON_MARGIN_RIGHT;
                foreach (SideIconData iconData in _icons.Values)
                {
                    if (iconData.visible)
                    {
                        iconData.rect = new RectangleF(buttonsRect.Right - BUTTON_WIDTH - buttonOffset, buttonsRect.Top, BUTTON_WIDTH, buttonsRect.Height);
                        iconData.rect = iconData.rect.ShrinkToSize(new SizeF(BUTTON_WIDTH, BUTTON_WIDTH), ContentAlignment.MiddleCenter);
                        buttonOffset += BUTTON_WIDTH + BUTTON_MARGIN;
                    }
                }

                Graphics g = CreateGraphics();
                SizeF headerTextSize = g.MeasureString(Text, _textFont);

                float rightOffet = (_headerTextAlign & (ContentAlignment.TopCenter | ContentAlignment.MiddleCenter | ContentAlignment.BottomCenter)) != 0 ? 10 : 10 + buttonOffset;
                _textHeaderRect = _headerRect.ApplyPadding(LEFT_TEXT_PADDING, 0, rightOffet, 0).ShrinkToSize(headerTextSize, HeaderTextAlign);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            using (Brush headerBrush = new LinearGradientBrush(_headerRect, _headerColorDark, _headerColorLight, LinearGradientMode.Vertical))
            {
                g.FillRectangle(headerBrush, _headerRect);
            }

            if (_hasHeaderText)
            {
                using (Brush headerTextBrush = new SolidBrush(_headerTextColor))
                {
                    g.DrawText(Text, _textFont, headerTextBrush, _textHeaderRect);
                };
            }

            g.SmoothingMode = SmoothingMode.AntiAlias;

            foreach (SideIconData iconData in _icons.Values)
            {
                if (iconData.visible)
                {
                    g.DrawImage(iconData.GetImage(), iconData.rect);
                }
            }
            g.SmoothingMode = SmoothingMode.Default;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            foreach (SideIconData iconData in _icons.Values)
            {
                if (iconData.visible)
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
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (_headerRect.Contains(e.Location))
            {
                if (_icons["close"].rect.Contains(e.Location))
                {
                    Close();
                }
                else if (_icons["maximize"].rect.Contains(e.Location))
                {
                    MaximizeOrRestore();
                }
                else if (_icons["minimize"].rect.Contains(e.Location))
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

