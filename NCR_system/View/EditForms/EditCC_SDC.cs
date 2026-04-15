using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.EditForms
{
    public partial class EditCC_SDC : Form
    {
        private readonly ICustomerComplaint _cus;
        public string selectedImagepath = "";
        public bool isEditImage = false;
        public int storeID;
        public readonly int currentRecordID;

        public EditCC_SDC(CustomerModel cus, ICustomerComplaint cust)
        {
            InitializeComponent();
            _cus = cust;
            currentRecordID = cus.RecordID;

            EditModelText.Text = cus.ModelNo;

            EditLotText.Text = cus.LotNo;

            EditNGText.Text = cus.NGQty.ToString();

            EditProblemText.Text = cus.Details;

            comboBox1.SelectedIndex = cus.Status == 1 ? 0 : 1;
            comboBox1.Enabled = false;
            selectDepart.SelectedIndex = cus.SectionID;
            selectDepart.Enabled = false;

            ResetForm(true);

            if (cus.UploadImage != null && cus.UploadImage != "")
            {
                selectedImagepath = cus.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(cus.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(UploadServices.noImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        public void ResetForm(bool ismode)
        {
            EditModelText.ReadOnly = ismode;
            EditModelText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;

            EditLotText.ReadOnly = ismode;
            EditLotText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;

            EditNGText.ReadOnly = ismode;
            EditNGText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;

            EditProblemText.ReadOnly = ismode;
            EditProblemText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;

            Modetext.Text = ismode ? "View Mode" : "Edit Mode";
        }


        private void Cancel_btn_Click(object sender, EventArgs e) => this.Close();

        private async void Save_btn_Click(object sender, EventArgs e)
        {
           
        }

        private void button12_Click(object sender, EventArgs e) => this.Close();    

       

        private async void button1_Click(object sender, EventArgs e)
        {
            string status = comboBox1.SelectedItem.ToString();

            var obj = new CustomerModel
            {
                RecordID = currentRecordID,
                ModelNo = EditModelText.Text,
                LotNo = EditLotText.Text,
                NGQty = Convert.ToInt32(EditNGText.Text),
                Details = EditProblemText.Text,
                Status = status == "Open" ? 1 : 0
            };

            bool result = await _cus.UpdateCustomerData(obj, ComplaintUpdateType.WithoutCustomerInfo);

            if (result)
            {
                MessageBox.Show("Data Successfully Updated!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            ResetForm(false);
            Modetext.Text = "Edit Mode";
        }
    }
}
