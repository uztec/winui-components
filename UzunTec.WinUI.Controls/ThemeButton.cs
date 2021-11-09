using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeButton : Button, IThemeControlWithBackground
    {

        public bool ReverseTextColor { get; set; }
        public bool ShowBackground { get; set; }


        [Browsable(false)]
        public new Color BackColor { get; }

        [Browsable(false)]
        public new Color ForeColor { get; }

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

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color DisabledTextColor { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TextColor { get; set; }

        #endregion

        [Category("Z-Custom"), DefaultValue(true)]
        public Padding InternalPadding { get => this._internalPadding; set { this._internalPadding = value; this.Invalidate(); } }
        private Padding _internalPadding;

        public ThemeButton()
        {
            this.InternalPadding = new Padding(1);

            // Theme
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColor;
            this.DisabledTextColor = this.ThemeScheme.DisabledControlTextColor;
            this.HighlightColor = this.ThemeScheme.ControlHighlightColor;
            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.DisabledBackgroundColorDark = this.ThemeScheme.DisabledControlBackgroundColorDark;
            this.DisabledBackgroundColorLight = this.ThemeScheme.DisabledControlBackgroundColorLight;
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
            g.Clear(Parent.BackColor);

            if (this.ShowBackground)
            {
                g.FillBackground(this, false);
            }

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            RectangleF buttonRect = ClientRectangle.ToRectF().ApplyPadding(this._internalPadding);

            RectangleF textRect = buttonRect;
            SizeF textSize = g.MeasureString(this.Text, this.Font, buttonRect.Size);

            float textPlusImageHeight = textSize.Height + this.Image.Height;
            float textPlusImageWidth = textSize.Width + this.Image.Width;

            if (this.Image != null)
            {
                RectangleF imageRect = this.CalculateImageRect(buttonRect, textSize);// textRect;
                PointF imagePoint = imageRect.GetAlignmentPoint(this.Image.Size, this.ImageAlign);
                g.DrawImageUnscaled(this.Image, Point.Ceiling(imagePoint));

                textRect = this.CalculateTextRect(buttonRect, imageRect);// textRect;
            }

            Brush textBrush = (this.Enabled && this.MouseHovered) ? ThemeSchemeManager.Instance.GetHighlightBrush(this)
                    : this.ReverseTextColor ? ThemeSchemeManager.Instance.GetDisabledBackgroundBrush(this)
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