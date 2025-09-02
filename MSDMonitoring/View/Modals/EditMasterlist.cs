using MSDMonitoring.Data;
using MSDMonitoring.Interface;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MSDMonitoring.View.Modals
{
    public partial class EditMasterlist : Form
    {
        public static string filepath = @"\\sdp01034s\SYSTEM EXECUTABLE\P1SA-PC_System\TempData\MSDauthorizedpassword.txt";

        private readonly IMSD _msd;
        private readonly MSDMasterlist _master;
        private readonly MSDMasterlistodel _msdinput;

        public EditMasterlist(IMSD msd, MSDMasterlist master, MSDMasterlistodel msdinput)
        {
            InitializeComponent();
            _msd = msd;
            _master=master;
            _msdinput = msdinput;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
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

                        var obj = new MSDMasterlistodel
                        {
                            AmbassadorPartnum = _msdinput.AmbassadorPartnum,
                            Partname = partnameText.Text,
                            SupplyName = SupplierText.Text,
                            SupplyPartName = SupplierNameText.Text,
                            Level = Convert.ToInt32(levelText.Text),
                            FloorLife = Convert.ToInt32(FloorlifeText.Text)
                        };


                        bool result = await _msd.AddEditMasterlistData(obj, 1, Ambassador.Text.Trim());

                        if (result)
                        {
                            MessageBox.Show("Edit Masterlist Successfully");
                            await _master.DisplayData();
                            _master.searchBox.Text = "";
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

        private void EditMasterlist_Load(object sender, EventArgs e)
        {
            Ambassador.Text = _msdinput.AmbassadorPartnum;
            partnameText.Text = _msdinput.Partname;
            SupplierNameText.Text = _msdinput.SupplyPartName;
            SupplierText.Text = _msdinput.SupplyName;
            levelText.Text = _msdinput.Level.ToString();
            FloorlifeText.Text = _msdinput.FloorLife.ToString();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void levelText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
        }

        private void FloorlifeText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Reject the input
            }
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
