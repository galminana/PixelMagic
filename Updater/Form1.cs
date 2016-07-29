using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using PixelMagic.Helpers;

namespace Updater
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private int _gitVersion;
        private int GitHubVersion
        {
            get
            {
                if (_gitVersion == 0)
                {
                    try
                    {
                        var versionInfo = Web.GetString("https://raw.githubusercontent.com/winifix/PixelMagic-OpenSource/master/PixelMagic/Properties/AssemblyInfo.cs").
                            Split('\r').
                            FirstOrDefault(r => r.Contains("AssemblyFileVersion"))?.
                            Replace("\n", "").
                            Replace("[assembly: AssemblyFileVersion(\"", "").
                            Replace("\")]", "").
                            Split('.')[0];

                        if (versionInfo == null)
                        {
                            throw new Exception();
                        }

                        _gitVersion = int.Parse(versionInfo);
                    }
                    catch
                    {
                        Log.Write("Unable to determine GitHub version", Color.Red);
                        Log.Write("Please ensure internet connectivity is working...", Color.Red);
                        _gitVersion = 1000000;
                    }
                }
                return _gitVersion;
            }
        }

        private int LocalVersion
        {
            get
            {
                var myFileVersionInfo = FileVersionInfo.GetVersionInfo(Application.StartupPath + "\\PixelMagic.exe");

                return int.Parse(myFileVersionInfo.FileVersion.Split('.')[0]);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Log.Initialize(richTextBox1, this);
            Log.Write("Checking for updates...");

            this.Shown += Form1_Shown;
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            Log.Write("Latest version: " + GitHubVersion);
            Log.Write("Current version: " + LocalVersion);

            if (GitHubVersion >= LocalVersion)
            {
                var path = Application.StartupPath;

                Log.Write("Starting download of update...", Color.Black);

                if (!File.Exists($"{path}\\Update_v{GitHubVersion}.zip"))
                {
                    using (WebClient wb = new WebClient())
                    {
                        wb.DownloadProgressChanged += Wb_DownloadProgressChanged;
                        wb.DownloadFileCompleted += Wb_DownloadFileCompleted;
                        wb.DownloadFileAsync(new Uri("https://github.com/winifix/PixelMagic-OpenSource/archive/master.zip"), $"Update_v{GitHubVersion}.zip");
                    }
                }
                else
                {
                    Log.Write($"Download completed, extract 'Update_v{GitHubVersion}.zip'");
                }
            }
            else
            {
                Log.Write("No update found.", Color.Green);
            }
        }

        private void Wb_DownloadFileCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            Log.Write($"Download completed, extract 'Update_v{GitHubVersion}.zip'");
        }

        private void Wb_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            progressBar1.Maximum = 100;
            progressBar1.Value = e.ProgressPercentage;

            Application.DoEvents();
        }
    }
}
