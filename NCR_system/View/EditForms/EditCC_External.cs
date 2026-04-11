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
    public partial class EditCC_External : Form
    {
        private readonly ICustomerComplaint _cus;
        public string selectedImagepath = "";
        public bool isEditImage = false;
        public int storeID;
        public readonly int currentRecordID;

        public EditCC_External(CustomerModel cus, ICustomerComplaint cust)
        {
            InitializeComponent();
            _cus = cust;

            EditRegNo.Text = cus.RegNo;
            EditRegNo.ReadOnly = true;
            EditRegNo.BackColor = SystemColors.Window;

            EditCustomerText.Text = cus.CustomerName;
            EditCustomerText.ReadOnly = true;
            EditCustomerText.BackColor = SystemColors.Window;

            EditModelText.Text = cus.ModelNo;
            EditModelText.ReadOnly = true;
            EditCustomerText.BackColor = SystemColors.Window;

            EditLotText.Text = cus.LotNo;
            EditLotText.ReadOnly = true;
            EditLotText.BackColor = SystemColors.Window;

            EditNGText.Text = cus.NGQty.ToString();
            EditNGText.ReadOnly = true;
            EditNGText.BackColor = SystemColors.Window;

            EditProblemText.Text = cus.Details;
            EditProblemText.ReadOnly = true;
            EditProblemText.BackColor = SystemColors.Window;

            selectDepart.SelectedIndex = cus.SectionID;
            comboBox1.SelectedIndex = cus.Status == 1 ? 0 : 1;

            if (cus.UploadImage != null && cus.UploadImage != "")
            {
                selectedImagepath = cus.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(cus.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

    
        private async void Save_btn_Click(object sender, EventArgs e)
        {
            bool result = await _cus.UpdateCustomerData(new CustomerModel
            {
                RecordID = storeID,
                RegNo = EditRegNo.Text,
                CustomerName = EditCustomerText.Text,
                ModelNo = EditModelText.Text,
                LotNo = EditLotText.Text,
                Status = comboBox1.SelectedItem.ToString() == "Open" ? 1 : 0,
                NGQty = Convert.ToInt32(EditNGText.Text),
                Details = EditProblemText.Text
            }, 0);

            if (result)
            {
                MessageBox.Show("Edit Successfully");
                this.Close();
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e) => this.Close();    
    }
}
