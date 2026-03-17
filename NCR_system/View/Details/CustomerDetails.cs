using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Details
{
    public partial class CustomerDetails : Form
    {
        private readonly ICustomerComplaint _cus;
        public string selectedImagepath = "";
        public bool isEditImage = false;
        public readonly int currentRecordID;

        public CustomerDetails(CustomerModel cus, ICustomerComplaint cust)
        {
            InitializeComponent();
            _cus = cust;    

            currentRecordID = cus.RecordID;
            EditRegNo.Text = cus.RegNo;
            EditCustomerText.Text = cus.CustomerName;
            EditLotText.Text = cus.LotNo;
            EditProblemText.Text = cus.Details;
            selectDepart.SelectedIndex = cus.SectionID;
            EditNGText.Text = cus.NGQty.ToString();
            EditModelText.Text = cus.ModelNo;

            ReadOnlyText();

            if (cus.UploadImage != null && cus.UploadImage != "")
            {
                selectedImagepath = cus.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(cus.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void CustomerDetails_Load(object sender, EventArgs e)
        {

        }

        private void Finalizebtn_Click(object sender, EventArgs e)
        {
            EditRegNo.ReadOnly = false;
            EditCustomerText.ReadOnly = false;
            EditLotText.ReadOnly = false;
            EditProblemText.ReadOnly = false;
            EditNGText.ReadOnly = false; 
            EditModelText.ReadOnly = false;

            Finalizebtn.Visible = false;
            Cancel_btn.Visible = true;
            Save_btn.Visible = true;
            Deletebtn.Visible = true;
        }

        public void ReadOnlyText()
        {
            EditRegNo.ReadOnly = true;
            EditCustomerText.ReadOnly = true;
            EditLotText.ReadOnly = true;
            EditProblemText.ReadOnly = true;
            EditNGText.ReadOnly = true;
            EditModelText.ReadOnly = true;
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            try
            {

                string ImageUpload = await UploadServices.SaveImageFolder(selectedImagepath);

                var obj = new CustomerModel
                {
                    RecordID = currentRecordID,
                    RegNo = EditRegNo.Text,
                    CustomerName = EditCustomerText.Text,
                    SectionID = selectDepart.SelectedIndex,
                    ModelNo = EditModelText.Text,
                    LotNo = EditLotText.Text,
                    NGQty = Convert.ToInt32(EditNGText.Text),
                    Details = EditProblemText.Text,
                    Status = 1,
                    CCtype = 0,
                    UploadImage = isEditImage ? ImageUpload : selectedImagepath
                };

               

                bool result = await _cus.UpdateCustomers(obj, 0);

                if (result)
                {
                    MessageBox.Show("Updated Data successfully");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error : " + ex.Message);
            }
        }

        private void EditNGText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !EditNGText.Text.Contains("."))) ? false : true; // Allow the character
        }

        private async void Deletebtn_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to delete this Attendance", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (exit == DialogResult.Yes)
            {
                bool result = await _cus.DeleteCustomers(currentRecordID);
                if (result)
                {
                    DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                isEditImage = true;
                selectedImagepath = ofd.FileName;
                pictureBox1.Image = System.Drawing.Image.FromFile(selectedImagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            ReadOnlyText();
            Finalizebtn.Visible = true;
            Cancel_btn.Visible = false;
            Save_btn.Visible = false;
            Deletebtn.Visible = false;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK; Close();
        }
    }
}
