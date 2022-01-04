using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public partial class ThemeBaseForm : StyledForm
    {

        #region Theme

        public Color TitlePanelColorDark { get => _titlePanelColorDark; set { _titlePanelColorDark = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _titlePanelColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color TitlePanelColorLight { get => _titlePanelColorLight; set { _titlePanelColorLight = value; this.UpdateRects(); this.Invalidate(); } }
        private Color _titlePanelColorLight;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font TitleTextFont { get => _titleTextFont; set { _titleTextFont = value; this.UpdateRects();  this.Invalidate(); } }
        private Font _titleTextFont;

        [Category("Theme"), DefaultValue(60)]
        public int TitlePanelHeight { get => _titlePanelHeight; set { _titlePanelHeight = value; this.SetBasePadding(_padding); this.UpdateRects(); this.Invalidate(); } }
        private int _titlePanelHeight;

        #endregion

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Control")]
        public string Title { get => this._titleText; set { this._titleText = value; this.UpdateRects();  this.Invalidate(); } }
        private string _titleText;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowTitlePanel { get => _showTitlePanel; set { _showTitlePanel = value; this.UpdateRects(); this.Invalidate(); } }
        private bool _showTitlePanel;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Black")]
        public Color TitleTextColor { get => _titleTextColor; set { _titleTextColor = value; this.Invalidate(); } }
        private Color _titleTextColor;
        
        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment TitleTextAlign { get => this._titleTextAlign; set { this._titleTextAlign = value; this.UpdateRects(); this.Invalidate(); } }
        private ContentAlignment _titleTextAlign;


        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image LogoImage { get => this.logoImageData.image; set { this.logoImageData.image = value; this.UpdateRects(); this.Invalidate(); } }

 
        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleRight")]
        public ContentAlignment LogoImageAlign { get => this._logoImageAlign; set { this._logoImageAlign = value; this.UpdateRects(); this.Invalidate(); } }
        private ContentAlignment _logoImageAlign;

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "0;0;0;0")]
        public new Padding Padding { get => this._padding; set { this._padding = value; this.SetBasePadding(value); this.Invalidate(); } }

        private void SetBasePadding(Padding value)
        {
            base.Padding = value.AddPadding(new Padding(0, (int)this._titlePanelHeight, 0, 0));
        }
        private Padding _padding;

        private RectangleF textTitleRect, titleRect, logoTitleRect;
        private bool hasTitle;
        private readonly SideIconData logoImageData = new SideIconData();

        public ThemeBaseForm()
        {
            this._titlePanelHeight = 60;
            this._showTitlePanel = true;
            this._titleTextAlign = ContentAlignment.MiddleLeft;
            this._logoImageAlign = ContentAlignment.MiddleRight;

            this.UpdateStylesFromTheme();
            this.UpdateRects();
            this.SetBasePadding(this._padding);

        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            SizeChanged += (s, e) => { this.UpdateRects(); this.Invalidate(); };

            this.UpdateStylesFromTheme();
            this.UpdateRects();
        }

        private void UpdateStylesFromTheme()
        {
            // Theme
            this.HeaderColorDark = this.ThemeScheme.FormHeaderColorDark;
            this.HeaderColorLight = this.ThemeScheme.FormHeaderColorLight;
            this.HeaderTextColor = this.ThemeScheme.FormHeaderTextColor;
            this.Font = this.ThemeScheme.FormHeaderTextFont;

            this._titlePanelColorDark = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
            this._titlePanelColorLight = this.ThemeScheme.FormTitlePanelBackgroundColorLight;
            this._titleTextColor = this.ThemeScheme.FormTitleTextColor;
            this._titleTextFont = this.ThemeScheme.FormTitleFont;

            this.BackColor = this.ThemeScheme.FormBackgroundColor;
            this.BorderColorDark = this.ThemeScheme.FormTitlePanelBackgroundColorDark.Darken(30);
            this.BorderColorLight = this.ThemeScheme.FormTitlePanelBackgroundColorDark;
        }

        private void UpdateRects()
        {
            hasTitle = !string.IsNullOrEmpty(_titleText);

            if (this._showTitlePanel)
            {
                this.titleRect = new RectangleF(0, 25, this.ClientRectangle.Width, _titlePanelHeight);

                RectangleF titleRectWithPadding = titleRect.ApplyPadding(10, 5);

                if (this.logoImageData.image != null)
                {
                    this.logoTitleRect = titleRectWithPadding.ShrinkToSize(this.logoImageData.image.Size, this._logoImageAlign);
                }

                Graphics g = CreateGraphics();
                SizeF titleTextSize = g.MeasureString(this._titleText, this._titleTextFont);
                this.textTitleRect = titleRectWithPadding.ShrinkToSize(titleTextSize, this._titleTextAlign);
            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e); 
            Graphics g = e.Graphics;

            if (titleRect.Width > 0f && titleRect.Height > 0f && this._showTitlePanel)
            {
                Brush titleBrush = new LinearGradientBrush(titleRect, _titlePanelColorDark, _titlePanelColorLight, LinearGradientMode.ForwardDiagonal);
                g.FillRectangle(titleBrush, titleRect);
            }

            if (this.logoImageData.image != null)
            {
                g.DrawImage(logoImageData.image, this.logoTitleRect);
                //g.FillRectangle(Brushes.DarkMagenta, logoTitleRect);
            }

            Brush titleTextBrush = new SolidBrush(this.TitleTextColor);
            if (hasTitle && this._showTitlePanel)
            {
                g.Clip = new Region(this.textTitleRect);
                g.DrawText(this.Title, this.TitleTextFont, titleTextBrush, this.textTitleRect, this.TitleTextAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.Firebrick, textTitleRect);
            }
        }
    }
}
