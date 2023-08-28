﻿using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeButton : Button, IThemeControlWithTextBackground
    {
        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BackgroundColorDark; set { } }

        [Browsable(false), ReadOnly(true)]
        public new Color ForeColor { get => this.TextColor; set { } }

        [Browsable(false), ReadOnly(true)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false), ReadOnly(true)]
        public bool MouseHovered { get; private set; }

        [Browsable(false), ReadOnly(true)]
        public bool UpdatingTheme { get; set; }

        [Category("Theme"), DefaultValue(true)]
        public bool UseThemeColors { get => this.props.UseThemeColors; set => this.props.UseThemeColors = value; }

        [Category("Theme"), DefaultValue(typeof(Padding), "1; 1; 1; 1;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }

        [Category("Theme"), DefaultValue(typeof(int), "20")]
        public int HoverLighten { get => this._hoverLighten; set => this._hoverLighten = value; }
        private int _hoverLighten;


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


        #region Theme Properties - Button Specific Properties

        [Category("Theme"), DefaultValue(typeof(FontClass), "Body")]
        public FontClass TextFontClass { get => this.btnProps.TextFontClass; set => this.btnProps.TextFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color TextColorHighlight { get => this.btnProps.TextColorHighlight; set => this.btnProps.TextColorHighlight = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color BorderColor { get => this.btnProps.BorderColor; set => this.btnProps.BorderColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color BorderColorHighlight { get => this.btnProps.BorderColorHighlight; set => this.btnProps.BorderColorHighlight = value; }
        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color BorderColorDisabled { get => this.btnProps.BorderColorDisabled; set => this.btnProps.BorderColorDisabled = value; }

        [Category("Theme"), DefaultValue(typeof(int), "0")]
        public int BorderWidth { get => this.btnProps.BorderWidth; set => this.btnProps.BorderWidth = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant TextColorVariant { get => this.btnProps.TextColorVariant; set => this.btnProps.TextColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant TextColorHightlightVariant { get => this.btnProps.TextColorHightlightVariant; set => this.btnProps.TextColorHightlightVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant TextColorDisabledVariant { get => this.btnProps.TextColorDisabledVariant; set => this.btnProps.TextColorDisabledVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BackgroundColorVariant { get => this.btnProps.BackgroundColorVariant; set => this.btnProps.BackgroundColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BackgroundColorFocusedVariant { get => this.btnProps.BackgroundColorFocusedVariant; set => this.btnProps.BackgroundColorFocusedVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BackgroundColorDisabledVariant { get => this.btnProps.BackgroundColorDisabledVariant; set => this.btnProps.BackgroundColorDisabledVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BorderColorVariant { get => this.btnProps.BorderColorVariant; set => this.btnProps.BorderColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BorderColorHighlightVariant { get => this.btnProps.BorderColorHighlightVariant; set => this.btnProps.BorderColorHighlightVariant = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BorderColorDisabledVariant { get => this.btnProps.BorderColorDisabledVariant; set => this.btnProps.BorderColorDisabledVariant = value; }

        [Category("Theme"), DefaultValue(false)]
        public bool Transparent { get => this.btnProps.Transparent; set => this.btnProps.Transparent = value; }

        #endregion

        private readonly ThemeControlWithTextBackgroundProperties props;
        private readonly ThemeButtonProperties btnProps;

        public ThemeButton()
        {
            this.props = new ThemeControlWithTextBackgroundProperties(this);
            this.btnProps = new ThemeButtonProperties(this);
            this.InternalPadding = new Padding(1);
            this.BorderWidth = 0;
            this.TextColorVariant = ColorVariant.Dark;
            this.TextColorHightlightVariant = ColorVariant.Light;
            this.TextColorDisabledVariant = ColorVariant.Light;
            this.BackgroundColorVariant = ColorVariant.Secondary;
            this.BackgroundColorFocusedVariant = ColorVariant.Info;
            this.BackgroundColorDisabledVariant = ColorVariant.Secondary;
            this.BorderColorVariant = ColorVariant.Primary;
            this.BorderColorHighlightVariant = ColorVariant.Primary;
            this.BorderColorDisabledVariant = ColorVariant.Secondary;
            this.TextFontClass = FontClass.Body;
            this._hoverLighten = 20;
        }

        public void UpdateStylesFromTheme()
        {
            this.Font = this.ThemeScheme.GetFontFromClass(this.TextFontClass);
            this.TextColor = this.ThemeScheme.GetPaletteColor(this.TextColorVariant);
            this.TextColorDisabled = this.ThemeScheme.GetPaletteColor(this.TextColorDisabledVariant);
            this.TextColorHighlight = this.ThemeScheme.GetPaletteColor(this.TextColorHightlightVariant);

            this.BackgroundColorDark = this.ThemeScheme.GetPaletteColor(this.BackgroundColorVariant, true);
            this.BackgroundColorLight = this.ThemeScheme.GetPaletteColor(this.BackgroundColorVariant, false);
            this.BackgroundColorFocusedDark = this.ThemeScheme.GetPaletteColor(this.BackgroundColorFocusedVariant, true);
            this.BackgroundColorFocusedLight = this.ThemeScheme.GetPaletteColor(this.BackgroundColorFocusedVariant, false);
            this.BackgroundColorDisabledDark = this.ThemeScheme.GetPaletteColor(this.BackgroundColorDisabledVariant, true);
            this.BackgroundColorDisabledLight = this.ThemeScheme.GetPaletteColor(this.BackgroundColorDisabledVariant, false);

            this.BorderColor = this.ThemeScheme.GetPaletteColor(this.BorderColorVariant, true);
            this.BorderColorHighlight = this.ThemeScheme.GetPaletteColor(this.BorderColorHighlightVariant, true);
            this.BorderColorDisabled = this.ThemeScheme.GetPaletteColor(this.BorderColorDisabledVariant, true);
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.SupportsTransparentBackColor | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            base.BackColor = Color.Transparent;
            LostFocus += (sender, args) => { MouseHovered = false; this.UpdateRects(); this.Invalidate(); };
            GotFocus += (sender, args) => { this.UpdateRects(); this.Invalidate(); };
            MouseEnter += (sender, args) => { MouseHovered = true; this.UpdateRects(); this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.UpdateRects(); this.Invalidate(); };
            Click += (sender, args) => { this.UpdateRects(); this.Invalidate(); };
            this.UpdateRects();

        }

        private RectangleF buttonRect, textRect, imageRect, borderRect;
        private Pen borderPen;
        private SizeF textSize;
        private Brush textBrush, backgroundBrush;


        public void UpdateRects()
        {
            if (IsHandleCreated)
            {
                this.buttonRect = ClientRectangle.ToRectF();

                Brush borderBrush = this.Enabled ?
                       (this.Focused) ? new SolidBrush(this.BorderColorHighlight)
                       : new SolidBrush(this.BorderColor)
                       : new SolidBrush(this.BorderColorDisabled);

                int border = (this.Focused) ? this.BorderWidth + 1 : this.BorderWidth;
                this.borderPen = (border > 0)? new Pen(borderBrush, border) : null;
                this.borderRect = this.buttonRect.ApplyPadding((float)border / 2); 
                this.textRect = buttonRect.ApplyPadding(this.InternalPadding);

                Graphics g = CreateGraphics();
                this.textSize = g.MeasureString(this.Text, this.Font, buttonRect.Size);

                if (this.Image != null)
                {
                    this.imageRect = this.CalculateImageRect(buttonRect, textSize);
                    imageRect = imageRect.ShrinkToSize(this.Image.Size, this.ImageAlign);
                    textRect = this.CalculateTextRect(buttonRect, imageRect);
                }

                this.textBrush = (this.Enabled && this.MouseHovered) ? new SolidBrush(this.TextColorHighlight)
                        : ThemeSchemeManager.Instance.GetTextBrush(this);

                this.backgroundBrush = this.Enabled ?
                            this.MouseHovered ? new LinearGradientBrush(this.ClientRectangle,
                                                            this.BackgroundColorFocusedDark.LightenOrDarken(this._hoverLighten),
                                                            this.BackgroundColorFocusedLight.LightenOrDarken(this._hoverLighten),
                                                            LinearGradientMode.Vertical)
                            /* Normal */   : new LinearGradientBrush(this.ClientRectangle, this.BackgroundColorDark, this.BackgroundColorLight, LinearGradientMode.Vertical)
                            /* Disabled */ : new LinearGradientBrush(this.ClientRectangle, this.BackgroundColorDisabledDark, this.BackgroundColorDisabledLight, LinearGradientMode.Vertical);

            }
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            base.OnPaintBackground(e);
            Graphics g = e.Graphics;

            if (!this.Transparent)
            {
                g.FillRectangle(backgroundBrush, this.ClientRectangle);
            }
            else if (MouseHovered)
            {
                g.FillRectangle(new SolidBrush(Color.FromArgb(this._hoverLighten, 0, 0, 0)), this.ClientRectangle);
            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            InvokePaintBackground(this, e);

            if (this.borderPen != null)
            {
                g.DrawRectangle(this.borderPen, this.borderRect.ToRect());
            }

            if (this.Image != null)
            {               
                g.DrawImageUnscaled(this.Image, Point.Ceiling(imageRect.Location));
            }
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.DrawText(this.Text, this.Font, textBrush, textRect, textSize, this.TextAlign);
        }

        private RectangleF CalculateTextRect(RectangleF buttonRect, RectangleF imageRect)
        {
            switch (this.TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    return buttonRect;

                case TextImageRelation.ImageAboveText:
                    return buttonRect.ApplyPadding(0, imageRect.Height, 0, 0);

                case TextImageRelation.ImageBeforeText:
                    return buttonRect.ApplyPadding(imageRect.Width, 0, 0, 0);

                case TextImageRelation.TextAboveImage:
                    return buttonRect.ApplyPadding(0, 0, 0, imageRect.Height);

                case TextImageRelation.TextBeforeImage:
                    return buttonRect.ApplyPadding(0, 0, imageRect.Width, 0);
            }
            return buttonRect;
        }

        private RectangleF CalculateImageRect(RectangleF buttonRect, SizeF textSize)
        {
            switch (this.TextImageRelation)
            {
                case TextImageRelation.Overlay:
                    return buttonRect;

                case TextImageRelation.ImageAboveText:
                    return buttonRect.ApplyPadding(0, 0, 0, textSize.Height);

                case TextImageRelation.ImageBeforeText:
                    return buttonRect.ApplyPadding(0, 0, buttonRect.Width - this.Image.Width, 0);

                case TextImageRelation.TextAboveImage:
                    return buttonRect.ApplyPadding(0, textSize.Height, 0, 0);

                case TextImageRelation.TextBeforeImage:
                    return buttonRect.ApplyPadding(buttonRect.Width - this.Image.Width, 0, 0, 0);
            }
            return buttonRect;
        }
    }
}