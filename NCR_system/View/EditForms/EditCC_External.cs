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


        public Color clrdisable = Color.FromArgb(242, 243, 245);

        public EditCC_External(CustomerModel cus, ICustomerComplaint cust)
        {
            InitializeComponent();
            _cus = cust;

            storeID = cus.RecordID;

            EditRegNo.Text = cus.RegNo;
            EditRegNo.ReadOnly = true;
            EditRegNo.BackColor = clrdisable; 

            EditCustomerText.Text = cus.CustomerName;
            EditCustomerText.ReadOnly = true;
            EditCustomerText.BackColor = clrdisable;

            EditModelText.Text = cus.ModelNo;
            EditModelText.ReadOnly = true;
            EditCustomerText.BackColor = clrdisable;

            EditLotText.Text = cus.LotNo;
            EditLotText.ReadOnly = true;
            EditLotText.BackColor = clrdisable;

            EditNGText.Text = cus.NGQty.ToString();
            EditNGText.ReadOnly = true;
            EditNGText.BackColor = clrdisable;

            EditProblemText.Text = cus.Details;
            EditProblemText.ReadOnly = true;
            EditProblemText.BackColor = clrdisable;

            selectDepart.SelectedIndex = cus.SectionID;
            selectDepart.Enabled = false;
            comboBox1.SelectedIndex = cus.Status == 1 ? 0 : 1;
            comboBox1.Enabled = false;

            EditProblemText.ReadOnly = true;
            EditProblemText.BackColor = clrdisable;

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
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        


        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button12_Click(object sender, EventArgs e) => this.Close();

        private void Editbtn_Click(object sender, EventArgs e)
        {
            EditRegNo.ReadOnly = false;
            EditRegNo.BackColor = Color.White;

          
            EditCustomerText.ReadOnly = false;
            EditCustomerText.BackColor = Color.White;
            EditModelText.ReadOnly = false;
            EditModelText.BackColor = Color.White;
            EditLotText.ReadOnly = false;
            EditLotText.BackColor = Color.White;
            EditNGText.ReadOnly = false;
            EditNGText.BackColor = Color.White;
            EditProblemText.ReadOnly = false;
            EditProblemText.BackColor = Color.White;
  
            comboBox1.Enabled = true;
            comboBox1.ForeColor = Color.White;
            selectDepart.Enabled = true;
            selectDepart.BackColor = Color.White;

            EditProblemText.ReadOnly = false;
            EditProblemText.BackColor = Color.White;

            Save_btn.Visible = true;
            Save_btn.BackColor = Color.FromArgb(25, 131, 230);
            Save_btn.ForeColor = Color.White;

            Editbtn.Visible = false;
            Editbtn.BackColor = Color.WhiteSmoke;
            //Editbtn.ForeColor = Color.DarkGray;

        }

        private void EditCC_External_Load(object sender, EventArgs e)
        {
            Save_btn.Visible = false;
            Editbtn.Visible = true;
        }
    }
}
