// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Krkadoni.SESE
{
    public class PositionTextBox : TextBox
    {
        // Restricts the entry of characters to digits (including hex), the negative sign, 
        // the decimal point, and editing keystrokes (backspace). 
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            var numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;
            var negativeSign = numberFormatInfo.NegativeSign;
            var keyInput = e.KeyChar.ToString(numberFormatInfo);
            var position = SelectionStart;
            if (Char.IsDigit(e.KeyChar))
            {
                if (Math.Abs(IntValue) >= 180)
                {
                    e.Handled = true;
                }
                if (e.KeyChar.ToString(CultureInfo.InvariantCulture) == "0" && Text.TrimStart('-').Length == 0)
                {
                    e.Handled = true;
                }
                if (Text.IndexOf('-') > -1 && position < 1)
                {
                    e.Handled = true;
                }
            }
            else if (keyInput.Equals(negativeSign))
            {

                if (Text.IndexOf("-", System.StringComparison.InvariantCulture) > -1)
                    e.Handled = true;
                else if (Text == @"0")
                {
                    Text = @"-";
                    SelectionStart = 1;
                    e.Handled = true;
                }
                else if (position > 0)
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                if (this.Text.Length == 0 || this.Text == @"-") return 0;
                return Int32.Parse(this.Text);
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(Text))
            {
                e.Cancel = true;
                Text = @"0";
            }
            base.OnValidating(e);
        }



    }
}
