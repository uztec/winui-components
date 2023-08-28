using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UzunTec.WinUI.Controls
{
    public class GradientPanel : Panel
    {
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorDark { get { return _backgroundColorDark; } set { _backgroundColorDark = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorLight { get { return _backgroundColorLight; } set { _backgroundColorLight = value; this.Invalidate(); } }

        [Category("Theme"), DefaultValue(typeof(float), "90")]
        public float Angle { get { return _angle; } set { _angle = value; this.Invalidate(); } }

        private Color _backgroundColorDark;

        private Color _backgroundColorLight;

        private float _angle;

        public GradientPanel()
        {
            this._angle = 90f;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Brush bgBrush = new LinearGradientBrush(this.ClientRectangle, this._backgroundColorDark, this._backgroundColorLight, _angle);
            g.FillRectangle(bgBrush, this.ClientRectangle);
        }
    }
}
