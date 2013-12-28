// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System.Drawing;
using System.Windows.Forms;

namespace Krkadoni.SESE
{
    public partial class GeneralOptionsPage : OptionsPage
    {

        public GeneralOptionsPage()
        {
            InitializeComponent();
            cbLanguage.DataSource = AppSettings.DefInstance.Languages;
            cbLanguage.DataBindings.Add(new System.Windows.Forms.Binding("SelectedItem", AppSettings.DefInstance, "CurrentLanguage", true));
            cbLanguage.DataBindings.Add(new System.Windows.Forms.Binding("SelectedValue", new BindingSource(AppSettings.DefInstance,""), "CurrentLanguage.Name", true));
            ceDonated.DataBindings.Add(new System.Windows.Forms.Binding("Checked", AppSettings.DefInstance, "Donated", true));
            ceUpdates.DataBindings.Add(new System.Windows.Forms.Binding("Checked", AppSettings.DefInstance, "CheckUpdates", true));
            lbAuthorName.DataBindings.Add(new System.Windows.Forms.Binding("Text", AppSettings.DefInstance.Languages, "Author", true));
        }

        public override void OnSetActive()
        {

        }

        public override void OnApply()
        {

        }

    }
}
