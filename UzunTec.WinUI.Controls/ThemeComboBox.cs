using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeComboBox : ComboBox, IThemeControlWithHint
    {
        private const int LINE_BOTTOM_HEIGHT = 1;
        private const int FOCUSED_LINE_BOTTOM_HEIGHT = 2;
        private const int LINE_BOTTOM_PADDING = 1;

        private bool hasHint;

        [Browsable(false)]
        public new Color BackColor { get; }

        [Browsable(false)]
        public new Color ForeColor { get; }

        [Browsable(false)]
        public new Size MinimumSize { get; set; }


        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false)]
        public bool MouseHovered { get; private set; }

        #region Theme Properties

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorDark { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorLight { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color DisabledBackgroundColorDark { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color DisabledBackgroundColorLight { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color FocusedBackgroundColorDark { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color FocusedBackgroundColorLight { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color HighlightColor { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HintColor { get; set; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font HintFont { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color PlaceholderColor { get; set; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public Font PlaceholderFont { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color DisabledTextColor { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TextColor { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color DisabledHintColor { get; set; }

        #endregion

        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string PlaceholderHintText
        {
            get => _placeholderHintText;
            set
            {
                _placeholderHintText = value;
                hasHint = this._showHint && !string.IsNullOrEmpty(_placeholderHintText);
                Invalidate();
            }
        }
        private string _placeholderHintText = string.Empty;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowHint
        {
            get => _showHint;
            set
            {
                _showHint = value;
                hasHint = this._showHint && !string.IsNullOrEmpty(_placeholderHintText);
                Invalidate();
            }
        }
        private bool _showHint;


        [Category("Z-Custom"), DefaultValue(true)]
        public Padding InternalPadding { get => this._internalPadding; set { this._internalPadding = value; this.Invalidate(); } }

        public Color SelectionColorLight { get; set; }
        public Color SelectionColorDark { get; set; }

        private Padding _internalPadding;

        public ThemeComboBox()
        {

            // Control Defaults
            this.PlaceholderHintText = "";
            this.InternalPadding = new Padding(5);
            this._showHint = true;
            this.Size = new Size(200, 50);

            // Theme
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColor;
            this.DisabledTextColor = this.ThemeScheme.DisabledControlTextColor;
            
            this.FocusedBackgroundColorDark = this.ThemeScheme.ControlBackgroundColorLight;
            this.FocusedBackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;

            this.HintColor = this.ThemeScheme.ControlHintTextColor;
            this.HintFont = this.ThemeScheme.ControlHintFont;
            this.DisabledHintColor = this.ThemeScheme.DisableControlHintTextColor;

            this.HighlightColor = this.ThemeScheme.ControlHighlightColor;
            this.SelectionColorDark = Color.FromArgb(80, this.HighlightColor);
            this.SelectionColorLight = Color.FromArgb(50, this.HighlightColor);

            this.PlaceholderFont = this.ThemeScheme.ControlPlaceholderFont;
            this.PlaceholderColor = this.ThemeScheme.ControlPlaceholderColor;

            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.DisabledBackgroundColorDark = this.ThemeScheme.DisabledControlBackgroundColorDark;
            this.DisabledBackgroundColorLight = this.ThemeScheme.DisabledControlBackgroundColorLight;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.AutoSize = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);
            LostFocus += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };

            MeasureItem += CustomMeasureItem;
            DrawItem += CustomDrawItem;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawVariable;
            this.UpdateRects();

        }

        private void UpdateRects()
        {
        }

        private void CustomMeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = this.Height - 7;
        }

        private void CustomDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count || !Focused)
            {
                return;
            }

            Graphics g = e.Graphics;

            // Background
            Brush backgroundBrush = e.State.HasFlag(DrawItemState.Focus) ? (Brush)new LinearGradientBrush(e.Bounds, this.SelectionColorDark, this.SelectionColorLight, LinearGradientMode.Vertical)
                    : new SolidBrush(this.BackgroundColorLight);
            g.FillRectangle(backgroundBrush, e.Bounds);

            string text = this.GetItemText(this.Items[e.Index]);

            g.DrawString(text, this.Font, new SolidBrush(this.TextColor),
                new Point(e.Bounds.Location.X + 14, e.Bounds.Location.Y));

            e.DrawFocusRectangle();

        }



        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);

            RectangleF availableRectangle = new RectangleF(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height).ApplyPadding(this._internalPadding);

            Brush backgroundBrush = Enabled ?
                            (Focused || MouseHovered) ? new LinearGradientBrush(ClientRectangle, this.FocusedBackgroundColorDark, this.FocusedBackgroundColorLight, LinearGradientMode.Vertical)
                            : new LinearGradientBrush(ClientRectangle, this.BackgroundColorDark, this.BackgroundColorLight, LinearGradientMode.Vertical)
                            : new LinearGradientBrush(ClientRectangle, this.DisabledBackgroundColorDark, this.DisabledBackgroundColorLight, LinearGradientMode.Vertical);

            g.FillRectangle(backgroundBrush, ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height - LINE_BOTTOM_PADDING);

            Brush highlightBrush = new SolidBrush(this.HighlightColor);
            Brush textBrush = Enabled ? new SolidBrush(this.TextColor) : new SolidBrush(this.DisabledTextColor);

            // Draw Botom line base
            Brush lineBrush = Focused ? highlightBrush : textBrush;
            float lineHeight = Focused ? FOCUSED_LINE_BOTTOM_HEIGHT : LINE_BOTTOM_HEIGHT;
            float lineY = ClientRectangle.Bottom - lineHeight - (Focused ? 0 : LINE_BOTTOM_PADDING);
            g.FillRectangle(lineBrush, 0, lineY, this.Width, lineHeight);
            availableRectangle = availableRectangle.ApplyPadding(0, 0, 0, ClientRectangle.Bottom - lineY);

            this.DrawTriangle(g, this);


            if (this.hasHint && (Focused || !string.IsNullOrWhiteSpace(this.Text)))
            {
                Brush hintBrush = Enabled ? Focused ? highlightBrush
                    : new SolidBrush(this.HintColor)
                    : new SolidBrush(this.DisabledHintColor);

                SizeF hintSize = g.MeasureString(this._placeholderHintText, this.HintFont, this.Width);
                RectangleF hintRect = new RectangleF(this._internalPadding.Left, this._internalPadding.Top, hintSize.Width, hintSize.Height);
                g.DrawString(this._placeholderHintText, this.HintFont, hintBrush, hintRect);
                availableRectangle = availableRectangle.ApplyPadding(0, hintSize.Height, 0, 0);
            }

            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                g.Clip = new Region(availableRectangle);
                g.DrawString(this.Text, this.Font, textBrush, availableRectangle);
                g.ResetClip();
            }
            else if (!string.IsNullOrWhiteSpace(this._placeholderHintText) && !Focused)
            {
                Brush placeHolderBrush = Enabled ? new SolidBrush(this.PlaceholderColor) : textBrush;
                g.Clip = new Region(availableRectangle);
                g.DrawString(this._placeholderHintText, this.PlaceholderFont, placeHolderBrush, availableRectangle);
                g.ResetClip();
            }
        }

        private void DrawTriangle(Graphics g, ThemeComboBox ctrl)
        {
            // Create and Draw the arrow
            GraphicsPath pth = new GraphicsPath();
            PointF TopRight = new PointF(this.Width - 0.5f - 14, (ctrl.Height >> 1) - 2.5f);
            PointF MidBottom = new PointF(this.Width - 4.5f - 14, (ctrl.Height >> 1) + 2.5f);
            PointF TopLeft = new PointF(this.Width - 8.5f - 14, (ctrl.Height >> 1) - 2.5f);
            pth.AddLine(TopLeft, TopRight);
            pth.AddLine(TopRight, MidBottom);

            g.SmoothingMode = SmoothingMode.AntiAlias;
            Brush triangleBrush = Enabled ? Focused ? new SolidBrush(ctrl.HighlightColor)
                  : new SolidBrush(this.HintColor)
                  : new SolidBrush(this.DisabledHintColor);
            g.FillPath(triangleBrush, pth);
            g.SmoothingMode = SmoothingMode.None;
        }
    }
}
