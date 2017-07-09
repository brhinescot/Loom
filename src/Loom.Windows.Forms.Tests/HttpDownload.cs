#region Using Directives

using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Windows.Forms;

#endregion

namespace Loom.Windows.Forms.Tests
{
    public partial class HttpDownload : Form
    {
        public HttpDownload()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (saveFile.ShowDialog(this) == DialogResult.OK)
            {
                WebClient client = new WebClient();
                client.DownloadFileAsync(new Uri(textBox1.Text), saveFile.FileName + Path.GetFileName(textBox1.Text));
                client.DownloadFileCompleted += HandleClientDownloadFileCompleted;
            }
        }

        private void HandleClientDownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            MessageBox.Show("Download Complete");
        }
    }
}