// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace Krkadoni.SESE
{
    public class PortTextBox : TextBox
    {

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            NumberFormatInfo numberFormatInfo = CultureInfo.CurrentCulture.NumberFormat;

            if (Char.IsDigit(e.KeyChar))
            {
                if (Text.Length == 5)
                    e.Handled = true;
                else if (Text.Length == 0 && e.KeyChar == '0')
                {
                    e.Handled = true;
                }
                else if (Int32.Parse(Text + e.KeyChar.ToString(numberFormatInfo)) > 65536)
                {
                    e.Handled = true;
                }
            }
            else if (e.KeyChar == '\b')
            {

            }
            else
            {
                e.Handled = true;
            }
        }

        public int IntValue
        {
            get
            {
                return Int32.Parse(Text);
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

