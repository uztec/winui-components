using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class RoundedCornersForm : Form
    {
        [Browsable(false), ReadOnly(true)]
        public new FormBorderStyle FormBorderStyle { get; }

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpLeftWidth { get => _cornerUpLeftWidth; set { _cornerUpLeftWidth = value; UpdateShapes(); } }
        private float _cornerUpLeftWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpLeftHeight { get => _cornerUpLeftHeight; set { _cornerUpLeftHeight = value; UpdateShapes(); } }
        private float _cornerUpLeftHeight;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpRightWidth { get => _cornerUpRightWidth; set { _cornerUpRightWidth = value; UpdateShapes(); } }
        private float _cornerUpRightWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpRightHeight { get => _cornerUpRightHeight; set { _cornerUpRightHeight = value; UpdateShapes(); } }
        private float _cornerUpRightHeight;


        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownLeftWidth { get => _cornerDownLeftWidth; set { _cornerDownLeftWidth = value; UpdateShapes(); } }
        private float _cornerDownLeftWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownLeftHeight { get => _cornerDownLeftHeight; set { _cornerDownLeftHeight = value; UpdateShapes(); } }
        private float _cornerDownLeftHeight;


        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownRightWidth { get => _cornerDownRightWidth; set { _cornerDownRightWidth = value; UpdateShapes(); } }
        private float _cornerDownRightWidth;


        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownRightHeight { get => _cornerDownRightHeight; set { _cornerDownRightHeight = value; UpdateShapes(); } }
        private float _cornerDownRightHeight;

        [Category("Z-Custom"), DefaultValue(typeof(float), "5")]
        public float BorderWidth { get => _borderWidth; set { _borderWidth = value; UpdateShapes(); } }
        private float _borderWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "3")]
        public float ShadowSize { get => _shadowSize; set { _shadowSize = value; UpdateShapes(); } }
        private float _shadowSize;

        [Browsable(false)]
        public Region BorderRegion { get; private set; }

        [Browsable(false), ReadOnly(true)]
        public Region ShadowRegion { get; private set; }

        private RectangleF utilRect;

        public RoundedCornersForm()
        {
            _cornerUpLeftHeight = 32;
            _cornerUpLeftWidth = 32;
            _cornerUpRightWidth = 32;
            _cornerUpRightHeight = 32;
            _cornerDownLeftHeight = 32;
            _cornerDownLeftWidth = 32;
            _cornerDownRightWidth = 32;
            _cornerDownRightHeight = 32;
            _borderWidth = 5;
            _shadowSize = 3;
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.FormBorderStyle = FormBorderStyle.None;
            SizeChanged += (e, s) => { UpdateShapes(); };
            UpdateShapes();
        }
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Win32ApiFunction.ReleaseCapture();
            Win32ApiFunction.SendMessage(Handle, Win32ApiConstants.WM_NCLBUTTONDOWN, Win32ApiConstants.HT_CAPTION, 0);
        }

        protected void UpdateShapes()
        {
            GraphicsPath graphicpath = new GraphicsPath();
            graphicpath.StartFigure();
            if (_cornerUpLeftHeight > 0 && _cornerUpLeftWidth > 0)
            {
                graphicpath.AddArc(0, 0, _cornerUpLeftWidth, _cornerUpLeftHeight, 180, 90);
            }
            graphicpath.AddLine(_cornerUpLeftWidth, 0, Width - _cornerUpRightWidth, 0);

            if (_cornerUpRightHeight > 0 && _cornerUpRightWidth > 0)
            {
                graphicpath.AddArc(Width - _cornerUpRightWidth, 0, _cornerUpRightWidth, _cornerUpRightHeight, 270, 90);
            }
            graphicpath.AddLine(Width, _cornerUpRightHeight, Width, Height - _cornerDownRightHeight);

            if (_cornerDownRightHeight > 0 && _cornerDownRightWidth > 0)
            {
                graphicpath.AddArc(Width - _cornerDownRightWidth, Height - _cornerDownRightHeight, _cornerDownRightWidth, _cornerDownRightHeight, 0, 90);
            }
            graphicpath.AddLine(Width - _cornerDownRightWidth, Height, _cornerDownLeftWidth, Height);

            if (_cornerDownLeftHeight > 0 && _cornerDownLeftWidth > 0)
            {
                graphicpath.AddArc(0, Height - _cornerDownLeftHeight, _cornerDownLeftWidth, _cornerDownLeftHeight, 90, 90);
            }

            graphicpath.CloseFigure();
            Region = new Region(graphicpath);

            utilRect = ClientRectangle.ToRectF().ApplyPadding(10);

            Region nonShadowedRegion = GetTransformedRegion(Region, _shadowSize, 0, 0);
            ShadowRegion = Region.Clone();
            ShadowRegion.Exclude(nonShadowedRegion);

            Region nonBorderRegion = GetTransformedRegion(nonShadowedRegion, _borderWidth * 2, _borderWidth, _borderWidth);
            BorderRegion = nonShadowedRegion.Clone();
            BorderRegion.Exclude(nonBorderRegion);

            Invalidate();
        }

        private Region GetTransformedRegion(Region baseRegion, float shrink, float offsetX, float offsetY)
        {
            RectangleF rect = ClientRectangle.ToRectF();
            float scaleX = (rect.Width - shrink) / rect.Width;
            float scaleY = (rect.Height - shrink) / rect.Height;

            Matrix transformMatrix = new Matrix();
            transformMatrix.Scale(scaleX, scaleY);
            transformMatrix.Translate(offsetX, offsetY);
            Region output = baseRegion.Clone();
            output.Transform(transformMatrix);

            return output;
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
    }
}
