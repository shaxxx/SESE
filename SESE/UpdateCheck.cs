using System;
using System.Globalization;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using System.Windows.Forms;
using Krkadoni.SESE.Properties;


namespace Krkadoni.SESE
{
    /// <summary>
    /// Checks if new version is available
    /// </summary>
    public static class UpdateCheck
    {
        private static readonly object LockObject = new Object();

        static string Check()
        {
            lock (LockObject)
            {
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(AppSettings.DefInstance.CurrentLanguage.Culture);
                if (AppSettings.DefInstance.CheckUpdates)
                {
                    string url = AppSettings.IsRunningOnMono() ? "http://www.krkadoni.com/SESEMono.txt" : "http://www.krkadoni.com/SESE.txt";
                    var request = (HttpWebRequest)WebRequest.Create(url);
                    request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)";
                    request.Timeout = 10000;

                    var response = request.GetResponse();
                    var responseStream = response.GetResponseStream();

                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            var result = reader.ReadToEnd();
                            AppSettings.Log.DebugFormat("Version check URL {0} returned {1}{2}", url, Environment.NewLine, result);
                            return result;
                        }
                    }
                }
                return null;
            }
        }

        public static void CheckAsync(AsyncCallback callback)
        {
            AppSettings.Log.DebugFormat("Initialized CheckAsync");
            new Func<string>(Check).BeginInvoke(callback, null);
            AppSettings.Log.DebugFormat("CheckAsync finished");
        }

        public static void CheckAsyncCallback(IAsyncResult ar)
        {
            AppSettings.Log.Debug("CheckAsyncCallback initialized");

            // Retrieve the delegate.
            AsyncResult result = (AsyncResult)ar;
            var caller = (Func<string>)result.AsyncDelegate;
            try
            {
                string updateInfo = (string)caller.EndInvoke(ar);
                if (updateInfo == null)
                {
                    AppSettings.Log.Debug("Update check returned no results.");
                    return;
                }
                ParseCheckResult(updateInfo);
            }
            catch (Exception ex)
            {
                AppSettings.Log.Error(String.Format("Failed to check for updates.{0}", Environment.NewLine), ex);
            }
            AppSettings.Log.Debug("CheckAsyncCallback finished");
        }

        public static void ParseCheckResult(string updateInfo)
        {
            if (updateInfo != null)
            {
                AppSettings.Log.DebugFormat("ParseCheckResult initialized with result {0}{1}", Environment.NewLine, updateInfo);
                try
                {
                    //check if there's new version available
                    if (updateInfo.ToLower().StartsWith(Application.ProductVersion))
                    {
                        //version number is the same as the one in update file, nothing to do
                        AppSettings.Log.Debug("Current application version seems to be up to date.");
                        return;
                    }
                    if (updateInfo.IndexOf("http", System.StringComparison.InvariantCultureIgnoreCase) >-1)
                    {
                        if (MessageBox.Show(Resources.QUESTION_NEW_VERSION, Resources.QUESTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            string url = updateInfo.Substring(updateInfo.IndexOf("http", System.StringComparison.InvariantCultureIgnoreCase)).Trim();
                            AppSettings.Log.DebugFormat("Starting new process for URL {0} found from update info.", url);
                            System.Diagnostics.Process.Start(url);
                            AppSettings.Log.DebugFormat("New process started for URL {0} found from update info.", url);
                        }
                    }
                    else
                    {
                        AppSettings.Log.Debug("No update URL found. Just showing information dialog.");
                        MessageBox.Show(String.Format(Properties.Resources.INFO_NEW_VERSION, Environment.NewLine,"http://www.krkadoni.com"),string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                catch (Exception ex)
                {
                    AppSettings.Log.Error(String.Format("Failed to parse update info results.{0}", Environment.NewLine), ex);
                }
                AppSettings.Log.Debug("ParseCheckResult finished");
            }
            else
            {
                AppSettings.Log.Debug("ParseCheckResult initialized with updateInfo = null");
            }
        }

    }
}
