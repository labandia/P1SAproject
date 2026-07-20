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

namespace IssuanceSystem
{
    public partial class sample : Form
    {
        private readonly string rootFolder = @"C:\Users\jaye-labandia\Desktop\PCB";

        public sample()
        {
            InitializeComponent();
            lstResults.View = View.Details;
            lstResults.FullRowSelect = true;
            lstResults.Columns.Add("Name", 250);
            lstResults.Columns.Add("Type", 80);
            lstResults.Columns.Add("Full Path", 400);
            lstResults.Columns.Add("Modified", 130);

            lblStatus.Text = $"Searching within: {rootFolder}";
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

    }
}
