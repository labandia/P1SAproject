using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        BindingList<CustomerModel> listdata = new BindingList<CustomerModel>();

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
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !NGText.Text.Contains("."))) ? false : true; // Allow the character
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

        private async void button1_Click(object sender, EventArgs e)
        {
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

            listdata.Add(obj);

            CustomDatagrid.DataSource = listdata;
            ResetDisplay();
            ModelText.Focus();
        }

        public void ResetDisplay()
        {
            ModelText.Text = "";
            LotText.Text = "";
            NGText.Text = "";
            ProblemText.Text = "";
            ModelText.Text = "";
            selectDepart.SelectedIndex = 0;
        }

        private async void Finalizebtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in listdata)
                {
                    await _cus.InsertCustomerData(item, 1);
                }

                Finalizebtn.Enabled = false;
                Finalizebtn.BackColor = Color.Gray;
                Finalizebtn.ForeColor = Color.White;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Cancel_btn_Click_1(object sender, EventArgs e)
        {

        }
    }
}
