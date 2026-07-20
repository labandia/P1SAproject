using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IssuanceSystem.Interface;
using IssuanceSystem.Model;
using Microsoft.Web.WebView2.WinForms;

namespace IssuanceSystem
{
    internal partial class Form1 : Form
    {
        private CancellationTokenSource _cts;

        private readonly IIsuanceRespository _sua;

        // Fixed root folder to search within
        private readonly string rootFolder = @"C:\Users\jaye-labandia\Desktop\PCB";
        private readonly BindingSource _bindingSource = new BindingSource();

        public Form1(IIsuanceRespository sua)
        {
            InitializeComponent();
            _sua = sua;

            dataGridView1.AutoGenerateColumns = true;
            dataGridView1.DataSource = _bindingSource;
            EnableDoubleBuffering(dataGridView1);
        }

        public static void EnableDoubleBuffering(DataGridView dgv)
        {
            typeof(DataGridView)
                .GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic)
                .SetValue(dgv, true, null);
        }

        private async void Form1_Load(object sender, EventArgs e) => await loadData();

        public async Task loadData(bool append = false)
        {
            _cts?.Cancel();
            _cts?.Dispose();
            _cts = new CancellationTokenSource();
            var token = _cts.Token;

            try
            {
                dataGridView1.SuspendLayout();


                var dataTask = await _sua.GetIssuanceTransaction();

                if (token.IsCancellationRequested) return;
                BindGrid(dataTask, append);

            }
            catch (Exception ex)
            {
                //MessageBox.Show($"Load error: {ex.Message}");
                Debug.WriteLine($"Load error: {ex.Message}");
            }

        }

        private void BindGrid(List<IssuanceTransactionModel> result, bool append)
        {
            if (append && _bindingSource.DataSource is List<IssuanceTransactionModel> existing)
            {
                existing.AddRange(result);
                _bindingSource.ResetBindings(false);
            }
            else
            {
                _bindingSource.DataSource = result;
            }

            dataGridView1.ClearSelection();
        }

        public void ArrangeColumns()
        {
          
            dataGridView1.Columns["ShopOrder"].HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dataGridView1.Columns["ShopOrder"].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            dataGridView1.Columns["ShopOrder"].Width = 120;

            ConfigureColumn(dataGridView1, "ItemNo", 120);
            ConfigureColumn(dataGridView1, "PartNo", 120);
            ConfigureColumn(dataGridView1, "ProcessName", 200);
            ConfigureColumn(dataGridView1, "Description", 200);
            ConfigureColumn(dataGridView1, "PreparedQuantity", 200);

            ConfigureColumn(dataGridView1, "DepartmentID", 120);
            dataGridView1.Columns["PreparedQuantity"].DefaultCellStyle.Format = "0.##";
        }

        public static void ConfigureColumn(DataGridView grid, string name, int width)
        {
            if (!grid.Columns.Contains(name)) return;

            var col = grid.Columns[name];

            col.HeaderCell.Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            col.AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            col.Width = width;
        }

        private async void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;

            await SearchandDisplay();
        }

        public async Task SearchandDisplay()
        {
            if (!FormValidation()) return;

            string partnum = textBox1.Text.Trim();
            //if (string.IsNullOrEmpty(partnum)) return;

            var getDetails = await _sua.GetIssuanceDetails(partnum, RevText.Text);

            if (getDetails != null)
            {
                string filename = partnum + RevText.Text;

                label10.Text = getDetails.Application;
                label6.Text = getDetails.Remarks;

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
                            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;

                            pictureBox1.BringToFront();
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

                            webView21.BringToFront();
                        }
                        else
                        {
                            System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo(foundPath)
                            {
                                UseShellExecute = true
                            });
                        }

                        ShowStatus(Color.FromArgb(14, 209, 69), "GOOD PCB");

                        bool result = await _sua.OnsubmitTransaction(AmbassadorText.Text, textBox1.Text,
                            label13.Text, label6.Text, label10.Text);

                        if (result)
                        {
                            MessageBox.Show("Save succesfully");

                           await loadData();
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
                ShowStatus(Color.FromArgb(238, 28, 36), "WRONG PCB");
            }
        }



        private async void btnSearch_Click(object sender, EventArgs e)
        {
           
        }

     


        private void txtSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnSearch_Click(sender, e);
            }
        }

        private async void Savebtn_Click(object sender, EventArgs e)
        {
            if (!FormValidation()) return;

            bool result = await _sua.OnsubmitTransaction(AmbassadorText.Text, textBox1.Text, 
                label13.Text, label6.Text, label10.Text);

            if (result)
            {
                MessageBox.Show("Save succesfully");
            }
        }

        public bool FormValidation()
        {
            bool result = true;

            if (string.IsNullOrEmpty(AmbassadorText.Text))
            {
                MessageBox.Show("Input FA Shop Order is required");
                return false;
            }else if (string.IsNullOrEmpty(textBox1.Text))
            {
                MessageBox.Show("Input PRepared PCB assembly is required");
                return false;
            }
            else if (string.IsNullOrEmpty(RevText.Text))
            {
                MessageBox.Show("Input Revision is required");
                return false;
            }

            return result;
        }

        private async void RevText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;
            e.SuppressKeyPress = true;

            await SearchandDisplay();
        }

        // ══════════════════════════════════════════════════════════════
        //  STATUS DISPLAY & TIMER
        // ══════════════════════════════════════════════════════════════
        private void ShowStatus(Color bg, string text)
        {
            //_statusTimer.Stop();
            Statustext.BackColor = bg;
            Statustext.Text = text;
            //_statusTimer.Start();
        }
        private void StatusTimer_Tick(object sender, EventArgs e)
        {
            Statustext.BackColor = Color.FromArgb(26, 36, 59);
            Statustext.Text = "CHECK PCBA ....";
        }
    }
}
