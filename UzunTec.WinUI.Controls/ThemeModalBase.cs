using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeModalBase : Form
    {
        [Browsable(false), ReadOnly(true)]
        public new FormBorderStyle FormBorderStyle { get; private set; }


        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BorderColorDark { get => _borderColorDark; set { _borderColorDark = value; Invalidate(); } }
        private Color _borderColorDark;


        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BorderColorLight { get => _borderColorLight; set { _borderColorLight = value; Invalidate(); } }
        private Color _borderColorLight;


        [Category("Z-Custom"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        public new Padding Padding { get => _internalPadding; set { SetPadding(value); Invalidate(); } }
        private Padding _internalPadding;

        private void SetPadding(Padding value)
        {
            _internalPadding = value;
            base.Padding = Padding.Add(value, new Padding(_borderWidth));
        }

        [Category("Z-Custom"), DefaultValue(typeof(int), "5")]
        public int BorderWidth { get => _borderWidth; set { _borderWidth = value; Invalidate(); } }
        private int _borderWidth;


        public ThemeModalBase()
        {
            ControlBox = false;
            Padding = new Padding(5);
            _borderWidth = 3;

            // Theme
            BackColor = ThemeScheme.FormBackgroundColor;
            BorderColorDark = ThemeScheme.ControlHintTextColor;
            BorderColorLight = ThemeScheme.ControlHintTextColor;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.FormBorderStyle = FormBorderStyle.None;

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (_borderWidth > 0)
            {
                Graphics g = e.Graphics;
                Brush borderBrush = new LinearGradientBrush(ClientRectangle, _borderColorDark, _borderColorLight, LinearGradientMode.ForwardDiagonal);

                var borderRegion = new Region(this.ClientRectangle);
                borderRegion.Exclude(this.ClientRectangle.ToRectF().ApplyPadding(new Padding(this.BorderWidth)));
                g.FillRegion(borderBrush, borderRegion);
            }
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // ThemeModalBase
            // 
            this.ClientSize = new System.Drawing.Size(284, 261);
            this.Name = "ThemeModalBase";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);

        }
    }
}
