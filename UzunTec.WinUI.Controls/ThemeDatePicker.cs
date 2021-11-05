using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeDatePicker : DateTimePicker, IThemeControl
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

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public new Font Font { get => this._textFont; set { this._textFont = value; this.Invalidate(); } }
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


        //-> Other Values
        private bool droppedDown = false;
        private Image calendarIcon = Properties.Resources.calendarWhite;
        private RectangleF iconRect;

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
            this.InternalPadding = new Padding(5);
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

            LostFocus += (sender, args) => MouseHovered = false;
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };
  
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

            // TODO: Icon on Left
            int iconRectWidth =  this.calendarIcon.Width + this._internalPadding.Horizontal;
            this.iconRect = new RectangleF(ClientRectangle.Right - iconRectWidth, 0, iconRectWidth, ClientRectangle.Height);

           // g.FillRectangle(Brushes.Red, iconRect);
            availableRectangle = availableRectangle.ApplyPadding(0, 0, this.iconRect.Width - this._internalPadding.Right, 0);

            if (droppedDown == true)
            {
                SolidBrush openIconBrush = new SolidBrush(Color.FromArgb(50, 64, 64, 64));
                g.FillRectangle(openIconBrush, iconRect);
            }
            //Draw icon
            g.DrawImage(calendarIcon, 
                iconRect.Left + (iconRect.Width - this.calendarIcon.Width) / 2,
                iconRect.Top + (iconRect.Height - this.calendarIcon.Height) / 2);


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
                g.DrawString(this.Text, this._textFont, textBrush, availableRectangle);
                g.ResetClip();

            }
            else if (string.IsNullOrWhiteSpace(this._placeholderHintText) && !(droppedDown || Focused))
            {
                Brush placeHolderBrush = Enabled ? new SolidBrush(this.PlaceholderColor) : textBrush;
                g.Clip = new Region(availableRectangle);
                g.DrawString(this._placeholderHintText, this.PlaceholderFont, placeHolderBrush, availableRectangle);
                g.ResetClip();
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            this.Cursor = (iconRect.Contains(e.Location)) ? Cursors.Hand : Cursors.Default;
        }
    }
}
