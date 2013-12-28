// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
//Permission is hereby granted, free of charge, to any person obtaining a copy
//of this software and associated documentation files (the "Software"), to deal
//in the Software without restriction, including without limitation the rights
//to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
//copies of the Software, and to permit persons to whom the Software is
//furnished to do so, subject to the following conditions:

//The above copyright notice and this permission notice shall be included in
//all copies or substantial portions of the Software.

//THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
//IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
//FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
//AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
//LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
//OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
//THE SOFTWARE.

using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Krkadoni.SESE
{
    public partial class OptionsDialog : Form
    {

        private static OptionsDialog _defInstance;
        public static OptionsDialog DefInstance
        {
            get { return _defInstance ?? (_defInstance = new OptionsDialog()); }
            set { _defInstance = value; }
        }

        private OptionsPage _activePage;
        public IList<OptionsPage> Pages { get; private set; }

        public OptionsDialog()
        {
            Pages = new List<OptionsPage>();
            InitializeComponent();
            panelPage.Validating += panelPage_Validating;
        }

        private void OptionsDialog_Load(object sender, System.EventArgs e)
        {

            //var resources = new System.Resources.ResourceManager(typeof(OptionsDialog));
            //var defaultImage = (Bitmap)resources.GetObject("run");

            AddPageControls();
            SelectFirstListItem();
        }

        private void SelectFirstListItem()
        {
            if (listView.Items.Count != 0)
                listView.Items[0].Selected = true;
        }

        private void AddPageControls()
        {
            var maxPageSize = panelPage.Size;
            foreach (var page in Pages)
            {
                AddPage(page, ref maxPageSize);
            }

            SizeToFit(maxPageSize);
            CenterToParent();
        }

        private void SizeToFit(Size maxPageSize)
        {
            var newSize = new Size();
            newSize.Width = maxPageSize.Width + (Width - panelPage.Width);
            newSize.Height = maxPageSize.Height + (Height - panelPage.Height);

            Size = newSize;
        }

        private void AddPage(OptionsPage page, ref Size maxPageSize)
        {
            panelPage.Controls.Add(page);

            AddListItemForPage(page);

            // Adjust to fit the largest child page.
            if (page.Width > maxPageSize.Width)
                maxPageSize.Width = page.Width;
            if (page.Height > maxPageSize.Height)
                maxPageSize.Height = page.Height;

            // Set page.Dock *after* looking at its size.
            page.Dock = DockStyle.Fill;
            page.Visible = false;
        }

        private void AddListItemForPage(OptionsPage page)
        {
            int imageIndex = 0;

            var image = page.Image;
            if (image != null)
            {
                imageList.Images.Add(image);
                imageIndex = imageList.Images.Count - 1;
            }

            var item = new ListViewItem(page.Title, imageIndex);
            item.Tag = page;

            listView.Items.Add(item);
        }

        private void SelectPage(OptionsPage page)
        {
            for (int i = 0; i <= listView.Items.Count -1; i++)
            {
                if (listView.Items[i].Tag == page)
                {
                    listView.FocusedItem = listView.Items[i];
                    listView.Items[i].Selected = true;
                    break;
                }
            }
        }

        private void listView_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if (_activePage != null)
                _activePage.Visible = false;

            if (listView.SelectedItems.Count != 0)
            {
                var selectedItem = listView.SelectedItems[0];
                var page = (OptionsPage)selectedItem.Tag;
                _activePage = page;
            }

            if (_activePage != null)
            {
                _activePage.Visible = true;
                _activePage.OnSetActive();
            }
        }

        private void btnOk_Click(object sender, System.EventArgs e)
        {
            AppSettings.DefInstance.EndEdit();
            AppSettings.Save();
            foreach (var settingsPage in Pages)
            {
                settingsPage.OnApply();
            }
            Close();
        }

        private void btnCancel_Click(object sender, System.EventArgs e)
        {
            AppSettings.DefInstance.CancelEdit();
            Close();
        }

        private void btnApply_Click(object sender, System.EventArgs e)
        {
            AppSettings.DefInstance.EndEdit();
            AppSettings.Save();
            foreach (var settingsPage in Pages)
            {
                settingsPage.OnApply();
            }
        }

        private void panelPage_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_activePage != null)
                e.Cancel = !_activePage.ValidateChildren();
        }

        private void OptionsDialog_FormClosed(object sender, FormClosedEventArgs e)
        {
            AppSettings.DefInstance.CancelEdit();
            //Dispose();
        }
    
    }
}
