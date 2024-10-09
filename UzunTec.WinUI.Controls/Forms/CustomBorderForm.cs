using System.ComponentModel;
using System.Drawing.Drawing2D;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class CustomBorderForm : Form
    {
        [Browsable(false), ReadOnly(true)]
        public new FormBorderStyle FormBorderStyle { get; }

        [Category("Z-Border"), DefaultValue(typeof(Color), "Control")]
        public Color BorderColorDark { get => _borderColorDark; set { _borderColorDark = value; Invalidate(); } }
        private Color _borderColorDark;

        [Category("Z-Border"), DefaultValue(typeof(Color), "Control")]
        public Color BorderColorLight { get => _borderColorLight; set { _borderColorLight = value; Invalidate(); } }
        private Color _borderColorLight;

        [Category("Z-Border"), DefaultValue(typeof(int), "7")]
        public int BorderWidth { get => _borderWidth; set { _borderWidth = value; SetBasePadding(this._padding); UpdateRects(); Invalidate(); } }
        private int _borderWidth;

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        public new Padding Padding { get => _padding; set { _padding = value; SetBasePadding(this._padding); UpdateRects(); Invalidate(); } }
        private Padding _padding;

        [Category("Z-Custom"), DefaultValue(typeof(bool), "True")]
        public bool Sizable { get => _sizable; set { _sizable = value; Invalidate(); } }
        private bool _sizable;

        // To yse a custom Header
        protected bool borderOnTop;

        // Internal private fields
        private RectangleF _utilRect;
        private Region _borderRegion;

        private void SetBasePadding(Padding value)
        {
            if (borderOnTop)
            {
                base.Padding = value.AddPadding(new Padding((int)this._borderWidth));
            }
            else
            {
                base.Padding = value.AddPadding(new Padding((int)this._borderWidth, 0, (int)this._borderWidth, (int)this._borderWidth));
            }
        }

        public CustomBorderForm()
        {
            this.ControlBox = false;
            this.Padding = new Padding(0);
            this._sizable = true;
            this.borderOnTop = true;
            UpdateRects();
            SizeChanged += (s, e) => { UpdateRects(); Invalidate(); };
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.FormBorderStyle = FormBorderStyle.None;
            this.DoubleBuffered = true;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            UpdateRects();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Win32ApiFunction.SetWindowTheme(Handle, string.Empty, string.Empty);
        }

        private void UpdateRects()
        {
            if (ClientRectangle.Width > 0 && ClientRectangle.Height > 0)
            {
                if (this.borderOnTop)
                {
                    _utilRect = this.ClientRectangle.ToRectF().ApplyPadding(this._borderWidth);
                }
                else
                {
                    _utilRect = this.ClientRectangle.ToRectF().ApplyPadding(this._borderWidth, 0, this._borderWidth, this._borderWidth);
                }

                _borderRegion = new Region(this.ClientRectangle);
                _borderRegion.Exclude(_utilRect);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (this._borderWidth > 0)
            {
                Graphics g = e.Graphics;

                using (var gradientPath = new GraphicsPath())
                {
                    gradientPath.AddRectangle(this.ClientRectangle);
                    using (var borderBrush = new PathGradientBrush(gradientPath))
                    {
                        borderBrush.CenterPoint = new PointF(this.ClientRectangle.Width / 2f, this.ClientRectangle.Height / 2f);
                        borderBrush.CenterColor = this._borderColorLight;
                        borderBrush.SurroundColors = new[] { this._borderColorDark };
                        borderBrush.FocusScales = new PointF(1f - (_borderWidth) / (this.ClientRectangle.Width / 2f), 1f - (_borderWidth) / (this.ClientRectangle.Height / 2f));

                        g.FillRegion(borderBrush, _borderRegion);

                    }
                }
            }
        }

        // Adding Resize Feature
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Win32ApiConstants.WM_NCHITTEST)
            {
                Point pos = new Point(m.LParam.ToInt32());
                pos = PointToClient(pos);

                if (this._sizable && !_utilRect.Contains(pos))
                {
                    if (pos.X > _utilRect.Right)
                    {
                        m.Result = (IntPtr)(pos.Y > _utilRect.Bottom ? Win32ApiConstants.HTBOTTOMRIGHT : Win32ApiConstants.HTRIGHT);
                        return;
                    }
                    else if (pos.X < _utilRect.Left)
                    {
                        m.Result = (IntPtr)(pos.Y > _utilRect.Bottom ? Win32ApiConstants.HTBOTTOMLEFT : Win32ApiConstants.HTLEFT);
                        return;
                    }
                    else if (pos.Y > _utilRect.Bottom)
                    {
                        m.Result = (IntPtr)Win32ApiConstants.HTBOTTOM;
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }
    }
}