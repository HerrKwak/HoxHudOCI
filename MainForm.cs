using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HoxHudOneClickInstall
{
    public partial class HoxHudOCI : Form
    {
        //TODO: make code less "hacky"
        //TODO: add more comments
        //TODO: Actually have proper io stuff (respond to catches)

        public HoxHudOCI()
        {
            InitializeComponent();
        }

        private static Random random = new Random((int)DateTime.Now.TimeOfDay.TotalSeconds);
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        /// <summary>
        /// Reads the game path from the uninstaller location
        /// </summary>
        /// <returns>the game installation directory or null</returns>
        static string getPayday2Dir()
        {

            RegistryKey localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64).OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218620", false);

            if (Environment.Is64BitOperatingSystem)
                localKey = RegistryHelper.OpenBaseKey(RegistryHive.LocalMachine, RegistryHelper.RegistryHiveType.X64);
            else
                localKey = RegistryHelper.OpenBaseKey(RegistryHive.LocalMachine, RegistryHelper.RegistryHiveType.X86);
            try
            {
                return localKey.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218620", false).GetValue("InstallLocation").ToString();
            }
            catch (Exception)
            {
                try
                {
                    return localKey.OpenSubKey(@"Software\WOW6432Node\Microsoft\Windows\CurrentVersion\Uninstall\Steam App 218620", false).GetValue("InstallLocation").ToString();
                }
                catch(Exception)
                {
                    return null;
                }
            }
        }

        static bool isPayday2Installed()
        {
            return Convert.ToBoolean(Registry.GetValue(@"HKEY_CURRENT_USER\Software\Valve\Steam\Apps\218620", "Installed", "0"));
        }

        /// <summary>
        /// A random Filename for the downloaded file + folder to unpack to
        /// </summary>
        string DownloadFile = "";

        /// <summary>
        /// Initiate the Downloading (and unpacking)
        /// of the master branch
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void InstallBtn_Click(object sender, EventArgs e)
        {
            InstallBtn.Text = "Initializing...";
            InstallBtn.Enabled = false;
            DownloadFile = RandomString(5);
            while (File.Exists(DownloadFile))
                DownloadFile = RandomString(5);

            DoCleanup();

            InstallBtn.Text = "Downloading...";

            WebClient client = new WebClient();
            try
            {
                client.DownloadFileCompleted += Client_DownloadFileCompleted;
                client.DownloadProgressChanged += Client_DownloadProgressChanged;
                this.Size = new Size(278, 262);
                client.DownloadFileAsync(new Uri("https://github.com/HoxHud/HoxHud-bin/archive/master.zip"), DownloadFile);

            }
            catch(Exception)
            {
                if (MessageBox.Show("Failed Downloading newest Release!", "ERROR", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error) == DialogResult.Retry)
                    InstallBtn_Click(null, null);
                else
                    Application.Exit();
            }
        }

        /// <summary>
        /// Simple Progressbar updater to show ~some~ response
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">Download Progress</param>
        private void Client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            downloadProgressBar.Value = e.ProgressPercentage;
        }

        /// <summary>
        /// Remove all the files created by the installer
        /// </summary>
        private void DoCleanup()
        {
            InstallBtn.Text = "Cleaning up...";

            if (Directory.Exists("../HoxHud-bin-master/") && !Directory.Exists("HoxHud-bin-master/"))
                Directory.SetCurrentDirectory("..");
            try
            {
                foreach (string file in Directory.GetFiles("HoxHud-bin-master/", "*", SearchOption.AllDirectories))
                {
                    File.Delete(file);
                }
            }
            catch (Exception) { }
            
            try
            {
                string[] dirs = Directory.GetDirectories("HoxHud-bin-master/", "*", SearchOption.AllDirectories);

                Array.Sort(dirs, (x, y) => y.Length.CompareTo(x.Length));

                foreach (string dir in dirs)
                {
                    Directory.Delete(dir);
                }
                Directory.Delete("HoxHud-bin-master");
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Unpack and and iterate over the downladed files
        /// creating the folders if they dont exists and
        /// replace already existing files
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">validation checks</param>
        private void Client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            if(e.Cancelled)
            {
                InstallBtn.Text = "Error: " + e.Error.Message;
                return;
            }
            InstallBtn.Text = "Unpacking...";
            this.Update();
            this.Size = new Size(278, 238);
            downloadProgressBar.Value = 0;
            downloadProgressBar.Enabled = false;

            System.IO.Compression.ZipFile.ExtractToDirectory(DownloadFile, ".");

            InstallBtn.Text = "Installing...";
            this.Update();
            //remove the installation files

            Directory.SetCurrentDirectory("HoxHud-bin-master/");

            try
            {
                foreach (string file in Directory.GetFiles(".", "*install*"))
                {
                    File.Delete(file);
                }

                foreach (string file in Directory.GetFiles(".", "*.txt"))
                {
                    File.Delete(file);
                }

                File.Delete(".gitignore");
            }
            catch (Exception) { }

            //CHECKING FOR DIRECTORY PERSISTENCE
            try
            {
                List<string> dirs = Directory.GetDirectories(".", "*", SearchOption.AllDirectories).ToList();

                foreach (string dir in dirs)
                {
                    Directory.CreateDirectory(GameFolderDiag.SelectedPath + dir);
                }
            }
            catch (Exception) { }

            if (keepTweakDataChkBtn.Checked)
                File.Delete("HoxHud/HoxHudTweakData.lua");

            //MOVE FILES OVER TO PAYDAY 2 FOLDER
            try
            {
                foreach (string filer in Directory.GetFiles(".", "*.*", SearchOption.AllDirectories))
                {
                    string file = filer.Replace(".\\", ""); //Dat shit is 2 hacky right now but it works :P
                    if (File.Exists(GameFolderDiag.SelectedPath + file))
                    {
                        if (file.ToLower().Contains("hoxhud\\"))
                        {
                            if (File.Exists(GameFolderDiag.SelectedPath + file + ".backup"))
                                File.Delete(GameFolderDiag.SelectedPath + file + ".backup");
                            File.Move(GameFolderDiag.SelectedPath + file, GameFolderDiag.SelectedPath + file + ".backup");
                        }
                        else
                        {
                            File.Delete(file);  //Why care to backup data that cannot be changed?
                        }
                    }
                    File.Move(file, GameFolderDiag.SelectedPath + file);
                }
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message);
            }

            DoCleanup();
            File.Delete(DownloadFile);

            InstallBtn.Text = "Finished!";
        }

        /// <summary>
        /// Find the Game Folder by firstly trying to get it from the registry
        /// if that fails it prompts the user
        /// </summary>
        /// <param name="sender">not used</param>
        /// <param name="e">not used</param>
        private void HoxHudOCI_Shown(object sender, EventArgs e)
        {
            if(!isPayday2Installed())   //Fuck Piracy
            {
                MessageBox.Show("Payday 2 is not Installed", "INFO", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Environment.Exit(0);
            }
            if (Directory.Exists(getPayday2Dir()) && File.Exists(getPayday2Dir() + "/payday2_win32_release.exe"))
            {
                GameFolderDiag.SelectedPath = getPayday2Dir() + "/";
            }
            else
            {
                GameFolderDiag.Description = "Select the Payday 2 Game Folder!";
                while (!File.Exists(GameFolderDiag.SelectedPath + "/payday2_win32_release.exe"))
                    if (GameFolderDiag.ShowDialog() != DialogResult.OK)
                        Environment.Exit(0);
                GameFolderDiag.SelectedPath += '/';
            }

            if(File.Exists(GameFolderDiag.SelectedPath + "HoxHud/HoxHudTweakData.lua" ))
            {
                InstallBtn.Location = new Point(0, 33);
                InstallBtn.Size = new Size(262, 167);
                keepTweakDataChkBtn.Visible = true;
            }

            //0, 33 ; 262, 167
        }

        private void HoxHudOCI_FormClosing(object sender, FormClosingEventArgs e)
        {
            DoCleanup();
        }
    }
}
