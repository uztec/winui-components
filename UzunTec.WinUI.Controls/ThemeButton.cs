using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeButton : Button, IThemeControlWithTextBackground
    {
        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BackgroundColorDark; set => this.BackgroundColorDark = value; }

        [Browsable(false), ReadOnly(true)]
        public new Color ForeColor { get => this.TextColor; set => this.TextColor = value; }

        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Browsable(false)]
        public bool MouseHovered { get; private set; }

        #region Theme Properties
        [Category("Theme"), DefaultValue(true)]
        public bool UseThemeColors { get => this.props.UseThemeColors; set => this.props.UseThemeColors = value; }

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

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color HighlightColor { get => this.props.HighlightColor; set => this.props.HighlightColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color TextColorDisabled { get => this.props.TextColorDisabled; set => this.props.TextColorDisabled = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TextColor { get => this.props.TextColor; set => this.props.TextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Padding), "1; 1; 1; 1;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }
        #endregion

       
        [Category("Z-Custom"), DefaultValue(false)]
        public bool ReverseTextColor { get => this._reverseTextColor; set { this._reverseTextColor = value; this.Invalidate(); } }
        private bool _reverseTextColor;

        [Category("Z-Custom"), DefaultValue(false)]
        public bool ShowBackground { get => this._showBackground; set { this._showBackground = value; this.Invalidate(); } }
        private bool _showBackground;

        private readonly ThemeControlWithTextBackgroundProperties props;

        public ThemeButton()
        {
            this.props = new ThemeControlWithTextBackgroundProperties(this)
            {
                Invalidate = this.Invalidate,
                UpdateStylesFromTheme = this.UpdateStylesFromTheme,
            };
            this.InternalPadding = new Padding(1);
        }

        private void UpdateStylesFromTheme()
        {
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColor;
            this.TextColorDisabled = this.ThemeScheme.DisabledControlTextColor;
            this.HighlightColor = this.ThemeScheme.ControlHighlightColor;
            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.BackgroundColorFocusedDark = this.ThemeScheme.ControlBackgroundColorLight;
            this.BackgroundColorFocusedLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.BackgroundColorDisabledDark = this.ThemeScheme.DisabledControlBackgroundColorDark;
            this.BackgroundColorDisabledLight = this.ThemeScheme.DisabledControlBackgroundColorLight;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            LostFocus += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;
            g.Clear(this.GetParentColor());

            if (this._showBackground)
            {
                g.FillBackground(this, false);
            }

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF buttonRect = ClientRectangle.ToRectF().ApplyPadding(this.InternalPadding);

            RectangleF textRect = buttonRect;
            SizeF textSize = g.MeasureString(this.Text, this.Font, buttonRect.Size);

            if (this.Image != null)
            {
                RectangleF imageRect = this.CalculateImageRect(buttonRect, textSize);// textRect;
                imageRect = imageRect.ShrinkToSize(this.Image.Size, this.ImageAlign);
                g.DrawImageUnscaled(this.Image, Point.Ceiling(imageRect.Location));

                textRect = this.CalculateTextRect(buttonRect, imageRect);// textRect;
            }

            Brush textBrush = (this.Enabled && this.MouseHovered) ? ThemeSchemeManager.Instance.GetHighlightBrush(this)
                    : this._reverseTextColor ? ThemeSchemeManager.Instance.GetDisabledBackgroundBrush(this)
                    : ThemeSchemeManager.Instance.GetTextBrush(this);

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
            throw new NotImplementedException();
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