using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeTextBox : RichTextBox, IThemeControl
    {
        private const int LINE_BOTTOM_HEIGHT = 1;
        private const int FOCUSED_LINE_BOTTOM_HEIGHT = 2;
        private const int LINE_BOTTOM_PADDING = 1;

        private bool hasHint;

        [Browsable(false)]
        public new Color BackColor { get; }

        [Browsable(false), ReadOnly(true)]
        public new BorderStyle BorderStyle { get; }

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
        private Padding _internalPadding;

        public ThemeTextBox()
        {
            // Control Defaults
            this.PlaceholderHintText = "";
            this.InternalPadding = new Padding(5);
            this._showHint = true;
            this.Size = new Size(200, 50);
            base.BorderStyle = BorderStyle.None;

            // Theme
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColor;
            this.DisabledTextColor = this.ThemeScheme.DisabledControlTextColor;

            this.HintColor = this.ThemeScheme.ControlHintTextColor;
            this.HintFont = this.ThemeScheme.ControlHintFont;
            this.DisabledHintColor = this.ThemeScheme.DisableControlHintTextColor;

            this.HighlightColor = this.ThemeScheme.ControlHighlightColor;

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

            this.UpdateRects();

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

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            //if (LeadingIcon != null && _leadingIconBounds.Contains(e.Location))
            //{
            //    LeadingIconClick?.Invoke(this, new EventArgs());
            //}
            //else if (TrailingIcon != null && _trailingIconBounds.Contains(e.Location))
            //{
            //    TrailingIconClick?.Invoke(this, new EventArgs());
            //}
            //else
            //{
            //    if (DesignMode)
            //        return;
            //}
            base.OnMouseDown(e);
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            Invalidate();
        }

        private void UpdateRects(bool RedefineTextField = true)
        {
            Graphics g = Graphics.FromHwnd(this.Handle);

            RectangleF availableRectangle = new RectangleF(ClientRectangle.X, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height).ApplyPadding(this._internalPadding);

            float lineHeight = Focused ? FOCUSED_LINE_BOTTOM_HEIGHT : LINE_BOTTOM_HEIGHT;
            float lineY = ClientRectangle.Bottom - lineHeight - (Focused ? 0 : LINE_BOTTOM_PADDING);
            availableRectangle = availableRectangle.ApplyPadding(0, 0, 0, ClientRectangle.Bottom - lineY);

            SizeF hintSize = g.MeasureString(this._placeholderHintText, this.HintFont, this.Width);
            RectangleF hintRect = new RectangleF(this._internalPadding.Left, this._internalPadding.Top, hintSize.Width, hintSize.Height);
            availableRectangle = availableRectangle.ApplyPadding(0, hintSize.Height, 0, 0);

            if (RedefineTextField)
            {
                RECT rc = new RECT(availableRectangle);
                SendMessage(Handle, EM_SETRECT, 0, ref rc);
            }

        }


        [DllImport(@"User32.dll", EntryPoint = @"SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, ref RECT lParam);


        // Padding
        private const int EM_SETRECT = 0xB3;


        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public readonly int Left;
            public readonly int Top;
            public readonly int Right;
            public readonly int Bottom;

            private RECT(int left, int top, int right, int bottom)
            {
                Left = left;
                Top = top;
                Right = right;
                Bottom = bottom;
            }

            public RECT(Rectangle r) : this(r.Left, r.Top, r.Right, r.Bottom)
            {
            }

            public RECT(RectangleF r) : this((int)r.Left, (int)r.Top, (int)r.Right, (int)r.Bottom)
            {
            }
        }
    }

}
