using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class RoundedCornersForm : Form
    {
        [Browsable(false), ReadOnly(true)]
        public new FormBorderStyle FormBorderStyle { get; }

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpLeftWidth { get => this._cornerUpLeftWidth; set { this._cornerUpLeftWidth = value; this.UpdateShapes(); } }
        private float _cornerUpLeftWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpLeftHeight { get => this._cornerUpLeftHeight; set { this._cornerUpLeftHeight = value; this.UpdateShapes(); } }
        private float _cornerUpLeftHeight;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpRightWidth { get => this._cornerUpRightWidth; set { this._cornerUpRightWidth = value; this.UpdateShapes(); } }
        private float _cornerUpRightWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerUpRightHeight { get => this._cornerUpRightHeight; set { this._cornerUpRightHeight = value; this.UpdateShapes(); } }
        private float _cornerUpRightHeight;


        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownLeftWidth { get => this._cornerDownLeftWidth; set { this._cornerDownLeftWidth = value; this.UpdateShapes(); } }
        private float _cornerDownLeftWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownLeftHeight { get => this._cornerDownLeftHeight; set { this._cornerDownLeftHeight = value; this.UpdateShapes(); } }
        private float _cornerDownLeftHeight;


        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownRightWidth { get => this._cornerDownRightWidth; set { this._cornerDownRightWidth = value; this.UpdateShapes(); } }
        private float _cornerDownRightWidth;


        [Category("Z-Custom"), DefaultValue(typeof(float), "25")]
        public float CornerDownRightHeight { get => this._cornerDownRightHeight; set { this._cornerDownRightHeight = value; this.UpdateShapes(); } }
        private float _cornerDownRightHeight;

        [Category("Z-Custom"), DefaultValue(typeof(float), "5")]
        public float BorderWidth { get => this._borderWidth; set { this._borderWidth = value; this.UpdateShapes(); } }
        private float _borderWidth;

        [Category("Z-Custom"), DefaultValue(typeof(float), "3")]
        public float ShadowSize { get => this._shadowSize; set { this._shadowSize = value; this.UpdateShapes(); } }
        private float _shadowSize;

        [Browsable(false)]
        public Region BorderRegion { get; private set; }

        [Browsable(false), ReadOnly(true)]
        public Region ShadowRegion { get; private set; }

        private RectangleF utilRect;

        public RoundedCornersForm()
        {
            this._cornerUpLeftHeight = 32;
            this._cornerUpLeftWidth = 32;
            this._cornerUpRightWidth = 32;
            this._cornerUpRightHeight = 32;
            this._cornerDownLeftHeight = 32;
            this._cornerDownLeftWidth = 32;
            this._cornerDownRightWidth = 32;
            this._cornerDownRightHeight = 32;
            this._borderWidth = 5;
            this._shadowSize = 3;
        }


        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.FormBorderStyle = FormBorderStyle.None;
            SizeChanged += (e, s) => { this.UpdateShapes(); };
            this.UpdateShapes();
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
            if (this._cornerUpLeftHeight > 0 && this._cornerUpLeftWidth > 0)
            {
                graphicpath.AddArc(0, 0, this._cornerUpLeftWidth, this._cornerUpLeftHeight, 180, 90);
            }
            graphicpath.AddLine(this._cornerUpLeftWidth, 0, this.Width - this._cornerUpRightWidth, 0);

            if (this._cornerUpRightHeight > 0 && this._cornerUpRightWidth > 0)
            {
                graphicpath.AddArc(this.Width - this._cornerUpRightWidth, 0, this._cornerUpRightWidth, this._cornerUpRightHeight, 270, 90);
            }
            graphicpath.AddLine(this.Width, this._cornerUpRightHeight, this.Width, this.Height - this._cornerDownRightHeight);

            if (this._cornerDownRightHeight > 0 && this._cornerDownRightWidth > 0)
            {
                graphicpath.AddArc(this.Width - this._cornerDownRightWidth, this.Height - this._cornerDownRightHeight, this._cornerDownRightWidth, this._cornerDownRightHeight, 0, 90);
            }
            graphicpath.AddLine(this.Width - this._cornerDownRightWidth, this.Height, this._cornerDownLeftWidth, this.Height);

            if (this._cornerDownLeftHeight > 0 && this._cornerDownLeftWidth > 0)
            {
                graphicpath.AddArc(0, this.Height - this._cornerDownLeftHeight, this._cornerDownLeftWidth, this._cornerDownLeftHeight, 90, 90);
            }

            graphicpath.CloseFigure();
            this.Region = new Region(graphicpath);

            this.utilRect = this.ClientRectangle.ToRectF().ApplyPadding(10);

            Region nonShadowedRegion = this.GetTransformedRegion(this.Region, this._shadowSize, 0, 0);
            this.ShadowRegion = this.Region.Clone();
            this.ShadowRegion.Exclude(nonShadowedRegion);

            Region nonBorderRegion = this.GetTransformedRegion(nonShadowedRegion, this._borderWidth * 2, this._borderWidth, this._borderWidth);
            this.BorderRegion = nonShadowedRegion.Clone();
            this.BorderRegion.Exclude(nonBorderRegion);

            this.Invalidate();
        }

        private Region GetTransformedRegion(Region baseRegion, float shrink, float offsetX, float offsetY)
        {
            RectangleF rect = this.ClientRectangle.ToRectF();
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
                        m.Result = (IntPtr)Win32ApiConstants.HTBOTTOM;
                        return;
                    }
                }
            }
            base.WndProc(ref m);
        }



    }
}
