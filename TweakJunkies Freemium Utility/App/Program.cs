using System;
using System.Windows.Forms;

namespace TweakJunkies_Freemium_Utility
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\TweakJunkiesFreemiumUtility");
            if (key != null && key.GetValue("FirstRun") != null) {
                Application.Run(new Main());
            }
            else {
                Application.Run(new Loading());
            }
        }
    }
}
