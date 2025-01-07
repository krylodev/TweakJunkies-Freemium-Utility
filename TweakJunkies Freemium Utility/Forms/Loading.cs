using System;
using System.IO;
using System.Net;
using System.IO.Compression;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Threading.Tasks;
using System.Net.Http;

namespace TweakJunkies_Freemium_Utility
{
    public partial class Loading : Form
    {
        private const string DropboxUrl = "https://www.dropbox.com/scl/fi/wml2t8z1ir5ab27uq44g1/RW_Everything_Portable.zip?rlkey=o7hygewdnombjiuqi7tqurruq&st=ntch4jt4&dl=1";
        private const string ExtractFolderPath = @"C:\Program Files\RW Everything Portable";
        private const string RegistryKeyPath = @"Software\TweakJunkiesFreemiumUtility";
        private const string RegistryFirstRunValue = "FirstRun";

        public Loading()
        {
            InitializeComponent();
        }

        private void guna2ControlBox1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private async void Loading_Load(object sender, EventArgs e)
        {
            this.DoubleBuffered = true;
            MainPanel.Visible = false;
            this.Invalidate();
            guna2Transition1.ShowSync(MainPanel);
            MainPanel.Visible = true;
            this.Invalidate();
            await Task.Delay(3000);
            if (IsFirstRun()) {
                await DownloadAndExtractFiles();
                await Task.Delay(2000);
                ShowRebootDialog();
            }
            else {
                Application.Run(new Main());
            }
        }

        private bool IsFirstRun()
        {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(RegistryKeyPath);
            return key == null || key.GetValue(RegistryFirstRunValue) == null;
        }

        private void MarkFirstRunCompleted()
        {
            var key = Microsoft.Win32.Registry.CurrentUser.CreateSubKey(RegistryKeyPath);
            key.SetValue(RegistryFirstRunValue, "False");
        }

        private async Task DownloadAndExtractFiles()
        {
            string zipFilePath = Path.Combine(Path.GetTempPath(), "RW_Everything_Portable.zip");

            using (HttpClient client = new HttpClient()) {
                using (var response = await client.GetAsync(DropboxUrl, HttpCompletionOption.ResponseHeadersRead)) {
                    response.EnsureSuccessStatusCode();
                    using (var fileStream = new FileStream(zipFilePath, FileMode.Create, FileAccess.Write, FileShare.None)) {
                        await response.Content.CopyToAsync(fileStream);
                    }
                }
            }

            if (!Directory.Exists(ExtractFolderPath)) {
                Directory.CreateDirectory(ExtractFolderPath);
            }

            await Task.Run(() => ZipFile.ExtractToDirectory(zipFilePath, ExtractFolderPath));
            File.Delete(zipFilePath);
        }

        private void ShowRebootDialog()
        {
            Guna.UI2.WinForms.Guna2MessageDialog dialog = new Guna.UI2.WinForms.Guna2MessageDialog
            {
                Text = "A Reboot Is Required to Proceed, Do You want to restart your system now?",
                Buttons = Guna.UI2.WinForms.MessageDialogButtons.OK,
                Caption = "TweakJunkies",
                Icon = Guna.UI2.WinForms.MessageDialogIcon.Information,
                Parent = this,
                Style = Guna.UI2.WinForms.MessageDialogStyle.Dark
            };

            DialogResult result = dialog.Show();

            if (result == DialogResult.OK) {
                MarkFirstRunCompleted();
                System.Diagnostics.Process.Start("shutdown", "/r /t 0");
            }
            else {
                MarkFirstRunCompleted();
            }
        }
    }
}
