// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT

using System;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Krkadoni.EnigmaSettings;

namespace Krkadoni.SESE
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                AppSettings.Load();
            }
            catch (Exception ex)
            {
                AppSettings.Log.Error("There was an error while loading application settings.", ex);
                MessageBox.Show(String.Format("There was an error while loading application settings.{0}{1}", Environment.NewLine, ex.Message));
            }
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppSettings.DefInstance.CurrentLanguage.Culture);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(SplitList.DefInstance);
        }
    }
}
