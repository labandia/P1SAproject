using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IssuanceSystem.Interface;
using Microsoft.Web.WebView2.WinForms;

namespace IssuanceSystem
{
    internal partial class Form1 : Form
    {
        private readonly IIsuanceRespository _sua;

        // Fixed root folder to search within
        private readonly string rootFolder = @"C:\Users\jaye-labandia\Desktop\PCB";

        public Form1(IIsuanceRespository sua)
        {
            InitializeComponent();
            _sua = sua;

            lstResults.View = View.Details;
            lstResults.FullRowSelect = true;
            lstResults.Columns.Add("Name", 250);
            lstResults.Columns.Add("Type", 80);
            lstResults.Columns.Add("Full Path", 400);
            lstResults.Columns.Add("Modified", 130);

            lblStatus.Text = $"Searching within: {rootFolder}";
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            //await webView21.EnsureCoreWebView2Async(null);

            //string pdfPath = @"C:\Users\jaye-labandia\Desktop\PCB\00015063-01R\00015063-01M (P1SAC-2303001).pdf";

            //if (File.Exists(pdfPath))
            //{
            //    // WebView2 needs a properly encoded file:// URI, especially since
            //    // this path has spaces and parentheses in it
            //    string uri = new Uri(pdfPath).AbsoluteUri;
            //    webView21.CoreWebView2.Navigate(uri);
            //}
            //else
            //{
            //    MessageBox.Show("PDF not found:\n" + pdfPath);
            //}




            //pictureBox1.Image = Image.FromFile(@"\\172.29.1.5\sdpsyn01\Process Control\SystemImages\NonComformity\2c264587-ffab-42db-bdca-7abd826c3b3a.png");
        }

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;
            string partnum = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(partnum)) return;
            bool result = await _sua.SearchPartnumber(partnum, RevText.Text);
            if (result)
            {
                string filename = partnum + RevText.Text;

                // Step 1: find the folder matching filename
                string foundFolder = await Task.Run(() =>
                    Directory.GetDirectories(rootFolder, "*", SearchOption.AllDirectories)
                             .FirstOrDefault(d => Path.GetFileName(d).IndexOf(filename, StringComparison.OrdinalIgnoreCase) >= 0)
                );

                if (foundFolder == null)
                {
                    MessageBox.Show($"Folder matching \"{filename}\" was not found in {rootFolder}.");
                    return;
                }

                // Step 2: find the first valid file inside that folder
                string foundPath = await Task.Run(() =>
                    Directory.GetFiles(foundFolder, "*", SearchOption.AllDirectories)
                             .FirstOrDefault(f => !Path.GetFileName(f).Equals(".DS_Store", StringComparison.OrdinalIgnoreCase))
                );

                if (foundPath != null)
                {
                    try
                    {
                        // Clear previous content first
                        pictureBox1.Image?.Dispose();
                        pictureBox1.Image = null;
                        webView21.CoreWebView2?.Navigate("about:blank");


                        string ext = Path.GetExtension(foundPath).ToLowerInvariant();
                        string[] imageExtensions = { ".jpg", ".jpeg", ".png", ".bmp", ".gif", ".tif", ".tiff" };
                        if (imageExtensions.Contains(ext))
                        {
                            pictureBox1.Image = Image.FromFile(foundPath);
                        }
                        else if (ext == ".pdf")
                        {
                            await webView21.EnsureCoreWebView2Async(null);

                            if (File.Exists(foundPath))
                            {
                                string uri = new Uri(foundPath).AbsoluteUri + "#view=FitH"; // or "#zoom=page-fit"
                                webView21.CoreWebView2.Navigate(uri);
                            }
                            else
                            {
                                MessageBox.Show("PDF not found:\n" + foundPath);
                            }
                        }
                        else
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(foundPath)
                            {
                                UseShellExecute = true
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Could not open file: " + ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show($"No usable files found in folder \"{foundFolder}\".");
                }
            }
            else
            {
                MessageBox.Show("Part number / revision not found.");
            }
        }

        private async void btnSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = txtSearch.Text.Trim();

            if (!Directory.Exists(rootFolder))
            {
                MessageBox.Show($"Folder not found:\n{rootFolder}");
                return;
            }
            if (string.IsNullOrEmpty(searchTerm))
            {
                MessageBox.Show("Enter a name to search for.");
                return;
            }

            lstResults.Items.Clear();
            btnSearch.Enabled = false;
            lblStatus.Text = "Searching...";

            try
            {
                var results = await Task.Run(() => SearchFolder(rootFolder, searchTerm));

                foreach (var item in results)
                    lstResults.Items.Add(item);

                lblStatus.Text = $"{results.Count} result(s) found in {rootFolder}";
            }
            catch (Exception ex)
            {
                MessageBox.Show("Search error: " + ex.Message);
            }
            finally
            {
                btnSearch.Enabled = true;
            }
        }

        private List<ListViewItem> SearchFolder(string startFolder, string searchTerm)
        {
            var matches = new List<ListViewItem>();

            void SearchRecursive(string path)
            {
                IEnumerable<string> entries;
                try
                {
                    entries = Directory.EnumerateFileSystemEntries(path);
                }
                catch (UnauthorizedAccessException) { return; }
                catch (PathTooLongException) { return; }

                foreach (var entry in entries)
                {
                    string name = Path.GetFileName(entry);

                    if (name.IndexOf(searchTerm, StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        bool isDir = Directory.Exists(entry);
                        var lvi = new ListViewItem(name);
                        lvi.SubItems.Add(isDir ? "Folder" : "File");
                        lvi.SubItems.Add(entry);
                        lvi.SubItems.Add(File.GetLastWriteTime(entry).ToString("yyyy-MM-dd HH:mm"));
                        lvi.Tag = entry;
                        matches.Add(lvi);
                    }

                    if (Directory.Exists(entry))
                        SearchRecursive(entry);
                }
            }

            SearchRecursive(startFolder);
            return matches;
        }

        private void lstResults_DoubleClick(object sender, EventArgs e)
        {
            if (lstResults.SelectedItems.Count == 0) return;
            string path = lstResults.SelectedItems[0].Tag.ToString();

            if (File.Exists(path))
                System.Diagnostics.Process.Start("explorer.exe", $"/select,\"{path}\"");
            else if (Directory.Exists(path))
                System.Diagnostics.Process.Start("explorer.exe", $"\"{path}\"");
        }

        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch_Click(sender, e);
            }
        }

        private void Savebtn_Click(object sender, EventArgs e)
        {

        }
    }
}
