using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Windows.Forms;
using MaterialSkin;

namespace UzunTec.WinUI.Controls
{
    public class EnhancedButton : Button, IMaterialControl
    {
        [Browsable(false)]
        public int Depth { get; set; }

        [Browsable(false)]
        public MaterialSkinManager SkinManager => MaterialSkinManager.Instance;

        [Browsable(false)]
        public MouseState MouseState { get; set; }

        public bool ReverseTextColor { get; set; }

        public EnhancedButton()
        {
            this.Font = this.SkinManager.getFontByType(MaterialSkinManager.fontType.Button);
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            //double hoverAnimProgress = _hoverAnimationManager.GetProgress();

            g.Clear(Parent.BackColor);
            // button rectand path

            RectangleF buttonRectF = new RectangleF(ClientRectangle.Location, ClientRectangle.Size);
            buttonRectF.X -= 0.5f;
            buttonRectF.Y -= 0.5f;

            //base.OnPaint(pevent);

            Rectangle textRect = ClientRectangle;

            if (this.Image != null)
            {
                SizeF textSize = g.MeasureString(this.Text, SkinManager.getFontByType(MaterialSkinManager.fontType.Button), ClientRectangle.Width);
                int textHeight = String.IsNullOrWhiteSpace(this.Text)? 0 : (int)Math.Ceiling(textSize.Height);

                int paddingTop = (ClientRectangle.Height - (textHeight + this.Image.Height)) / 2;

                Rectangle imageRect = new Rectangle(
                    ClientRectangle.X + (ClientRectangle.Width - this.Image.Width) / 2,
                    ClientRectangle.Y + paddingTop,
                    ClientRectangle.Width,
                    this.Image.Height);

                g.DrawImageUnscaled(this.Image, imageRect);

                textRect.Y = imageRect.Bottom + 4;
                textRect.Height = textHeight + 4;
            }

            Color textColor = this.Enabled ? this.MouseState == MouseState.HOVER ?
                   SkinManager.ColorScheme.AccentColor :   // Hover 
                   this.ReverseTextColor? SkinManager.ColorScheme.TextColor :  // Reverse Text Color
                   SkinManager.ColorScheme.PrimaryColor :  // Normal
                   SkinManager.TextDisabledOrHintColor; // Disabled

            using (NativeTextRenderer NativeText = new NativeTextRenderer(g))
            {
                NativeText.DrawMultilineTransparentText(this.Text, this.SkinManager.getLogFontByType(MaterialSkinManager.fontType.Button),
                    textColor,
                    textRect.Location,
                    textRect.Size,
                    NativeTextRenderer.TextAlignFlags.Center | NativeTextRenderer.TextAlignFlags.Middle);
            }
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode)
            {
                return;
            }

            MouseState = MouseState.OUT;
            MouseEnter += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                Invalidate();
            };
            MouseLeave += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                Invalidate();
            };
            MouseDown += (sender, args) =>
            {
                if (args.Button == MouseButtons.Left)
                {
                    MouseState = MouseState.DOWN;
                    Invalidate();
                }
            };
            MouseUp += (sender, args) =>
            {
                MouseState = MouseState.HOVER;
                Invalidate();
            };

            GotFocus += (sender, args) =>
            {
                Invalidate();
            };
            LostFocus += (sender, args) =>
            {
                MouseState = MouseState.OUT;
                Invalidate();
            };
        }

    }
}
