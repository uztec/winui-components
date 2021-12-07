using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class Postit : Control, IThemeControlWithBackground
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
        public Color DisabledBackgroundColorDark { get => this.props.DisabledBackgroundColorDark; set => this.props.DisabledBackgroundColorDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color DisabledBackgroundColorLight { get => this.props.DisabledBackgroundColorLight; set => this.props.DisabledBackgroundColorLight = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color FocusedBackgroundColorDark { get => this.props.FocusedBackgroundColorDark; set => this.props.FocusedBackgroundColorDark = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color FocusedBackgroundColorLight { get => this.props.FocusedBackgroundColorLight; set => this.props.FocusedBackgroundColorLight = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Red")]
        public Color HighlightColor { get => this.props.HighlightColor; set => this.props.HighlightColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Gray")]
        public Color DisabledTextColor { get => this.props.DisabledTextColor; set => this.props.DisabledTextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color TextColor { get => this.props.TextColor; set => this.props.TextColor = value; }

        [Category("Theme"), DefaultValue(typeof(Padding), "1; 1; 1; 1;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }
        #endregion



        private Color _headerColorDark;
        [Category("Custom"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorDark { get => _headerColorDark; set { _headerColorDark = value; Invalidate(); } }

        private Color _headerColorLight;
        [Category("Custom"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorLight { get => _headerColorLight; set { _headerColorLight = value; Invalidate(); } }

        private Color _headerTextColor;
        [Category("Custom"), DefaultValue(typeof(Color), "Black")]
        public Color HeaderTextColor { get => _headerTextColor; set { _headerTextColor = value; Invalidate(); } }

        private Color _dateColor;
        [Category("Custom"), DefaultValue(typeof(Color), "Black")]
        public Color DateColor { get => _dateColor; set { _dateColor = value; Invalidate(); } }

        public int _headerSize;
        [Category("Custom"), DefaultValue(40)]
        public int HeaderSize { get => _headerSize; set { _headerSize = Math.Max(value, 10); Invalidate(); } }


        private Font _headerFont;
        [Category("Custom"), DefaultValue(typeof(Font), "Arial; 14pt")]
        public Font HeaderFont { get => _headerFont; set { _headerFont = value; Invalidate(); } }

        private Font _dateFont;
        [Category("Custom"), DefaultValue(typeof(Font), "Comic Sans MS; 9pt; style=Bold")]
        public Font DateFont { get => _dateFont; set { _dateFont = value; Invalidate(); } }

        private Font _textFont;
        [Category("Custom"), DefaultValue(typeof(Font), "Modern No. 20; 12pt; style=Italic")]
        public override Font Font { get => _textFont; set { _textFont = value; Invalidate(); } }


        private string _text;
        [Category("Custom"), DefaultValue("")]
        public override string Text { get => _text; set { _text = value; Invalidate(); } }

        public string _headerText;
        [Category("Custom"), DefaultValue(typeof(string), "Title")]
        public string HeaderText { get => _headerText; set { _headerText = value; Invalidate(); } }

        private DateTime? _date;
        [Category("Custom")]
        public DateTime? Date { get => _date; set { _date = value; Invalidate(); } }

        public string _dateFormat;
        [Category("Custom"), DefaultValue(typeof(string), "dd-MMM-yyyy")]
        public string DateFormat { get => _dateFormat; set { _dateFormat = value; Invalidate(); } }

        public int _iconMargin;
        [Category("Custom"), DefaultValue(5)]
        public int IconMargin { get => _iconMargin; set { _iconMargin = value; Invalidate(); } }


        private RectangleF headerRect, headerClientRect, bodyRect, textRect, dateRect, iconsRect;
        private readonly ThemeControlWithBackgroundProperties props;
        private readonly Dictionary<string, SideIconData> icons;
        public EventHandler<string> IconClick;


        public Postit()
        {
            this.props = new ThemeControlWithBackgroundProperties(this)
            {
                Invalidate = this.Invalidate,
                UpdateDataFromTheme = this.UpdateDataFromTheme,
                UseThemeColors = false,
            };
            icons = new Dictionary<string, SideIconData>();

            _headerColorDark = Color.LightYellow;
            _headerColorLight = Color.LightYellow;
            _headerTextColor = Color.Black;
            _headerFont = new Font("Arial", 13);
            _headerText = "Title";
            _headerSize = 53;

            _date = DateTime.Today;
            _dateFont = new Font("Comic Sans MS", 9);
            _dateFormat = "dd-MMM-yyyy";
            _dateColor = Color.Black;

            _textFont = new Font("Modern No. 20", 12, FontStyle.Italic);

            this.BackgroundColorDark = Color.LightYellow;
            this.BackgroundColorLight = Color.LightYellow;
            Size = new Size(250, 200);
            _iconMargin = 5;


            this.InternalPadding = new Padding(10, 10, 3, 3);
        }

        private void UpdateDataFromTheme()
        {
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColor;
            this.DisabledTextColor = this.ThemeScheme.DisabledControlTextColor;
            this.HighlightColor = this.ThemeScheme.ControlHighlightColor;
            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.FocusedBackgroundColorDark = this.ThemeScheme.ControlBackgroundColorLight;
            this.FocusedBackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
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


        private void UpdateRects()
        {
            headerRect = new RectangleF(0, 0, Width, _headerSize);
            headerClientRect = headerRect.ApplyPadding(Padding);
            bodyRect = new RectangleF(0, _headerSize, Width, Height - _headerSize);
            textRect = bodyRect.ApplyPadding(this.InternalPadding);

            string date = _date?.ToString(_dateFormat);
            if (date != null)
            {
                SizeF sizeDate = CreateGraphics().MeasureString(date, _dateFont);
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
                PointF iconPoint = iconDrawRect.GetAlignmentPoint(iconData.image.Size, ContentAlignment.BottomLeft);
                iconData.rect = new RectangleF(iconPoint, iconData.image.Size);
                iconDrawRect = iconDrawRect.ApplyPadding(iconData.image.Width + IconMargin, 0, 0, 0);
            }

        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            Brush brushHeader = new LinearGradientBrush(headerRect, _headerColorDark, _headerColorLight, LinearGradientMode.Vertical);
            g.FillRectangle(brushHeader, headerRect);

            Brush brushHeaderText = new SolidBrush(_headerTextColor);
            g.DrawString(_headerText, _headerFont, brushHeaderText, headerClientRect);

            Brush backgroundBrush = new SolidBrush(BackColor);
            g.FillRectangle(backgroundBrush, bodyRect);

            Brush brushText = new SolidBrush(ForeColor);
            g.Clip = new Region(textRect);
            g.Clip.Exclude(iconsRect);
            g.DrawString(_text, _textFont, brushText, textRect);
            g.ResetClip();

            string date = _date?.ToString(_dateFormat);
            if (date != null)
            {
                Brush brushDate = new SolidBrush(_dateColor);
                g.DrawString(date, _dateFont, brushDate, dateRect);
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
