using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.IO;

namespace MSDMonitoring
{
    public partial class ChangeQuantity : Form
    {
        public static string filepath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\TempData\MSDauthorizedpassword.txt";

        private readonly IMSD _msd;
        private readonly MSDHIstory _msdhistory;
        public int _ID;
        public int _Quantity;
        public string _InputDate;
        public string _Time;

        public ChangeQuantity(IMSD msd, int ID, int Quantity, MSDHIstory msdhistory, string InputDate, string Time)
        {
            InitializeComponent();
            _msd = msd;
            _ID = ID;
            _msdhistory = msdhistory;   
            _Quantity = Quantity;
            _InputDate = InputDate;
            _Time = Time;   
        }

        private void quan_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }

        private async void Addbtn_Click(object sender, EventArgs e)
        {
            DialogResult diaresult = MessageBox.Show(
                   "You are not Authorized To Change the Data? Ask the Authorized person to change the Data",
                   "Confirmation",
                   MessageBoxButtons.YesNo,
                   MessageBoxIcon.Question
            );

            if (diaresult == DialogResult.Yes)
            {
                while (true)
                {
                    string input = ShowInputDialog("Enter authorized password:", "Authorization Required");

                    if (input == null) // User pressed Cancel
                    {
                        return;
                    }

                    if (string.IsNullOrWhiteSpace(input))
                    {
                        MessageBox.Show("Password cannot be empty.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        continue;
                    }

                    if (await VerifyAuthorizedPassword(input))
                    {
                        MessageBox.Show("Password verified!.");

                        int Quantity = Convert.ToInt32(quan.Text);

                        bool result = await _msd.EditComponentsData(_ID, Quantity);

                        if (result)
                        {
                            MessageBox.Show("Update Successfully ", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            await _msdhistory.LoadData("");
                            this.Close();
                        }

                        break; // ✅ exit loop after success
                    }
                    else
                    {
                        MessageBox.Show("Incorrect Password, please try again.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                return;
            }

        }

        private void Button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ChangeQuantity_Load(object sender, EventArgs e)
        {
            quan.Text = _Quantity.ToString();
        }

        public static string ShowInputDialog(string text, string caption)
        {
            Form prompt = new Form()
            {
                Width = 400,
                Height = 160,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                Text = caption,
                StartPosition = FormStartPosition.CenterScreen
            };

            Label lblText = new Label() { Left = 20, Top = 20, Text = text, Width = 340 };
            TextBox textBox = new TextBox() { Left = 20, Top = 50, Width = 340, PasswordChar = '*' };

            Button okButton = new Button() { Text = "OK", Left = 190, Width = 80, Top = 90, DialogResult = DialogResult.OK };
            Button cancelButton = new Button() { Text = "Cancel", Left = 280, Width = 80, Top = 90, DialogResult = DialogResult.Cancel };

            okButton.Click += (sender, e) => { prompt.Close(); };
            cancelButton.Click += (sender, e) => { prompt.Close(); };

            prompt.Controls.Add(lblText);
            prompt.Controls.Add(textBox);
            prompt.Controls.Add(okButton);
            prompt.Controls.Add(cancelButton);

            prompt.AcceptButton = okButton;
            prompt.CancelButton = cancelButton;

            return prompt.ShowDialog() == DialogResult.OK ? textBox.Text : null;
        }



        public async static Task<bool> VerifyAuthorizedPassword(string pass)
        {
            if (!File.Exists(filepath))
            {
                Debug.WriteLine($"File not found: {filepath}");
                return false;
            }

            try
            {
                string fileContent;
                using (var reader = new StreamReader(filepath))
                {
                    fileContent = await reader.ReadToEndAsync();
                }

                return fileContent.Trim() == pass;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error reading file: {ex.Message}");
                return false;
            }
        }
    }
}
