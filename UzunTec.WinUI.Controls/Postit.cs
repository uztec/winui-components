using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Controls.Themes;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class Postit : Control, IThemeControl
    {
        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BodyColorDark; set { this.BodyColorDark = value; } }

        [Browsable(false), ReadOnly(true)]
        public new Color ForeColor { get => this.BodyTextColor; set { this.BodyTextColor = value; } }

        [Browsable(false), ReadOnly(true)]
        public new Size MinimumSize { get; set; }

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


        #region Theme Properties
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color HeaderColorDark { get => this.postitProps.HeaderColorDark; set => this.postitProps.HeaderColorDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color HeaderColorLight { get => this.postitProps.HeaderColorLight; set => this.postitProps.HeaderColorLight = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Warning")]
        public ColorVariant HeaderColorVariant { get => this.postitProps.HeaderColorVariant; set => this.postitProps.HeaderColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BodyColorDark { get => this.postitProps.BodyColorDark; set => this.postitProps.BodyColorDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BodyColorLight { get => this.postitProps.BodyColorLight; set => this.postitProps.BodyColorLight = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Warning")]
        public ColorVariant PostitBackgroundColorVariant { get => this.postitProps.PostitBackgroundColorVariant; set => this.postitProps.PostitBackgroundColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color HeaderTextColor { get => this.postitProps.HeaderTextColor; set => this.postitProps.HeaderTextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color BodyTextColor { get => this.postitProps.BodyTextColor; set => this.postitProps.BodyTextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color DateTextColor { get => this.postitProps.DateTextColor; set => this.postitProps.DateTextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Font HeaderFont { get => this.postitProps.HeaderFont; set => this.postitProps.HeaderFont = value; }

        [Category("Theme"), DefaultValue(typeof(FontClass), "H1")]
        public FontClass HeaderFontClass { get => this.postitProps.HeaderFontClass; set => this.postitProps.HeaderFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Font DateFont { get => this.postitProps.DateFont; set => this.postitProps.DateFont = value; }

        [Category("Theme"), DefaultValue(typeof(FontClass), "H1")]
        public FontClass DateFontClass { get => this.postitProps.DateFontClass; set => this.postitProps.DateFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Font BodyFont { get => this.postitProps.BodyFont; set => this.postitProps.BodyFont = value; }

        [Category("Theme"), DefaultValue(typeof(FontClass), "H1")]
        public FontClass BodyFontClass { get => this.postitProps.BodyFontClass; set => this.postitProps.BodyFontClass = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color BorderColor { get => this.postitProps.BorderColor; set => this.postitProps.BorderColor = value; }

        [Category("Theme"), DefaultValue(typeof(ColorVariant), "Dark")]
        public ColorVariant BorderColorVariant { get => this.postitProps.BorderColorVariant; set => this.postitProps.BorderColorVariant = value; }

        [Category("Theme"), DefaultValue(typeof(int), "0")]
        public int BorderWidth { get => this.postitProps.BorderWidth; set => this.postitProps.BorderWidth = value; }

        [Category("Theme"), DefaultValue(10)]
        public int Lighten { get => this.postitProps.Lighten; set { this.postitProps.Lighten = value; } }

        [Category("Z-Custom"), DefaultValue(false)]
        public bool PostitFormat { get => this.postitProps.PostitFormat; set { this.postitProps.PostitFormat = value; } }
        #endregion


        public int _headerSize;
        [Category("Z-Custom"), DefaultValue(40)]
        public int HeaderSize { get => _headerSize; set { _headerSize = Math.Max(value, 10); Invalidate(); } }

        private string _text;
        [Category("Z-Custom"), DefaultValue("")]
        public override string Text { get => _text; set { _text = value; Invalidate(); } }

        public string _headerText;
        [Category("Z-Custom"), DefaultValue(typeof(string), "Title")]
        public string HeaderText { get => _headerText; set { _headerText = value; Invalidate(); } }

        private DateTime? _date;
        [Category("Z-Custom")]
        public DateTime? Date { get => _date; set { _date = value; Invalidate(); } }

        public string _dateFormat;
        [Category("Z-Custom"), DefaultValue(typeof(string), "dd-MMM-yyyy")]
        public string DateFormat { get => _dateFormat; set { _dateFormat = value; Invalidate(); } }

        public int _iconMargin;
        [Category("Z-Custom"), DefaultValue(5)]
        public int IconMargin { get => _iconMargin; set { _iconMargin = value; Invalidate(); } }


        private RectangleF headerRect, headerClientRect, bodyRect, textRect, dateRect, iconsRect;
        private readonly ThemeControlProperties props;
        private readonly ThemePostitProperties postitProps;
        private readonly Dictionary<string, SideIconData> icons;
        public EventHandler<string> IconClick;


        public Postit()
        {
            this.props = new ThemeControlProperties(this);
            this.postitProps = new ThemePostitProperties(this);

            icons = new Dictionary<string, SideIconData>();

            Lighten = 55;

            this.HeaderFontClass = FontClass.H2;
            this.BodyFontClass = FontClass.Styled;
            this.DateFontClass = FontClass.Small;
            this.PostitBackgroundColorVariant = ColorVariant.Warning;
            this.HeaderColorVariant = ColorVariant.Info;

            PostitFormat = true;

            this.DateFont = ThemeScheme.GetFontFromClass(DateFontClass);
            this.HeaderFont = ThemeScheme.GetFontFromClass(HeaderFontClass);
            this.DateFont = ThemeScheme.GetFontFromClass(DateFontClass);
            this.BodyFont = ThemeScheme.GetFontFromClass(BodyFontClass);

            HeaderText = "Title";
            HeaderSize = 53;

            Date = DateTime.Today;
            DateFormat = "dd-MMM-yyyy";

            IconMargin = 5;
            Size = new Size(250, 200);

            this.InternalPadding = new Padding(10, 10, 3, 3);
        }

        public void UpdateStylesFromTheme()
        {
            if (PostitFormat)
            {
                // Variants
                BodyColorDark = ThemeScheme.GetPaletteColor(PostitBackgroundColorVariant, true).Lighten(Lighten);
                BodyColorLight = ThemeScheme.GetPaletteColor(PostitBackgroundColorVariant, false).Lighten(Lighten);

                HeaderTextColor = ThemeScheme.ControlTextColorDark;
                BodyTextColor = ThemeScheme.ControlTextColorDark;
                DateTextColor = ThemeScheme.ControlTextColorDark;

                BorderColor = ThemeScheme.GetPaletteColor(BorderColorVariant);

                BorderWidth = 0;
            }
            else
            {
                // Variants
                HeaderColorDark = ThemeScheme.GetPaletteColor(HeaderColorVariant);
                HeaderColorLight = ThemeScheme.GetPaletteColor(HeaderColorVariant);
                BorderColor = ThemeScheme.GetPaletteColor(BorderColorVariant);

                HeaderTextColor = ThemeScheme.ControlTextColorLight;
                DateTextColor = ThemeScheme.ControlTextColorLight;
                BodyTextColor = ThemeScheme.ControlTextColorDark;

                //Theme
                BodyColorDark = ThemeScheme.CellBackgroundColor;
                BodyColorLight = ThemeScheme.CellBackgroundColor;

                BorderWidth = 0;
            }

            HeaderFont = ThemeScheme.GetFontFromClass(HeaderFontClass);
            DateFont = ThemeScheme.GetFontFromClass(DateFontClass);
            BodyFont = ThemeScheme.GetFontFromClass(BodyFontClass);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);
            LostFocus += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            UpdateRects();
        }

        public void AddIcon(string key, Image icon)
        {
            icons.Add(key, new SideIconData { image = icon });
            UpdateRects();
        }

        public void RemoveIcon(string key)
        {
            icons.Remove(key);
            UpdateRects();
        }

        public void ClearIcons()
        {
            icons.Clear();
            UpdateRects();
        }



        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            UpdateRects();
        }


        public void UpdateRects()
        {
            headerRect = new RectangleF(0, 0, Width, _headerSize);
            headerClientRect = headerRect.ApplyPadding(Padding);
            bodyRect = new RectangleF(0, _headerSize, Width, Height - _headerSize);
            textRect = bodyRect.ApplyPadding(this.InternalPadding);

            string date = _date?.ToString(_dateFormat);
            if (date != null)
            {
                SizeF sizeDate = CreateGraphics().MeasureString(date, DateFont);
                dateRect = new RectangleF(headerClientRect.Right - sizeDate.Width,
                                        headerClientRect.Bottom - sizeDate.Height,
                                        sizeDate.Width,
                                        sizeDate.Height);
            }

            float iconHeight = 0;
            float iconWidth = 0;

            foreach (SideIconData iconData in icons.Values)
            {
                iconWidth += iconData.image.Width + _iconMargin;
                if (iconData.image.Height > iconHeight)
                {
                    iconHeight = iconData.image.Height;
                }
            }

            iconsRect = new RectangleF(textRect.Right - iconWidth, textRect.Bottom - iconHeight, iconWidth, iconHeight);
            RectangleF iconDrawRect = iconsRect;

            foreach (SideIconData iconData in icons.Values)
            {
                iconData.rect = iconDrawRect.ShrinkToSize(iconData.image.Size, ContentAlignment.BottomLeft);
                iconDrawRect = iconDrawRect.ApplyPadding(iconData.image.Width + IconMargin, 0, 0, 0);
            }

        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            if (PostitFormat)
            {
                Brush backgroundBrush = new LinearGradientBrush(this.ClientRectangle, this.BodyColorDark, this.BodyColorLight, LinearGradientMode.Vertical);
                g.FillRectangle(backgroundBrush, this.ClientRectangle);
            }
            else
            {
                Brush brushHeader = new LinearGradientBrush(this.headerRect, HeaderColorDark, HeaderColorLight, LinearGradientMode.Vertical);
                g.FillRectangle(brushHeader, this.headerRect);

                Brush backgroundBrush = new LinearGradientBrush(this.bodyRect, this.BodyColorDark, this.BodyColorLight, LinearGradientMode.Vertical);
                g.FillRectangle(backgroundBrush, this.bodyRect);
            }

            Brush brushHeaderText = new SolidBrush(HeaderTextColor);
            g.DrawString(_headerText, HeaderFont, brushHeaderText, headerClientRect);

            Brush brushText = new SolidBrush(this.BodyTextColor);
            g.Clip = new Region(textRect);
            g.Clip.Exclude(iconsRect);
            g.DrawString(_text, BodyFont, brushText, textRect);
            g.ResetClip();

            string date = _date?.ToString(_dateFormat);
            if (date != null)
            {
                Brush brushDate = new SolidBrush(DateTextColor);
                g.DrawString(date, DateFont, brushDate, dateRect);
            }

            foreach (SideIconData iconData in icons.Values)
            {
                if (iconData.hovered)
                {
                    g.FillRectangle(Brushes.White, iconData.rect);
                }
                g.DrawImage(iconData.image, iconData.rect);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool cursorInIcon = false;
            if (iconsRect.Contains(e.Location))
            {
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.hovered = iconData.rect.Contains(e.Location);
                    cursorInIcon |= iconData.hovered;
                }
            }
            else
            {
                foreach (SideIconData iconData in icons.Values)
                {
                    iconData.hovered = false;
                }
            }
            Invalidate();
            Cursor = (cursorInIcon) ? Cursors.Hand : Cursors.Default;
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            base.OnMouseClick(e);
            if (iconsRect.Contains(e.Location))
            {
                foreach (string key in icons.Keys)
                {
                    SideIconData iconData = icons[key];
                    if (iconData.rect.Contains(e.Location))
                    {
                        IconClick?.Invoke(this, key);
                    }
                }
            }
        }
    }
}
