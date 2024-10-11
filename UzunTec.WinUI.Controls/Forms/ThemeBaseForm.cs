using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls.Forms
{
    public class ThemeBaseForm : StyledForm
    {

        #region Theme

        public Color TitlePanelColorDark { get => _titlePanelColorDark; set { _titlePanelColorDark = value; UpdateRects(); Invalidate(); } }
        private Color _titlePanelColorDark;

        [Category("Theme"), DefaultValue(typeof(Color), "LightYellow")]
        public Color TitlePanelColorLight { get => _titlePanelColorLight; set { _titlePanelColorLight = value; UpdateRects(); Invalidate(); } }
        private Color _titlePanelColorLight;

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Font TitleTextFont { get => _titleTextFont; set { _titleTextFont = value; UpdateRects(); Invalidate(); } }
        private Font _titleTextFont;

        [Category("Theme"), DefaultValue(60)]
        public int TitlePanelHeight { get => _titlePanelHeight; set { _titlePanelHeight = value; SetBasePadding(_padding); UpdateRects(); Invalidate(); } }
        private int _titlePanelHeight;

        #endregion

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Control")]
        public string Title { get => _titleText; set { _titleText = value; UpdateRects(); Invalidate(); } }
        private string _titleText;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowTitlePanel { get => _showTitlePanel; set { _showTitlePanel = value; UpdateRects(); Invalidate(); } }
        private bool _showTitlePanel = true;

        [Category("Z-Custom"), DefaultValue(true)]
        public new bool ShowInTaskbar { get => _showInTaskBar; set { _showInTaskBar = value; base.ShowInTaskbar = value; } }
        private bool _showInTaskBar = true;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Black")]
        public Color TitleTextColor { get => _titleTextColor; set { _titleTextColor = value; Invalidate(); } }
        private Color _titleTextColor;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment TitleTextAlign { get => _titleTextAlign; set { _titleTextAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _titleTextAlign;


        [Browsable(false)]
        public ThemeScheme ThemeScheme => ThemeSchemeManager.Instance.GetTheme();

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image LogoImage { get => logoImageData.image; set { logoImageData.image = value; UpdateRects(); Invalidate(); } }


        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleRight")]
        public ContentAlignment LogoImageAlign { get => _logoImageAlign; set { _logoImageAlign = value; UpdateRects(); Invalidate(); } }
        private ContentAlignment _logoImageAlign;

        [Category("Z-Custom"), DefaultValue(typeof(Padding), "0;0;0;0")]
        public new Padding Padding { get => _padding; set { _padding = value; SetBasePadding(value); Invalidate(); } }

        private void SetBasePadding(Padding value)
        {
            base.Padding = value.AddPadding(new Padding(0, _titlePanelHeight, 0, 0));
        }
        private Padding _padding;

        private RectangleF textTitleRect, titleRect, logoTitleRect;
        private bool hasTitle;
        private readonly SideIconData logoImageData = new SideIconData();

        public ThemeBaseForm()
        {
            _titlePanelHeight = 60;
            _showTitlePanel = true;
            _titleTextAlign = ContentAlignment.MiddleLeft;
            _logoImageAlign = ContentAlignment.MiddleRight;
            MaximizedBounds = Screen.FromHandle(Handle).WorkingArea;

            UpdateStylesFromTheme();
            UpdateRects();
            SetBasePadding(_padding);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            SizeChanged += (s, e) => { UpdateRects(); Invalidate(); };

            UpdateStylesFromTheme();
            UpdateRects();

        }

        private void UpdateStylesFromTheme()
        {
            // Theme
            HeaderColorDark = ThemeScheme.FormHeaderColorDark;
            HeaderColorLight = ThemeScheme.FormHeaderColorLight;
            HeaderTextColor = ThemeScheme.FormHeaderTextColor;
            Font = ThemeScheme.FormHeaderTextFont;

            _titlePanelColorDark = ThemeScheme.FormTitlePanelBackgroundColorDark;
            _titlePanelColorLight = ThemeScheme.FormTitlePanelBackgroundColorLight;
            _titleTextColor = ThemeScheme.FormTitleTextColor;
            _titleTextFont = ThemeScheme.FormTitleFont;

            BackColor = ThemeScheme.FormBackgroundColor;
            BorderColorDark = ThemeScheme.FormTitlePanelBackgroundColorDark.Darken(30);
            BorderColorLight = ThemeScheme.FormTitlePanelBackgroundColorDark;
        }

        private void UpdateRects()
        {
            hasTitle = !string.IsNullOrEmpty(_titleText);

            if (_showTitlePanel)
            {
                titleRect = new RectangleF(0, 25, ClientRectangle.Width, _titlePanelHeight);

                RectangleF titleRectWithPadding = titleRect.ApplyPadding(10, 5);

                if (logoImageData.image != null)
                {
                    logoTitleRect = titleRectWithPadding.ShrinkToSize(logoImageData.image.Size, _logoImageAlign);
                }

                Graphics g = CreateGraphics();
                SizeF titleTextSize = g.MeasureString(_titleText, _titleTextFont);
                textTitleRect = titleRectWithPadding.ShrinkToSize(titleTextSize, _titleTextAlign);
            }

        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;

            if (titleRect.Width > 0f && titleRect.Height > 0f && _showTitlePanel)
            {
                Brush titleBrush = new LinearGradientBrush(titleRect, _titlePanelColorDark, _titlePanelColorLight, LinearGradientMode.ForwardDiagonal);
                g.FillRectangle(titleBrush, titleRect);
            }

            if (logoImageData.image != null)
            {
                g.DrawImage(logoImageData.image, logoTitleRect);
                //g.FillRectangle(Brushes.DarkMagenta, logoTitleRect);
            }

            Brush titleTextBrush = new SolidBrush(TitleTextColor);
            if (hasTitle && _showTitlePanel)
            {
                g.Clip = new Region(textTitleRect);
                g.DrawText(Title, TitleTextFont, titleTextBrush, textTitleRect, TitleTextAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.Firebrick, textTitleRect);
            }
        }
    }
}
