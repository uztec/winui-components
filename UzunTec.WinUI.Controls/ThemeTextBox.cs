using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeTextBox : RichTextBox, IThemeControlWithHint
    {
        public event EventHandler PrependIconClick;
        public event EventHandler AppendIconClick;


        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => BackgroundColorDark; set => BackgroundColorDark = value; }

        [Browsable(false), ReadOnly(true)]
        public new BorderStyle BorderStyle { get; }

        [Browsable(false)]
        public new Color ForeColor { get => TextColor; set => TextColor = value; }

        [Browsable(false), ReadOnly(true)]
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
            set { _placeholderHintText = value; Invalidate(); }
        }
        private string _placeholderHintText = string.Empty;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowHint
        {
            get => _showHint;
            set { _showHint = value; Invalidate(); }
        }
        private bool _showHint;

        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string Prefix
        {
            get => _prefixText;
            set { _prefixText = value; this.UpdateRects(); this.Invalidate(); }
        }
        private string _prefixText = string.Empty;

        [Category("Z-Custom"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font PrefixFont
        {
            get => _prefixFont;
            set { _prefixFont = value; this.UpdateRects(); this.Invalidate(); }
        }
        private Font _prefixFont;

        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string Suffix
        {
            get => _suffixText;
            set { _suffixText = value; this.UpdateRects(); this.Invalidate(); }
        }
        private string _suffixText = string.Empty;

        [Category("Z-Custom"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font SuffixFont
        {
            get => _suffixFont;
            set { _suffixFont = value; this.UpdateRects(); this.Invalidate(); }
        }
        private Font _suffixFont;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Black")]
        public Color PrefixSuffixTextColor
        {
            get => _prefixSuffixTextColor;
            set { _prefixSuffixTextColor = value; Invalidate(); }
        }
        private Color _prefixSuffixTextColor;

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        public Padding InternalPadding { get => _internalPadding; set { _internalPadding = value; Invalidate(); } }
        private Padding _internalPadding;

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image PrependIcon
        {
            get => this.prependIconData.image;
            set { this.prependIconData.image = value; this.UpdateRects(); this.Invalidate(); }
        }

        [Category("Z-Custom"), DefaultValue(typeof(float), "5")]
        public float PrependIconMargin
        {
            get => this._prependIconMargin;
            set { this._prependIconMargin = value; this.UpdateRects(); this.Invalidate(); }
        }
        private float _prependIconMargin;

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image AppendIcon
        {
            get => this.appendIconData.image;
            set { this.appendIconData.image = value; this.UpdateRects(); this.Invalidate(); }
        }

        [Category("Z-Custom"), DefaultValue(typeof(float), "5")]
        public float AppendIconMargin
        {
            get => this._appendIconMargin;
            set { this._appendIconMargin = value; this.UpdateRects(); this.Invalidate(); }
        }
        private float _appendIconMargin;

        private RectangleF textRect, hintRect, prefixRect, suffixRect;
        private bool hasHint, hasPrefix, hasSuffix;
        private readonly SideIconData prependIconData = new SideIconData();
        private readonly SideIconData appendIconData = new SideIconData();

        public ThemeTextBox()
        {
            // Control Defaults
            PlaceholderHintText = "";
            InternalPadding = new Padding(5);
            _showHint = true;
            Size = new Size(200, 50);
            base.BorderStyle = BorderStyle.None;

            // Theme
            Font = ThemeScheme.ControlTextFont;
            TextColor = ThemeScheme.ControlTextColor;
            DisabledTextColor = ThemeScheme.DisabledControlTextColor;

            FocusedBackgroundColorDark = ThemeScheme.ControlBackgroundColorLight;
            FocusedBackgroundColorLight = ThemeScheme.ControlBackgroundColorLight;

            HintColor = ThemeScheme.ControlHintTextColor;
            HintFont = ThemeScheme.ControlHintFont;
            DisabledHintColor = ThemeScheme.DisableControlHintTextColor;

            HighlightColor = ThemeScheme.ControlHighlightColor;

            PlaceholderFont = ThemeScheme.ControlPlaceholderFont;
            PlaceholderColor = ThemeScheme.ControlPlaceholderColor;

            BackgroundColorDark = ThemeScheme.ControlBackgroundColorDark;
            BackgroundColorLight = ThemeScheme.ControlBackgroundColorLight;
            DisabledBackgroundColorDark = ThemeScheme.DisabledControlBackgroundColorDark;
            DisabledBackgroundColorLight = ThemeScheme.DisabledControlBackgroundColorLight;

            _prefixFont = ThemeScheme.ControlHintFont;
            _suffixFont = ThemeScheme.ControlHintFont;
            _prefixSuffixTextColor = ThemeScheme.ControlTextColor;
            this._prependIconMargin = 5;
            this._appendIconMargin = 5;

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.AutoSize = false;
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            UpdateRects();
            SetTextRect(textRect);

            LostFocus += (sender, args) => { MouseHovered = false; Invalidate(); };
            GotFocus += (sender, args) => Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; Invalidate(); };
            SizeChanged += (sender, args) => { UpdateRects(); SetTextRect(textRect); };
        }

        private void UpdateRects()
        {
            hasHint = _showHint && !string.IsNullOrEmpty(_placeholderHintText);
            hasPrefix = !string.IsNullOrEmpty(_prefixText);
            hasSuffix = !string.IsNullOrEmpty(_suffixText);

            Graphics g = CreateGraphics();

            this.textRect = this.ClientRectangle.ToRectF().ApplyPadding(_internalPadding);
            this.textRect = this.textRect.ApplyPadding(0, 0, 0, this.ClientRectangle.Height - this.GetBottomLineRect().Y);

            float hintOffset = 0;
            if (this.prependIconData.image != null)
            {
                float iconRectWidth = this.prependIconData.image.Width + (2 * this._prependIconMargin);
                this.prependIconData.rect = new RectangleF(0, 0, iconRectWidth, this.ClientRectangle.Height);
                hintOffset = iconRectWidth;
                this.textRect = this.textRect.ApplyPadding(iconRectWidth, 0, 0, 0);
            }

            if (this.appendIconData.image != null)
            {
                float iconRectWidth = this.appendIconData.image.Width + (2 * this._appendIconMargin);
                this.appendIconData.rect = new RectangleF(this.ClientRectangle.Width - iconRectWidth, 0, iconRectWidth, this.ClientRectangle.Height);
                this.textRect = this.textRect.ApplyPadding(0, 0, iconRectWidth, 0);
            }

            if (hasHint)
            {
                this.hintRect = this.GetHintRect(g, new PointF(hintOffset, 0));
                this.textRect = this.textRect.ApplyPadding(0, hintRect.Height, 0, 0);
            }

            if (hasPrefix)
            {
                SizeF prefixSize = g.MeasureString(_prefixText, _prefixFont, ClientRectangle.Width);
                PointF prefixLcation = new PointF(this.textRect.Left, this.textRect.Bottom - prefixSize.Height);
                this.prefixRect = new RectangleF(prefixLcation, prefixSize);
                this.textRect = this.textRect.ApplyPadding(prefixRect.Width, 0, 0, 0);
            }

            if (hasSuffix)
            {
                SizeF suffixSize = g.MeasureString(_suffixText, _suffixFont, ClientRectangle.Width);
                PointF suffixLcation = new PointF(this.textRect.Right - suffixSize.Width, this.textRect.Bottom - suffixSize.Height);
                this.suffixRect = new RectangleF(suffixLcation, suffixSize);
                this.textRect = this.textRect.ApplyPadding(0, 0, suffixSize.Width, 0);
            }

        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);

            g.FillBackground(this);
            g.DrawBottomLine(this);
            // g.FillRectangle(Brushes.Red, this.prependIconRect);

            if (hasHint && (Focused || !string.IsNullOrWhiteSpace(Text)))
            {
                g.DrawHint(this, this.hintRect);
                //g.FillRectangle(Brushes.Blue, hintRect);
            }

            Brush textBrush = ThemeSchemeManager.Instance.GetTextBrush(this);
            if (!string.IsNullOrWhiteSpace(Text))
            {
                g.Clip = new Region(this.textRect);
                g.DrawString(this.Text, this.Font, textBrush, this.textRect);
                g.ResetClip();
                //g.FillRectangle(Brushes.White, textRect);
            }
            else if (!string.IsNullOrWhiteSpace(_placeholderHintText) && !Focused)
            {
                Brush placeHolderBrush = Enabled ? new SolidBrush(this.PlaceholderColor) : textBrush;
                g.Clip = new Region(this.textRect);
                g.DrawString(this._placeholderHintText, this.PlaceholderFont, placeHolderBrush, this.textRect);
                g.ResetClip();
                // g.FillRectangle(Brushes.Brown, textRect);
            }

            if (this.hasPrefix)
            {
                Brush prefixSuffixBrush = new SolidBrush(this._prefixSuffixTextColor);
                g.Clip = new Region(this.prefixRect);
                g.DrawString(this._prefixText, this._prefixFont, prefixSuffixBrush, this.prefixRect);
                g.ResetClip();
            }

            if (this.hasSuffix)
            {
                Brush prefixSuffixBrush = new SolidBrush(this._prefixSuffixTextColor);
                // g.FillRectangle(prefixSuffixBrush, suffixRect);
                g.Clip = new Region(this.suffixRect);
                g.DrawString(this._suffixText, this._suffixFont, prefixSuffixBrush, this.suffixRect);
                g.ResetClip();
            }

            if (this.prependIconData.image != null)
            {
                g.DrawImage(this.prependIconData.image, this.prependIconData.rect.GetAlignmentPoint(this.prependIconData.image.Size, ContentAlignment.MiddleCenter));
            }
            if (this.appendIconData.image != null)
            {
                g.DrawImage(this.appendIconData.image, this.appendIconData.rect.GetAlignmentPoint(this.appendIconData.image.Size, ContentAlignment.MiddleCenter));
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = (this.PrependIconClick != null && this.prependIconData.image != null && this.prependIconData.rect.Contains(e.Location))
                || (this.AppendIconClick != null && this.appendIconData.image != null && this.appendIconData.rect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }


        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (this.PrependIconClick != null && this.prependIconData.image != null && this.prependIconData.rect.Contains(e.Location))
            {
                this.PrependIconClick?.Invoke(this, new EventArgs());
            }
            else if (this.AppendIconClick != null && this.appendIconData.image != null && this.appendIconData.rect.Contains(e.Location))
            {
                AppendIconClick?.Invoke(this, new EventArgs());
            }
            else
            {
                if (DesignMode)
                {
                    return;
                }
            }
            base.OnMouseDown(e);
        }

        protected override void OnSelectionChanged(EventArgs e)
        {
            base.OnSelectionChanged(e);
            Invalidate();
        }




        private void SetTextRect(RectangleF rect)
        {
            RECT rc = new RECT(rect.ApplyPadding(4, 0, 0, 0));
            SendMessage(Handle, EM_SETRECT, 0, ref rc);
        }

        [DllImport(@"User32.dll", EntryPoint = @"SendMessage", CharSet = CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, uint msg, int wParam, ref RECT lParam);


        // Padding
        private const int EM_SETRECT = 0xB3;
        //        Win32ApiConstants.EM_SETRECT

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

            public RECT(RectangleF r) : this(r.ToRect(true))
            {
            }
        }
    }

}
