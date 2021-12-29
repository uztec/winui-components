﻿using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeLabel : Label, IThemeControlWithTextBackground
    {
        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BackgroundColorDark; set => this.BackgroundColorDark = value; }

        [Browsable(false), ReadOnly(true)]
        public new Color ForeColor { get => this.TextColor; set => this.TextColor = value; }

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
        public new Font Font { get => this.props.TextFont; set { this.props.TextFont = value; base.Font = value; } }

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

        #region Theme Properties - Label Specific ( Button Properties Reducted )

        [Category("Theme"), DefaultValue(typeof(FontClass), "Body")]
        public FontClass TextFontClass { get => this.btnProps.TextFontClass; set => this.btnProps.TextFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant TextColorVariant { get => this.btnProps.TextColorVariant; set => this.btnProps.TextColorVariant = value; }

        [Category("Theme"), DefaultValue(true)]
        public bool Transparent { get => this.btnProps.Transparent; set => this.btnProps.Transparent = value; }

        #endregion

        private readonly ThemeControlWithTextBackgroundProperties props;
        private readonly ThemeButtonProperties btnProps;


        public ThemeLabel()
        {
            this.props = new ThemeControlWithTextBackgroundProperties(this);
            this.btnProps = new ThemeButtonProperties(this);
            this.InternalPadding = new Padding(1);
            this.Transparent = true;
            this.TextFontClass = FontClass.Body;
            this.TextColorVariant = ColorVariant.Dark;
        }

        public void UpdateStylesFromTheme()
        {
            // Variants
            this.Font = this.ThemeScheme.GetFontFromClass(this.TextFontClass);
            this.TextColor = this.ThemeScheme.GetPaletteColor(this.TextColorVariant);

            // Theme
            this.TextColorDisabled = this.ThemeScheme.ControlTextColorDisabled;
            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.BackgroundColorFocusedDark = this.ThemeScheme.ControlBackgroundColorFocusedLight;
            this.BackgroundColorFocusedLight = this.ThemeScheme.ControlBackgroundColorFocusedLight;
            this.BackgroundColorDisabledDark = this.ThemeScheme.ControlBackgroundColorDisabledDark;
            this.BackgroundColorDisabledLight = this.ThemeScheme.ControlBackgroundColorDisabledLight;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };
        }

        public void UpdateRects()
        {
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.Clear(this.GetParentColor());

            if (!this.Transparent)
            {
                g.FillBackground(this, false);
            }
            RectangleF textRect = ClientRectangle.ToRectF().ApplyPadding(this.InternalPadding);
            Brush textBrush = ThemeSchemeManager.Instance.GetTextBrush(this);
            g.DrawText(this.Text, this.Font, textBrush, textRect, this.TextAlign);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            Size s = base.GetPreferredSize(proposedSize);
            return new Size(s.Width + this.InternalPadding.Horizontal, s.Height + this.InternalPadding.Vertical);
        }
    }
}