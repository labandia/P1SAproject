using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Media.Media3D;
using NCR_system.Utilities;
using NCR_system.Interface;

namespace NCR_system.View.Details
{
    public partial class SDCDetails : Form
    {
        private readonly ICustomerComplaint _cus;
        public string selectedImagepath = "";
        public bool isEditImage = false;
        public readonly int currentRecordID;

        public SDCDetails(CustomerModel cus, ICustomerComplaint cust)
        {
            InitializeComponent();
            _cus = cust;
            currentRecordID = cus.RecordID;
            ModelText.Text = cus.ModelNo;
            LotText.Text = cus.LotNo;
            NGText.Text = cus.NGQty.ToString();
            ProblemText.Text = cus.Details;
            selectDepart.SelectedIndex = cus.SectionID;


            if (cus.UploadImage != null && cus.UploadImage != "")
            {
                selectedImagepath = cus.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(cus.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Finalizebtn_Click(object sender, EventArgs e)
        {
            Cancel_btn.Visible = true;
            Save_btn.Visible = true;
            button2.Visible = true;
            Deletebtn.Visible = true;
            Finalizebtn.Visible = false;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {

        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string imagepath = await UploadServices.SaveImageFolder(selectedImagepath);

                var obj = new CustomerModel
                {
                    RecordID = currentRecordID,
                    ModelNo = ModelText.Text,
                    LotNo = LotText.Text,
                    NGQty = Convert.ToInt32(NGText.Text),
                    Details = ProblemText.Text,
                    SectionID = selectDepart.SelectedIndex + 1,
                    UploadImage = isEditImage ? imagepath : selectedImagepath
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

        private void NGText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !NGText.Text.Contains("."))) ? false : true; // Allow the character
        }
    }
}
