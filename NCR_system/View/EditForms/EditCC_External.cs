using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using System;
using System.Drawing;
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

            storeID = cus.RecordID;

            EditRegNo.Text = cus.RegNo;

            EditCustomerText.Text = cus.CustomerName;

            EditModelText.Text = cus.ModelNo;

            EditLotText.Text = cus.LotNo;

            EditNGText.Text = cus.NGQty.ToString();

            EditProblemText.Text = cus.Details;

            selectDepart.SelectedIndex = cus.SectionID;
            selectDepart.Enabled = false;
            comboBox1.SelectedIndex = cus.Status == 1 ? 0 : 1;
            comboBox1.Enabled = false;

            ModeForm(true);

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
            ModeForm(false);
        }

        private void EditCC_External_Load(object sender, EventArgs e)
        {
            Save_btn.Enabled = false;
            Editbtn.Enabled = true;
        }


        // Default is True
        public void ModeForm(bool ismode)
        {
            EditRegNo.ReadOnly = ismode;
            EditRegNo.BackColor = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;


            EditCustomerText.ReadOnly = ismode;
            EditCustomerText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            EditModelText.ReadOnly = ismode;
            EditModelText.BackColor = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;
            EditLotText.ReadOnly = ismode;
            EditLotText.BackColor  = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;
            EditNGText.ReadOnly = ismode;
            EditNGText.BackColor = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;
            EditProblemText.ReadOnly = ismode;
            EditProblemText.BackColor = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;

            comboBox1.Enabled = !ismode;
            comboBox1.ForeColor = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;
            selectDepart.Enabled = !ismode;
            selectDepart.BackColor = ismode ? ApplicationColors.PrimaryColor  : ApplicationColors.TextColor;


            Save_btn.Enabled = !ismode;
            Save_btn.BackColor = !ismode ? ApplicationColors.BackgroundColor : ApplicationColors.DisableColor;
            Save_btn.ForeColor = !ismode ? Color.White : Color.DarkGray;

            Editbtn.Enabled = ismode;
            Editbtn.BackColor = ismode ? ApplicationColors.BackgroundColor : ApplicationColors.DisableColor;
            Editbtn.ForeColor = ismode ? Color.WhiteSmoke : Color.DarkGray;

            Modetext.Text = ismode ? "View Mode" : "Edit Mode";
        }
    }
}
