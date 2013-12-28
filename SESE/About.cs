// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Windows.Forms;

namespace Krkadoni.SESE
{
    public partial class About : Form
    {
        private static About _defInstance;
        public static About DefInstance
        {
            get { return _defInstance ?? (_defInstance = new About()); }
            set { _defInstance = value; }
        }

        public About()
        {
            InitializeComponent();
            richTextBox.Text = Properties.Resources.license;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
