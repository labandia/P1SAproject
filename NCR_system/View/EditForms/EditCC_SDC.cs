using NCR_system.Interface;
using NCR_system.Models;
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
            EditModelText.ReadOnly = true;
            EditModelText.BackColor = SystemColors.Window;

            EditLotText.Text = cus.LotNo;
            EditLotText.ReadOnly = true;
            EditLotText.BackColor = SystemColors.Window;

            EditNGText.Text = cus.NGQty.ToString();
            EditNGText.ReadOnly = true;
            EditNGText.BackColor = SystemColors.Window;

            EditProblemText.Text = cus.Details;
            EditProblemText.ReadOnly = true;
            EditProblemText.BackColor = SystemColors.Window;

            comboBox1.SelectedIndex = cus.Status == 1 ? 0 : 1;
            selectDepart.SelectedIndex = cus.SectionID;

            if (cus.UploadImage != null && cus.UploadImage != "")
            {
                selectedImagepath = cus.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(cus.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

      

        private void Cancel_btn_Click(object sender, EventArgs e) => this.Close();

        private async void Save_btn_Click(object sender, EventArgs e)
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
                this.Close();
            }
        }

        private void selectDepart_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void EditModelText_TextChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e) => this.Close();    
    }
}
