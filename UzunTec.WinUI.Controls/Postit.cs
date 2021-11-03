using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class Postit : Control
    {
        
        private Color _headerColorDark;
        [Category("Custom"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorDark { get => this._headerColorDark; set { this._headerColorDark = value; this.Invalidate(); } }

        private Color _headerColorLight;
        [Category("Custom"), DefaultValue(typeof(Color), "LightYellow")]
        public Color HeaderColorLight { get => this._headerColorLight; set { this._headerColorLight = value; this.Invalidate(); } }

        private Color _headerTextColor;
        [Category("Custom"), DefaultValue(typeof(Color), "Black")]
        public Color HeaderTextColor { get => this._headerTextColor; set { this._headerTextColor = value; this.Invalidate(); } }

        private Color _dateColor;
        [Category("Custom"), DefaultValue(typeof(Color), "Black")]
        public Color DateColor { get => this._dateColor; set { this._dateColor = value; this.Invalidate(); } }

        public int _headerSize;
        [Category("Custom"), DefaultValue(40)]
        public int HeaderSize { get => this._headerSize; set { this._headerSize = Math.Max(value, 10); this.Invalidate(); } }


        private Font _headerFont;
        [Category("Custom"), DefaultValue(typeof(Font), "Arial; 14pt")]
        public Font HeaderFont { get => this._headerFont; set { this._headerFont = value; this.Invalidate(); } }

        private Font _dateFont;
        [Category("Custom"), DefaultValue(typeof(Font), "Comic Sans MS; 9pt; style=Bold")]
        public Font DateFont { get => this._dateFont; set { this._dateFont = value; this.Invalidate(); } }

        private Font _textFont;
        [Category("Custom"), DefaultValue(typeof(Font), "Modern No. 20; 12pt; style=Italic")]
        public override Font Font { get => this._textFont; set { this._textFont = value; this.Invalidate(); } }


        private string _text;
        [Category("Custom"), DefaultValue("")]
        public override string Text { get => this._text; set { this._text = value; this.Invalidate(); } }

        public string _headerText;
        [Category("Custom"), DefaultValue(typeof(string), "Title")]
        public string HeaderText { get => this._headerText; set { this._headerText = value; this.Invalidate(); } }

        private DateTime? _date;
        [Category("Custom")]
        public DateTime? Date { get => this._date; set { this._date = value; this.Invalidate(); } }

        public string _dateFormat;
        [Category("Custom"), DefaultValue(typeof(string), "dd-MMM-yyyy")]
        public string DateFormat { get => this._dateFormat; set { this._dateFormat = value; this.Invalidate(); } }


        public Postit()
        {
            this._headerColorDark = Color.LightYellow;
            this._headerColorLight = Color.LightYellow;
            this._headerTextColor = Color.Black;
            this._headerFont = new Font("Arial", 13);
            this._headerText = "Title";
            this._headerSize = 53;

            this._date = DateTime.Today;
            this._dateFont = new Font("Comic Sans MS", 9);
            this._dateFormat = "dd-MMM-yyyy";
            this._dateColor = Color.Black;

            this._textFont = new Font("Modern No. 20", 12, FontStyle.Italic);

            this.BackColor = Color.LightYellow;
            this.Size = new Size(250, 200);
            this.Padding = new Padding(10, 10, 3, 3);
            
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            Graphics g = pevent.Graphics;

            Rectangle rectHeader = new Rectangle(0, 0, this.Width, this._headerSize);
            Brush brushHeader = new LinearGradientBrush(rectHeader, this._headerColorDark, this._headerColorLight, LinearGradientMode.Vertical);
            g.FillRectangle(brushHeader, rectHeader);

            Brush brushHeaderText = new SolidBrush(this._headerTextColor);
            Rectangle headerClientRect = rectHeader.ApplyPadding(this.Padding);
            g.DrawString(this._headerText, this._headerFont, brushHeaderText, headerClientRect);

            Rectangle bodyRectangle = new Rectangle(0, this._headerSize, this.Width, this.Height - this._headerSize);
            Brush backgroundBrush = new SolidBrush(this.BackColor);
            g.FillRectangle(backgroundBrush, bodyRectangle);

            Rectangle bodyClientRectangle = bodyRectangle.ApplyPadding(this.Padding);
            Brush brushText = new SolidBrush(this.ForeColor);
            g.DrawString(this._text, this._textFont, brushText, bodyClientRectangle);

            try
            {
                string date = this._date?.ToString(this._dateFormat);
                if (date != null)
                {
                    SizeF sizeDate = g.MeasureString(date, this._dateFont);
                    Brush brushDate = new SolidBrush(this._dateColor);
                    RectangleF rectDate = new RectangleF(headerClientRect.Right - sizeDate.Width,
                                                            headerClientRect.Bottom - sizeDate.Height,
                                                            sizeDate.Width,
                                                            sizeDate.Height);
                    g.DrawString(date, this._dateFont, brushDate, rectDate);
                }
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Date Format Invalid", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //SizeF x = g.MeasureString(this.Text, this.Font, this.ClientRectangle.Width);
        }

    }
}
