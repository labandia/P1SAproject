using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using NCR_system.View.Module;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddExternalCC : Form
    {
        private readonly ICustomerComplaint _cus;
        private readonly Customer_Complaint_user _user;

        string selectedImagepath = "";

        public AddExternalCC(ICustomerComplaint cus, Customer_Complaint_user user)
        {
            InitializeComponent();
            _cus = cus;
            _user = user;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {

            if(!FormValid()) return;

            string ImageUpload = await UploadServices.SaveImageFolder(selectedImagepath);

            var obj = new CustomerModel
            {
                RegNo = EditRegNo.Text,
                CustomerName = EditCustomerText.Text,
                SectionID = selectDepart.SelectedIndex,
                ModelNo = EditModelText.Text,
                LotNo = EditLotText.Text,
                NGQty = (int)EditNGText.Value,
                Details = EditProblemText.Text,
                CCtype = 0, 
                UploadImage = ImageUpload
            };

            bool result = await _cus.InsertCustomerData(obj, 0);

            if (result)
            {
                MessageBox.Show("Data saved successfully.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                await _user.DisplayCustomer(0);
                this.Close();
            }
            else
            {
                MessageBox.Show("Failed to save data.");
            }

        }

        private void AddExternalCC_Load(object sender, EventArgs e)
        {
            EditNGText.MinimumSize = new Size(0, 40);
            EditNGText.MaximumSize = new Size(500, 40);
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public bool FormValid()
        {
            if (string.IsNullOrWhiteSpace(EditRegNo.Text) ||
                string.IsNullOrWhiteSpace(EditCustomerText.Text) ||
                string.IsNullOrWhiteSpace(EditModelText.Text) ||
                string.IsNullOrWhiteSpace(EditLotText.Text) ||
                string.IsNullOrWhiteSpace(EditNGText.Text) ||
                string.IsNullOrWhiteSpace(EditProblemText.Text) ||
                selectDepart.SelectedIndex == -1)
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
    }
}
