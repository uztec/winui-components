using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using UzunTec.WinUI.Utils;

namespace UzunTec.WinUI.Controls
{
    public class ThemeNumericTextBox : ThemeTextBox
    {
        private readonly List<NumberFormatInfo> numberFormatInfos = new List<NumberFormatInfo>
        {
            CultureInfo.CurrentCulture.NumberFormat,
            CultureInfo.InvariantCulture.NumberFormat,
        };

        private readonly List<char> decimalSeparators = new List<char>();
        private readonly List<char> groupSeparators = new List<char>();
        private readonly List<char> negativeSigns = new List<char>();
        private readonly List<char> positiveSigns = new List<char>();

        private readonly string invalidCharRegex; 

        public ThemeNumericTextBox()
        {
            string chars = @"\d";
            foreach (NumberFormatInfo numberFormatInfo in this.numberFormatInfos)
            {
                this.decimalSeparators.Add(numberFormatInfo.NumberDecimalSeparator[0]);
                this.groupSeparators.Add(numberFormatInfo.NumberGroupSeparator[0]);
                this.negativeSigns.Add(numberFormatInfo.NegativeSign[0]);
                this.positiveSigns.Add(numberFormatInfo.PositiveSign[0]);

                chars += @"\" + numberFormatInfo.NumberDecimalSeparator;
                chars += @"\" + numberFormatInfo.NumberGroupSeparator;
                chars += @"\" + numberFormatInfo.NegativeSign;
                chars += @"\" + numberFormatInfo.PositiveSign;
            }

            this.invalidCharRegex = $"[^{chars}]";
        }


        [DllImport("user32.dll")]
        private static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        public string Mask { get; set; }
        public bool Percentual { get; set; }

        private decimal _decimalValue = 0;
        public decimal DecimalValue { get => this._decimalValue; set { this._decimalValue = value; this.UpdateText(); } }

        public int IntValue { get => (int)this._decimalValue; set { this._decimalValue = value; this.UpdateText(); } } // TODO
        public double DoubleValue { get => (double)this._decimalValue; set { this._decimalValue = (decimal)value; this.UpdateText(); } } // TODO

        // Restricts the entry of characters to digits (including hex), the negative sign,
        // the decimal point, and editing keystrokes (backspace).
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (char.IsDigit(e.KeyChar)) { }    // Digit
            else if (this.decimalSeparators.Contains(e.KeyChar)) { }   // Decimal Separator
            else if (this.groupSeparators.Contains(e.KeyChar)) { }   // Decimal Separator
            else if (this.negativeSigns.Contains(e.KeyChar)) { }   // Negative Sign
            else if (this.positiveSigns.Contains(e.KeyChar)) { }   // Positive Sign
            else if (e.KeyChar == '\b') { } // Backspace
            else { e.Handled = true; }  // Ignore Anything else
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
            this._decimalValue = this.ParseValue(this.Text);
        }

        protected override void OnLeave(EventArgs e)
        {
            base.OnLeave(e);
            this.UpdateText();
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            this.UpdateText();
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);
            this.UpdateText();
        }

        private void UpdateText()
        {
            if (this._decimalValue != 0 || !string.IsNullOrWhiteSpace(this.Text))
            {
                if (!this.Focused && !string.IsNullOrEmpty(this.Mask))
                {
                    this.Text = this.DecimalValue.ToString(this.Mask);
                }
                else
                {
                    this.Text = (this.Percentual?this._decimalValue * 100 :this._decimalValue).ToString();
                }
            }
        }

        public decimal ParseValue(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return 0;
            }

            value = new Regex(this.invalidCharRegex).Replace(value, "");

            if (value != "")    // Protection
            {
                bool bNegative = (value[0] == '-');             // Ignore sinal if wasn't the first char

                int commaIndex = -1;
                foreach (char decimalSeparator in this.decimalSeparators)
                {
                    commaIndex = Math.Max(commaIndex, value.LastIndexOf(decimalSeparator));
                }
                int decimalPlaces = (commaIndex > 0 ? value.Length - 1 - commaIndex : 0) + (this.Percentual ? 2 : 0);

                value = new Regex(@"[^\d]").Replace(value, "");
                if (decimal.TryParse(value, out decimal output))
                {
                    output /= (int) Math.Pow(10, decimalPlaces);
                    return (bNegative) ? -output : output;
                }
            }
            return 0;
        }

        #region Copy And Paste

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Enter:
                    //if (this._TabOnEnter)
                    //  return base.ProcessCmdKey(ref msg, Keys.Tab);
                    return true;
                case Keys.Control | Keys.V:
                case Keys.Shift | Keys.Insert:
                    PostMessage(this.Handle, Win32ApiConstants.WM_PASTE, 0, 0);
                    return true;
                case Keys.Control | Keys.C:
                case Keys.Control | Keys.Insert:
                    PostMessage(this.Handle, Win32ApiConstants.WM_COPY, 0, 0);
                    return true;
                case Keys.Control | Keys.X:
                case Keys.Shift | Keys.Delete:
                    PostMessage(this.Handle, Win32ApiConstants.WM_CUT, 0, 0);
                    return true;
                case Keys.Control | Keys.Delete:
                    PostMessage(this.Handle, Win32ApiConstants.WM_CLEAR, 0, 0);
                    return true;
                case Keys.Control | Keys.Z:
                    PostMessage(this.Handle, Win32ApiConstants.WM_UNDO, 0, 0);
                    return true;
                default:
                    return base.ProcessCmdKey(ref msg, keyData);
            }
        }
        

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case Win32ApiConstants.WM_PASTE:  // On Paste
                    if (Clipboard.ContainsText())
                    {
                        this.SelectedText = this.ParseValue(Clipboard.GetText()).ToString();
                    }
                    break;
                case Win32ApiConstants.WM_CUT:
                case Win32ApiConstants.WM_COPY:
                case Win32ApiConstants.WM_CLEAR:
                    base.WndProc(ref m);
                    break;

                case Win32ApiConstants.WM_UNDO:
                case Win32ApiConstants.EM_UNDO:
                    m.Result = new IntPtr(1);
                    break;
                case Win32ApiConstants.EM_CANUNDO:
                    m.Result = new IntPtr(0);
                    break;
                default:
                    base.WndProc(ref m);
                    break;
            }
        }
        #endregion
    }
}