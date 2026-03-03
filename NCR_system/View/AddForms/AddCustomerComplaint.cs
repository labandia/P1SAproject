using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using NCR_system.View.Module;

namespace NCR_system.View.AddForms
{
    public partial class AddCustomerComplaint : Form
    {
        private readonly ICustomerComplaint _cus;
        private readonly Customer_Complaint_user _user;

        public string selectedImagepath = "";

        public AddCustomerComplaint(ICustomerComplaint cus, Customer_Complaint_user user)
        {
            InitializeComponent();
            _cus = cus;
            _user = user;

            selectDepart.SelectedIndex = 0;
            DisplayPlaceholder();
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            if (!FormValid()) return;

            string ImageUpload = await UploadServices.SaveImageFolder(selectedImagepath);

            var obj = new CustomerModel
            {
                ModelNo = ModelText.Text,
                LotNo = LotText.Text,
                NGQty = Convert.ToInt32(NGText.Text),
                Status = 1,
                Details = ProblemText.Text,
                SectionID = selectDepart.SelectedIndex + 1,
                CCtype = 1,
                UploadImage = ImageUpload
            };

            bool result = await _cus.InsertCustomerData(obj, 1);

            if (!result)
            {
                MessageBox.Show("Failed to save data.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error); 
                return;
            }


            MessageBox.Show("Data saved successfully.");
            await _user.DisplayCustomer(1);
            this.Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        
        public bool FormValid()
        {
            if (string.IsNullOrWhiteSpace(ModelText.Text) ||
                string.IsNullOrWhiteSpace(LotText.Text) ||
                string.IsNullOrWhiteSpace(NGText.Text) ||
                string.IsNullOrWhiteSpace(ProblemText.Text) ||
                selectDepart.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            if (!int.TryParse(NGText.Text, out _))
            {
                MessageBox.Show("NG Quantity must be a valid number.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void AddCustomerComplaint_Load(object sender, EventArgs e)
        {

        }

        private void AddImagebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagepath = ofd.FileName;
                pictureBox1.Image = Image.FromFile(selectedImagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void templatebtn_Click(object sender, EventArgs e)
        {
            UploadServices.DownloadFiles(1);    
        }

        private void Uploadbtn_Click(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void NGText_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Allow control keys (like backspace)
            if (!char.IsControl(e.KeyChar))
            {
                // Allow only one dot and digits
                if (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !NGText.Text.Contains(".")))
                {
                    e.Handled = false; // Allow the character
                }
                else
                {
                    e.Handled = true; // Cancel the keypress event
                }
            }
        }

        private void selectDepart_SelectedIndexChanged(object sender, EventArgs e)
        {
            MessageBox.Show($"Selected Department: {selectDepart.SelectedIndex}");
        }


        public void DisplayPlaceholder()
        {
            ModelText.Text = "Enter Model No.";
            ModelText.ForeColor = Color.Gray;

            LotText.Text = "Enter Lot No...";
            LotText.ForeColor = Color.Gray;

            NGText.Text = "Enter Amount of NG Quantity...";
            NGText.ForeColor = Color.Gray;

            ProblemText.Text = "Enter Contents...";
            ProblemText.ForeColor = Color.Gray;
        }

        private void ModelText_Click(object sender, EventArgs e)
        {
            ModelText.Text = "";
            ModelText.ForeColor = Color.Black;
        }

        private void LotText_Click(object sender, EventArgs e)
        {
            LotText.Text = "";
            LotText.ForeColor = Color.Black;
        }

        private void NGText_Click(object sender, EventArgs e)
        {
            NGText.Text = "";
            NGText.ForeColor = Color.Black;
        }

        private void ProblemText_Click(object sender, EventArgs e)
        {
            ProblemText.Text = "";
            ProblemText.ForeColor = Color.Black;
        }
    }
}
