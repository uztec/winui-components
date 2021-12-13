using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeDatePicker : DateTimePicker, IThemeControlWithHint
    {
        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BackgroundColorDark; set => this.BackgroundColorDark = value; }

        [Browsable(false)]
        public new Color ForeColor { get => this.TextColor; set => this.TextColor = value; }

        [Browsable(false)]
        public new Size MinimumSize { get; set; }

        public new Size Size { get => base.Size; set { base.MinimumSize = value; base.Size = value; this.Invalidate(); } }

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false)]
        public bool MouseHovered { get; private set; }

        #region Theme Properties
        [Category("Theme"), DefaultValue(true)]
        public bool UseThemeColors { get => this.props.UseThemeColors; set => this.props.UseThemeColors = value; }

        [Category("Theme"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TextColor { get => this.props.TextColor; set => this.props.TextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color TextColorDisabled { get => this.props.TextColorDisabled; set => this.props.TextColorDisabled = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public new Font Font { get => this.props.TextFont; set => this.props.TextFont = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorDark { get => this.props.BackgroundColorDark; set => this.props.BackgroundColorDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorLight { get => this.props.BackgroundColorLight; set => this.props.BackgroundColorLight = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorDisabledDark { get => this.props.BackgroundColorDisabledDark; set => this.props.BackgroundColorDisabledDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorDisabledLight { get => this.props.BackgroundColorDisabledLight; set => this.props.BackgroundColorDisabledLight = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorFocusedDark { get => this.props.BackgroundColorFocusedDark; set => this.props.BackgroundColorFocusedDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BackgroundColorFocusedLight { get => this.props.BackgroundColorFocusedLight; set => this.props.BackgroundColorFocusedLight = value; }



        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HintColor { get => this.props.HintColor; set => this.props.HintColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font HintFont { get => this.props.HintFont; set => this.props.HintFont = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color HintHighlightColor { get => this.props.HintHighlightColor; set => this.props.HintHighlightColor = value; }

        [Category("Theme"), DefaultValue(typeof(FontClass), "Hint")]
        public FontClass HintFontClass { get => this.props.HintFontClass; set => this.props.HintFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HintDisabledColor { get => this.props.HintDisabledColor; set => this.props.HintDisabledColor = value; }

        #endregion

        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string PlaceholderHintText { get => this.props.PlaceholderHintText; set => this.props.PlaceholderHintText = value; }


        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowHint { get => this.props.ShowHint; set => this.props.ShowHint = value; }


        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign { get => this._textAlign; set { this._textAlign = value; this.Invalidate(); } }
        private ContentAlignment _textAlign;

        // Private
        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.calendarWhite;
        private RectangleF iconRect, textRect, hintRect;
        private bool hasHint;
        private readonly ThemeControlWithHintProperties props;

        public ThemeDatePicker()
        {
            this.props = new ThemeControlWithHintProperties(this);

            // Control Defaults
            base.MinimumSize = new Size(1, 1);
            base.Font = new Font(FontFamily.GenericSansSerif, 28);       // To Adjust the Height
            this.Size = ThemeConstants.DefaultControlSize.ToSize();
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "dd-MMM-yyyy";
            this.PlaceholderHintText = "";
            this._textAlign = ContentAlignment.MiddleCenter;
            this.ShowHint = true;
            this.InternalPadding = ThemeConstants.DefaultHintControlInternalPadding;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
        }

        public void UpdateStylesFromTheme()
        {
            // Theme
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColorDark;
            this.TextColorDisabled = this.ThemeScheme.ControlTextColorDisabled;

            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.BackgroundColorDisabledDark = this.ThemeScheme.DisabledControlBackgroundColorDark;
            this.BackgroundColorDisabledLight = this.ThemeScheme.DisabledControlBackgroundColorLight;
            this.BackgroundColorFocusedDark = ThemeScheme.ControlBackgroundColorLight;
            this.BackgroundColorFocusedLight = ThemeScheme.ControlBackgroundColorLight;

            this.HintColor = this.ThemeScheme.ControlHintTextColor;
            this.HintFont = this.ThemeScheme.GetFontFromClass(this.HintFontClass);
            this.HintDisabledColor = this.ThemeScheme.ThemeHighlightColor;
            this.HintHighlightColor = this.ThemeScheme.ControlHighlightColor;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.UpdateRects();

            LostFocus += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            SizeChanged += (sender, args) => { this.UpdateRects(); };

        }
        public void UpdateRects()
        {
            this.hasHint = this.ShowHint && !string.IsNullOrEmpty(this.PlaceholderHintText);

            Graphics g = this.CreateGraphics();

            RectangleF clientRect = this.ClientRectangle.ToRectF().ApplyPadding(this.InternalPadding);
            float iconRectWidth = this.calendarIcon.Width + this.InternalPadding.Horizontal;
            this.iconRect = clientRect.ApplyPadding(clientRect.Width - iconRectWidth, 0, 0, 0);
            this.textRect = clientRect.ApplyPadding(0, 0, iconRectWidth, 0);

            if (this.hasHint)
            {
                this.hintRect = this.GetHintRect(g);
                this.textRect = this.textRect.ApplyPadding(0, this.hintRect.Height, 0, 0);
            }
            this.textRect = this.textRect.ApplyPadding(0, 0, 0, this.ClientRectangle.Height - this.GetBottomLineRect().Y);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);

            g.FillBackground(this);
            g.DrawBottomLine(this);

            if (this.hasHint && (Focused || !string.IsNullOrWhiteSpace(this.Text)))
            {
                g.DrawHint(this, this.hintRect);
            }

            Brush textBrush = ThemeSchemeManager.Instance.GetTextBrush(this);
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                g.Clip = new Region(this.textRect);
                g.DrawText(this.Text, this.Font, textBrush, this.textRect, this._textAlign);
                g.ResetClip();
            }

            if (droppedDown == true)
            {
                SolidBrush openIconBrush = new SolidBrush(Color.FromArgb(50, 64, 64, 64));
                g.FillRectangle(openIconBrush, this.iconRect);
            }

            //Draw icon
            g.DrawImage(calendarIcon, this.iconRect.ShrinkToSize(this.calendarIcon.Size, ContentAlignment.MiddleCenter));
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            if (this.BackgroundColorDark.GetBrightness() >= 0.6F)
            {
                calendarIcon = Properties.Resources.calendarDark;
            }
            else
            {
                calendarIcon = Properties.Resources.calendarWhite;
            }
            base.OnInvalidated(e);
        }


        protected override void OnDropDown(EventArgs eventargs)
        {
            base.OnDropDown(eventargs);
            droppedDown = true;
        }
        protected override void OnCloseUp(EventArgs eventargs)
        {
            base.OnCloseUp(eventargs);
            droppedDown = false;
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);
            e.Handled = true;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = (iconRect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }
    }
}
