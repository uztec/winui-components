using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeDatePicker : DateTimePicker, IThemeControlWithHint
    {
        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BackgroundColorDark; set { this.BackgroundColorDark = value; } }
                
        [Browsable(false)]
        public new Color ForeColor { get => this.TextColor; set { this.TextColor = value; } }

        [Browsable(false)]
        public new Size MinimumSize { get; set; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public new Font Font { get => this._textFont; set { this._textFont = value; this.UpdateRects();  this.Invalidate(); } }
        private Font _textFont;
        
        public new Size Size { get => base.Size; set { base.MinimumSize = value; base.Size = value; this.Invalidate(); } }


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
                this.UpdateRects();
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
                this.UpdateRects();
                Invalidate();
            }
        }
        private bool _showHint;


        [Category("Z-Custom"), DefaultValue(true)]
        public Padding InternalPadding { get => this._internalPadding; set { this._internalPadding = value; this.UpdateRects(); this.Invalidate(); } }
        private Padding _internalPadding;


        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleCenter")]
        public ContentAlignment TextAlign { get => this._textAlign; set { this._textAlign = value; this.Invalidate(); } }
        private ContentAlignment _textAlign;


        //-> Other Values
        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.calendarWhite;
        private RectangleF iconRect, textRect, hintRect;
        private bool hasHint;

        //Constructor
        public ThemeDatePicker()
        {
            // Control Defaults
            base.MinimumSize = new Size(1, 1);
            base.Font = new Font(FontFamily.GenericSansSerif, 28);       // To Adjust the Height
            this.Size = new Size(200, 50);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            this.Format = DateTimePickerFormat.Custom;
            this.CustomFormat = "dd-MMM-yyyy";
            this.PlaceholderHintText = "";
            this._internalPadding = new Padding(5);
            this._textAlign = ContentAlignment.MiddleCenter;
            this._showHint = true;

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

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);

            g.FillBackground(this);
            g.DrawBottomLine(this);

            if (this.hasHint && (Focused || !string.IsNullOrWhiteSpace(this.Text)))
            {
                g.DrawHint(this);
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
            g.DrawImage(calendarIcon, this.iconRect.GetAlignmentPoint(this.calendarIcon.Size, ContentAlignment.MiddleCenter));
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.UpdateRects();
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            LostFocus += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };

            this.UpdateRects();
        }
        private void UpdateRects()
        {
            Graphics g = this.CreateGraphics();

            RectangleF clientRect = this.ClientRectangle.ToRectF().ApplyPadding(this._internalPadding);
            float iconRectWidth = this.calendarIcon.Width + this._internalPadding.Horizontal;
            this.iconRect = clientRect.ApplyPadding(clientRect.Width - iconRectWidth, 0,0,0);
            this.textRect = clientRect.ApplyPadding(0, 0, iconRectWidth, 0);

            if (this.hasHint)
            {
                this.hintRect = this.GetHintRect(g);
                this.textRect = this.textRect.ApplyPadding(0, this.hintRect.Height, 0, 0);
            }
            this.textRect = this.textRect.ApplyPadding(0, 0, 0, this.ClientRectangle.Height - this.GetBottomLineRect().Y);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = (iconRect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }
    }
}
