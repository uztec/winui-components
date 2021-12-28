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
    public class ThemeTextBox : RichTextBox, IThemeControlWithHintPlaceholder, IThemeControlWithPrefixSuffix
    {
        public event EventHandler PrependIconClick;
        public event EventHandler AppendIconClick;


        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => BackgroundColorDark; set => BackgroundColorDark = value; }

        [Browsable(false), ReadOnly(true)]
        public new BorderStyle BorderStyle { get; }

        [Browsable(false), ReadOnly(true)]
        public new bool Multiline { get; }

        [Browsable(false)]
        public new Color ForeColor { get => TextColor; set => TextColor = value; }

        [Browsable(false), ReadOnly(true)]
        public new Size MinimumSize { get; set; }


        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false)]
        public bool MouseHovered { get; private set; }

        [Browsable(false)]
        public bool UpdatingTheme { get; set; }

        [Category("Theme"), DefaultValue(true)]
        public bool UseThemeColors { get => this.props.UseThemeColors; set => this.props.UseThemeColors = value; }

        [Category("Theme"), DefaultValue(typeof(Padding), "1; 1; 1; 1;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }


        #region Theme Properties - Text and Background

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TextColor { get => this.props.TextColor; set => this.props.TextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color TextColorDisabled { get => this.props.TextColorDisabled; set => this.props.TextColorDisabled = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Seguoe UI")]
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
        #endregion


        #region Theme Properties - Hint And Placeholder

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HintColor { get => this.props.HintColor; set => this.props.HintColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color HintHighlightColor { get => this.props.HintHighlightColor; set => this.props.HintHighlightColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HintDisabledColor { get => this.props.HintDisabledColor; set => this.props.HintDisabledColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font HintFont { get => this.props.HintFont; set => this.props.HintFont = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public FontClass HintFontClass { get => this.props.HintFontClass; set => this.props.HintFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color PlaceholderColor { get => this.props.PlaceholderColor; set => this.props.PlaceholderColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public Font PlaceholderFont { get => this.props.PlaceholderFont; set => this.props.PlaceholderFont = value; }

        [Category("Theme"), DefaultValue(typeof(string), "")]
        public string PlaceholderHintText { get => this.props.PlaceholderHintText; set => this.props.PlaceholderHintText = value; }

        [Category("Theme"), DefaultValue(typeof(string), "")]
        public bool ShowHint { get => this.props.ShowHint; set => this.props.ShowHint = value; }

        #endregion

        #region Theme Properties - Prefix And Suffix

        [Category("Theme"), DefaultValue(typeof(string), "")]
        public string PrefixText { get => this.prefixSuffixProps.PrefixText; set => this.prefixSuffixProps.PrefixText = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color PrefixTextColor { get => this.prefixSuffixProps.PrefixTextColor; set => this.prefixSuffixProps.PrefixTextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font PrefixFont { get => this.prefixSuffixProps.PrefixFont; set => this.prefixSuffixProps.PrefixFont = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Green")]
        public Color PrefixTextHighlightColor { get => this.prefixSuffixProps.PrefixTextHighlightColor; set => this.prefixSuffixProps.PrefixTextHighlightColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color PrefixTextColorDisabled { get => this.prefixSuffixProps.PrefixTextColorDisabled; set => this.prefixSuffixProps.PrefixTextColorDisabled = value; }

        [Category("Theme"), DefaultValue(typeof(FontClass), "H1")]
        public FontClass PrefixFontClass { get => this.prefixSuffixProps.PrefixFontClass; set => this.prefixSuffixProps.PrefixFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant PrefixTextColorVariant { get => this.prefixSuffixProps.PrefixTextColorVariant; set => this.prefixSuffixProps.PrefixTextColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant PrefixTextColorHightlightVariant { get => this.prefixSuffixProps.PrefixTextColorHightlightVariant; set => this.prefixSuffixProps.PrefixTextColorHightlightVariant = value; }

        [Category("Theme"), DefaultValue(typeof(string), "")]
        public string SuffixText { get => this.prefixSuffixProps.SuffixText; set => this.prefixSuffixProps.SuffixText = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color SuffixTextColor { get => this.prefixSuffixProps.SuffixTextColor; set => this.prefixSuffixProps.SuffixTextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font SuffixFont { get => this.prefixSuffixProps.SuffixFont; set => this.prefixSuffixProps.SuffixFont = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Green")]
        public Color SuffixTextHighlightColor { get => this.prefixSuffixProps.SuffixTextHighlightColor; set => this.prefixSuffixProps.SuffixTextHighlightColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color SuffixTextColorDisabled { get => this.prefixSuffixProps.SuffixTextColorDisabled; set => this.prefixSuffixProps.SuffixTextColorDisabled = value; }

        [Category("Theme"), DefaultValue(typeof(FontClass), "H1")]
        public FontClass SuffixFontClass { get => this.prefixSuffixProps.SuffixFontClass; set => this.prefixSuffixProps.SuffixFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant SuffixTextColorVariant { get => this.prefixSuffixProps.SuffixTextColorVariant; set => this.prefixSuffixProps.SuffixTextColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant SuffixTextColorHightlightVariant { get => this.prefixSuffixProps.SuffixTextColorHightlightVariant; set => this.prefixSuffixProps.SuffixTextColorHightlightVariant = value; }
        #endregion

        // TODO: 
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color SelectionColorDark { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color SelectionColorLight { get; set; }

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


        private readonly ThemeControlWithHintPlaceHolderProperties props;
        private readonly ThemeControlWithPrefixSuffixProperties prefixSuffixProps;

        public ThemeTextBox()
        {
            this.props = new ThemeControlWithHintPlaceHolderProperties(this);
            this.prefixSuffixProps = new ThemeControlWithPrefixSuffixProperties(this);

            // Control Defaults
            this.PlaceholderHintText = "";
            this.ShowHint = true;
            Size = ThemeConstants.DefaultControlSize.ToSize();
            base.BorderStyle = BorderStyle.None;

            this._prependIconMargin = 5;
            this._appendIconMargin = 5;
        }

        public void UpdateStylesFromTheme()
        {
            // Variants
            PrefixTextColor = ThemeScheme.GetPaletteColor(this.PrefixTextColorVariant);
            PrefixTextHighlightColor = ThemeScheme.GetPaletteColor(this.PrefixTextColorHightlightVariant);
            SuffixTextColor = ThemeScheme.GetPaletteColor(this.SuffixTextColorVariant);
            SuffixTextHighlightColor = ThemeScheme.GetPaletteColor(this.SuffixTextColorHightlightVariant);

            PrefixFont = ThemeScheme.GetFontFromClass(this.PrefixFontClass);
            SuffixFont = ThemeScheme.GetFontFromClass(this.SuffixFontClass);

            HintFont = ThemeScheme.GetFontFromClass(this.HintFontClass);

            // Theme
            Font = ThemeScheme.ControlTextFont;
            TextColor = ThemeScheme.ControlTextColorDark;
            TextColorDisabled = ThemeScheme.ControlTextColorDisabled;

            BackgroundColorDark = ThemeScheme.ControlBackgroundColorDark;
            BackgroundColorLight = ThemeScheme.ControlBackgroundColorLight;
            BackgroundColorFocusedDark = ThemeScheme.ControlBackgroundColorDisabledDark;
            BackgroundColorFocusedLight = ThemeScheme.ControlBackgroundColorDisabledLight;
            BackgroundColorDisabledDark = ThemeScheme.ControlBackgroundColorDisabledDark;
            BackgroundColorDisabledLight = ThemeScheme.ControlBackgroundColorDisabledLight;

            HintColor = ThemeScheme.ControlHintTextColor;
            HintHighlightColor = ThemeScheme.ThemeHighlightColor;
            HintDisabledColor = ThemeScheme.ControlHintTextColorDisabled;

            PrefixTextColorDisabled = ThemeScheme.ControlTextColorDisabled;
            SuffixTextColorDisabled = ThemeScheme.ControlTextColorDisabled;

            PlaceholderFont = ThemeScheme.ControlPlaceholderFont;
            PlaceholderColor = ThemeScheme.ControlPlaceholderTextColor;

            SelectionColorDark = ThemeScheme.ThemeSelectionColorDark;
            SelectionColorLight = ThemeScheme.ThemeSelectionColorLight;

            this.InternalPadding = ThemeConstants.DefaultHintControlInternalPadding;
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

            base.Multiline = true;
        }

        public void UpdateRects()
        {
            hasHint = ShowHint && !string.IsNullOrEmpty(PlaceholderHintText);
            hasPrefix = !string.IsNullOrEmpty(PrefixText);
            hasSuffix = !string.IsNullOrEmpty(SuffixText);

            Graphics g = CreateGraphics();

            this.textRect = this.ClientRectangle.ToRectF().ApplyPadding(this.InternalPadding);
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
                SizeF prefixSize = g.MeasureString(PrefixText, PrefixFont, ClientRectangle.Width);
                PointF prefixLcation = new PointF(this.textRect.Left, this.textRect.Bottom - prefixSize.Height);
                this.prefixRect = new RectangleF(prefixLcation, prefixSize);
                this.textRect = this.textRect.ApplyPadding(prefixRect.Width, 0, 0, 0);
            }

            if (hasSuffix)
            {
                SizeF suffixSize = g.MeasureString(SuffixText, SuffixFont, ClientRectangle.Width);
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
            else if (!string.IsNullOrWhiteSpace(PlaceholderHintText) && !Focused)
            {
                Brush placeHolderBrush = Enabled ? new SolidBrush(this.PlaceholderColor) : textBrush;
                g.Clip = new Region(this.textRect);
                g.DrawString(this.PlaceholderHintText, this.PlaceholderFont, placeHolderBrush, this.textRect);
                g.ResetClip();
                // g.FillRectangle(Brushes.Brown, textRect);
            }

            if (this.hasPrefix)
            {
                Brush prefixSuffixBrush = new SolidBrush(this.PrefixTextColor);
                g.Clip = new Region(this.prefixRect);
                g.DrawString(this.PrefixText, this.PrefixFont, prefixSuffixBrush, this.prefixRect);
                g.ResetClip();
            }

            if (this.hasSuffix)
            {
                Brush prefixSuffixBrush = new SolidBrush(this.SuffixTextColor);
                // g.FillRectangle(prefixSuffixBrush, suffixRect);
                g.Clip = new Region(this.suffixRect);
                g.DrawString(this.SuffixText, this.SuffixFont, prefixSuffixBrush, this.suffixRect);
                g.ResetClip();
            }

            if (this.prependIconData.image != null)
            {
                g.DrawImage(this.prependIconData.image, this.prependIconData.rect.ShrinkToSize(this.prependIconData.image.Size, ContentAlignment.MiddleCenter));
            }
            if (this.appendIconData.image != null)
            {
                g.DrawImage(this.appendIconData.image, this.appendIconData.rect.ShrinkToSize(this.appendIconData.image.Size, ContentAlignment.MiddleCenter));
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
            else if (DesignMode)
            {
                return;
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
            Win32ApiFunction.SendMessage(Handle, Win32ApiConstants.EM_SETRECT, 0, ref rc);
        }

    }

}
