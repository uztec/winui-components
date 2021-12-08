using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using UzunTec.WinUI.Controls.Helpers;
using UzunTec.WinUI.Controls.Interfaces;
using UzunTec.WinUI.Controls.InternalContracts;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeComboBox : ComboBox, IThemeControlWithHint
    {
        public event EventHandler PrependIconClick;
        public event EventHandler AppendIconClick;

        [Browsable(false), ReadOnly(true)]
        public new Color BackColor { get => this.BackgroundColorDark; set { this.BackgroundColorDark = value; } }

        [Browsable(false), ReadOnly(true)]
        public new Color ForeColor { get => this.TextColor; set { this.TextColor = value; } }

        [Browsable(false), ReadOnly(true)]
        public new Size MinimumSize { get; set; }


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

        [Category("Theme"), DefaultValue(typeof(Padding), "5; 5; 5; 5;")]
        public Padding InternalPadding { get => this.props.InternalPadding; set => this.props.InternalPadding = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color HintColor { get => this.props.HintColor; set => this.props.HintColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font HintFont { get => this.props.HintFont; set => this.props.HintFont = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color PlaceholderColor { get => this.props.PlaceholderColor; set => this.props.PlaceholderColor = value; }

        [Category("Theme"), DefaultValue(typeof(Font), "Segoe UI; 15pt")]
        public Font PlaceholderFont { get => this.props.PlaceholderFont; set => this.props.PlaceholderFont = value; }

        [Category("Theme"), DefaultValue(typeof(Color), "Black")]
        public Color DisabledHintColor { get => this.props.DisabledHintColor; set => this.props.DisabledHintColor = value; }


        #endregion

        // TODO: 
        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color SelectionColorDark { get; set; }

        [Category("Theme"), DefaultValue(typeof(Color), "Control")]
        public Color SelectionColorLight { get; set; }



        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string PlaceholderHintText
        {
            get => _placeholderHintText;
            set
            {
                _placeholderHintText = value;
                hasHint = this._showHint && !string.IsNullOrEmpty(_placeholderHintText);
                Invalidate();
            }
        }
        private string _placeholderHintText = string.Empty;

        [Category("Z-Custom"), DefaultValue(true)]
        public bool ShowHint
        {
            get => _showHint;
            set
            {
                _showHint = value;
                hasHint = this._showHint && !string.IsNullOrEmpty(_placeholderHintText);
                Invalidate();
            }
        }
        private bool _showHint;

        //
        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string Prefix
        {
            get => _prefixText;
            set { _prefixText = value; this.UpdateRects(); this.Invalidate(); }
        }
        private string _prefixText = string.Empty;

        [Category("Z-Custom"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font PrefixFont
        {
            get => _prefixFont;
            set { _prefixFont = value; this.UpdateRects(); this.Invalidate(); }
        }
        private Font _prefixFont;

        [Category("Z-Custom"), DefaultValue(typeof(string), "")]
        public string Suffix
        {
            get => _suffixText;
            set { _suffixText = value; this.UpdateRects(); this.Invalidate(); }
        }
        private string _suffixText = string.Empty;

        [Category("Z-Custom"), DefaultValue(typeof(Font), "Segoe UI; 6pt")]
        public Font SuffixFont
        {
            get => _suffixFont;
            set { _suffixFont = value; this.UpdateRects(); this.Invalidate(); }
        }
        private Font _suffixFont;

        [Category("Z-Custom"), DefaultValue(typeof(Color), "Black")]
        public Color PrefixSuffixTextColor
        {
            get => _prefixSuffixTextColor;
            set { _prefixSuffixTextColor = value; Invalidate(); }
        }
        private Color _prefixSuffixTextColor;

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image PrependIcon
        {
            get => this.prependIconData.image;
            set { this.prependIconData.image = value; this.UpdateRects(); this.Invalidate(); }
        }

        [Category("Z-Custom"), DefaultValue(typeof(float), "5")]
        public float PrependIconMargin
        {
            get => this._prependIconMargin;
            set { this._prependIconMargin = value; this.UpdateRects(); this.Invalidate(); }
        }
        private float _prependIconMargin;

        [Category("Z-Custom"), DefaultValue(typeof(Image), "")]
        public Image AppendIcon
        {
            get => this.appendIconData.image;
            set { this.appendIconData.image = value; this.UpdateRects(); this.Invalidate(); }
        }

        [Category("Z-Custom"), DefaultValue(typeof(float), "5")]
        public float AppendIconMargin
        {
            get => this._appendIconMargin;
            set { this._appendIconMargin = value; this.UpdateRects(); this.Invalidate(); }
        }
        private float _appendIconMargin;

        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "BottomLeft")]
        public ContentAlignment TextAlign { get => this._textAlign; set { this._textAlign = value; this.Invalidate(); } }
        private ContentAlignment _textAlign;
       
        [Category("Z-Custom"), DefaultValue(typeof(ContentAlignment), "MiddleLeft")]
        public ContentAlignment ItemTextAlign { get => this._itemTextAlign; set { this._itemTextAlign = value; this.Invalidate(); } }
        private ContentAlignment _itemTextAlign;


        private RectangleF textRect, hintRect, prefixRect, suffixRect, triangleRect;
        private bool hasHint, hasPrefix, hasSuffix;
        private readonly SideIconData prependIconData = new SideIconData();
        private readonly SideIconData appendIconData = new SideIconData();
        private readonly ThemeControlWithHintProperties props;

        public ThemeComboBox()
        {
            this.props = new ThemeControlWithHintProperties(this)
            {
                Invalidate = this.Invalidate,
                UpdateRects = this.UpdateRects,
                UpdateDataFromTheme = this.UpdateDataFromTheme,
            };
        
            // Control Defaults
            _placeholderHintText = "";
            _showHint = true;
            this._textAlign = ContentAlignment.BottomLeft;
            this._itemTextAlign = ContentAlignment.MiddleLeft;
            Size = new Size(200, 50);
            ItemHeight = 44;
        }

        private void UpdateDataFromTheme()
        {
            // Theme
            this.Font = this.ThemeScheme.ControlTextFont;
            this.TextColor = this.ThemeScheme.ControlTextColor;
            this.DisabledTextColor = this.ThemeScheme.DisabledControlTextColor;

            this.FocusedBackgroundColorDark = this.ThemeScheme.ControlBackgroundColorLight;
            this.FocusedBackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;

            this.HintColor = this.ThemeScheme.ControlHintTextColor;
            this.HintFont = this.ThemeScheme.ControlHintFont;
            this.DisabledHintColor = this.ThemeScheme.DisableControlHintTextColor;

            this.HighlightColor = this.ThemeScheme.ControlHighlightColor;
            this.SelectionColorDark = Color.FromArgb(80, this.HighlightColor);
            this.SelectionColorLight = Color.FromArgb(50, this.HighlightColor);

            this.PlaceholderFont = this.ThemeScheme.ControlPlaceholderFont;
            this.PlaceholderColor = this.ThemeScheme.ControlPlaceholderColor;

            this.BackgroundColorDark = this.ThemeScheme.ControlBackgroundColorDark;
            this.BackgroundColorLight = this.ThemeScheme.ControlBackgroundColorLight;
            this.DisabledBackgroundColorDark = this.ThemeScheme.DisabledControlBackgroundColorDark;
            this.DisabledBackgroundColorLight = this.ThemeScheme.DisabledControlBackgroundColorLight;

            _prefixFont = ThemeScheme.ControlHintFont;
            _suffixFont = ThemeScheme.ControlHintFont;
            _prefixSuffixTextColor = ThemeScheme.PrefixSuffixTextColor;
            this._prependIconMargin = 5;
            this._appendIconMargin = 5;

            this.InternalPadding = this.ThemeScheme.HintControlInternalPadding;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            base.AutoSize = false;
            this.SetStyle(ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint | ControlStyles.OptimizedDoubleBuffer | ControlStyles.ResizeRedraw, true);

            SetStyle(ControlStyles.OptimizedDoubleBuffer | ControlStyles.DoubleBuffer | ControlStyles.AllPaintingInWmPaint | ControlStyles.UserPaint, true);

            this.UpdateRects();
            
            LostFocus += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            GotFocus += (sender, args) => this.Invalidate();
            MouseEnter += (sender, args) => { MouseHovered = true; this.Invalidate(); };
            MouseLeave += (sender, args) => { MouseHovered = false; this.Invalidate(); };
            SizeChanged += (sender, args) => { UpdateRects(); };

            MeasureItem += CustomMeasureItem;
            DrawItem += CustomDrawItem;
            DropDownStyle = ComboBoxStyle.DropDownList;
            DrawMode = DrawMode.OwnerDrawVariable;

        }

        private void CustomMeasureItem(object sender, MeasureItemEventArgs e)
        {
            e.ItemHeight = this.Height - 7;
        }

        private void CustomDrawItem(object sender, DrawItemEventArgs e)
        {
            if (e.Index < 0 || e.Index >= Items.Count || !Focused)
            {
                return;
            }

            Graphics g = e.Graphics;

            // Background
            Brush backgroundBrush = e.State.HasFlag(DrawItemState.Focus) ? (Brush)new LinearGradientBrush(e.Bounds, this.SelectionColorDark, this.SelectionColorLight, LinearGradientMode.Vertical)
                    : new SolidBrush(this.BackgroundColorLight);
            g.FillRectangle(backgroundBrush, e.Bounds);

            string text = this.GetItemText(this.Items[e.Index]);
            g.DrawText(text, this.Font, new SolidBrush(this.TextColor), e.Bounds, this._itemTextAlign);
            e.DrawFocusRectangle();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            //this.UpdateRects();
        }

        private void UpdateRects()
        {
            if (!this.Created && !DesignMode)
            {
                return;
            }
            hasHint = _showHint && !string.IsNullOrEmpty(_placeholderHintText);
            hasPrefix = !string.IsNullOrEmpty(_prefixText);
            hasSuffix = !string.IsNullOrEmpty(_suffixText);

            Graphics g = CreateGraphics();

            const float TRIANGLE_RECTANGLE_WIDTH = 30;

            this.triangleRect = new RectangleF(this.ClientRectangle.Width - TRIANGLE_RECTANGLE_WIDTH, 0, TRIANGLE_RECTANGLE_WIDTH, this.ClientRectangle.Height);

            this.textRect = this.ClientRectangle.ToRectF().ApplyPadding(this.InternalPadding);
            this.textRect = this.textRect.ApplyPadding(0, 0, triangleRect.Width, this.ClientRectangle.Height - this.GetBottomLineRect().Y);

            float hintOffset = 0;

            if (this.prependIconData.image != null)
            {
                float iconRectWidth = this.prependIconData.image.Width + (2 * this._prependIconMargin);
                this.prependIconData.rect = new RectangleF(0, 0, iconRectWidth, this.ClientRectangle.Height);
                hintOffset = iconRectWidth;
                this.textRect = this.textRect.ApplyPadding(iconRectWidth, 0, 0, 0);
            }

            if (this.appendIconData.image != null)
            {
                float iconRectWidth = (this.appendIconData.image.Width + (2 * this._appendIconMargin));
                this.appendIconData.rect = new RectangleF(this.ClientRectangle.Width - iconRectWidth, 0, iconRectWidth, this.ClientRectangle.Height);
                this.triangleRect = new RectangleF(this.ClientRectangle.Width - TRIANGLE_RECTANGLE_WIDTH - iconRectWidth, 0, TRIANGLE_RECTANGLE_WIDTH, this.ClientRectangle.Height);
                this.textRect = this.textRect.ApplyPadding(0, 0, appendIconData.rect.Width, 0);
            }

            if (this.hasHint)
            {
                this.hintRect = this.GetHintRect(g, new PointF(hintOffset, 0));
                this.textRect = this.textRect.ApplyPadding(0, this.hintRect.Height, 0, 0);
            }


            if (hasPrefix)
            {
                SizeF prefixSize = g.MeasureString(_prefixText, _prefixFont, ClientRectangle.Width);
                PointF prefixLcation = new PointF(this.textRect.Left, this.textRect.Bottom - prefixSize.Height);
                this.prefixRect = new RectangleF(prefixLcation, prefixSize);
                this.textRect = this.textRect.ApplyPadding(prefixRect.Width, 0, 0, 0);
            }

            if (hasSuffix)
            {
                SizeF suffixSize = g.MeasureString(_suffixText, _suffixFont, ClientRectangle.Width);
                PointF suffixLcation = new PointF(this.textRect.Right - suffixSize.Width, this.textRect.Bottom - suffixSize.Height);
                this.suffixRect = new RectangleF(suffixLcation, suffixSize);
                this.textRect = this.textRect.ApplyPadding(0, 0, suffixSize.Width, 0);
            }

            this.Invalidate();
        }


        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.Clear(Parent.BackColor);

            g.FillBackground(this);
            g.DrawBottomLine(this);

            if (this.hasHint && (Focused || !string.IsNullOrWhiteSpace(this.Text)))
            {
                g.DrawHint(this, hintRect);
                //g.FillRectangle(Brushes.BurlyWood, hintRect);
            }

            Brush textBrush = ThemeSchemeManager.Instance.GetTextBrush(this);
            if (!string.IsNullOrWhiteSpace(this.Text))
            {
                g.Clip = new Region(this.textRect);
                g.DrawText(this.Text, this.Font, textBrush, this.textRect, this._textAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.Blue, textRect);
            }
            else if (!string.IsNullOrWhiteSpace(this._placeholderHintText) && !Focused)
            {
                Brush placeHolderBrush = Enabled ? new SolidBrush(this.PlaceholderColor) : textBrush;
                g.Clip = new Region(this.textRect);
                g.DrawText(this._placeholderHintText, this.PlaceholderFont, placeHolderBrush, this.textRect, this._textAlign);
                g.ResetClip();
                //g.FillRectangle(Brushes.Blue, textRect);
            }

            g.DrawTriangle(this, triangleRect);
            //g.FillRectangle(Brushes.Red, triangleRect);

            if (this.hasPrefix)
            {
                Brush prefixSuffixBrush = new SolidBrush(this._prefixSuffixTextColor);
                g.Clip = new Region(this.prefixRect);
                g.DrawString(this._prefixText, this._prefixFont, prefixSuffixBrush, this.prefixRect);
                g.ResetClip();
                //g.FillRectangle(Brushes.Yellow, prefixRect);
            }

            if (this.hasSuffix)
            {
                Brush prefixSuffixBrush = new SolidBrush(this._prefixSuffixTextColor);
                g.Clip = new Region(this.suffixRect);
                g.DrawString(this._suffixText, this._suffixFont, prefixSuffixBrush, this.suffixRect);
                g.ResetClip();
                //g.FillRectangle(Brushes.Chocolate, suffixRect);
            }

            if (this.prependIconData.image != null)
            {
                g.DrawImage(this.prependIconData.image, this.prependIconData.rect.ShrinkToSize(this.prependIconData.image.Size, ContentAlignment.MiddleCenter));
                //g.FillRectangle(Brushes.Green, prependIconData.rect);
            }

            if (this.appendIconData.image != null)
            {
                g.DrawImage(this.appendIconData.image, this.appendIconData.rect.ShrinkToSize(this.appendIconData.image.Size, ContentAlignment.MiddleCenter));
                //g.FillRectangle(Brushes.Green, appendIconData.rect);
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            base.OnTextChanged(e);
            Invalidate();
        }


    }
}
