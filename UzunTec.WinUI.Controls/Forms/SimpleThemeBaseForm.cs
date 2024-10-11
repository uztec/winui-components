using System.ComponentModel;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class SimpleThemeBaseForm : StyledForm
    {
        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image LogoImage { get => _logoImage; set { _logoImage = value; UpdateRects(); Invalidate(); } }
        private Image _logoImage;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleRight")]
        public ContentAlignment LogoImageAlign { get => _logoImageAlign; set { _logoImageAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _logoImageAlign;

        private RectangleF _logoImageRect;

        public SimpleThemeBaseForm()
        {
            this.MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;
            this.BackColor = ThemeScheme.FormBackgroundColor;
            this._logoImageAlign = ContentAlignment.MiddleLeft;

            this.Font = ThemeScheme.FormTitleFont;
            this.HeaderTextColor = ThemeScheme.FormTitleTextColor;
            this.HeaderColorDark = ThemeScheme.FormHeaderColorDark;
            this.HeaderColorLight = ThemeScheme.FormHeaderColorLight;
            this.BorderColorDark = ThemeScheme.FormTitlePanelBackgroundColorDark.Darken(30);
            this.BorderColorLight = ThemeScheme.FormTitlePanelBackgroundColorDark;

            UpdateRects();

        }

        private void UpdateRects()
        {
            if (this.ClientRectangle.Height > 0 && this.ClientRectangle.Width > 0)
            {
                var headerRect = new RectangleF(0, 0, ClientRectangle.Width, this.HeaderHeight);

                if (_logoImage != null)
                {
                    _logoImageRect = headerRect.ApplyPadding(10, 5).ShrinkToSize(_logoImage.Size, _logoImageAlign);
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            Graphics g = e.Graphics;

            if (_logoImage != null)
            {
                g.DrawImage(_logoImage, _logoImageRect);
            }
        }
    }
}
