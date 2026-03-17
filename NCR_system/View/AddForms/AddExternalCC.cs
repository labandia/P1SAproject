using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using NCR_system.View.Module;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddExternalCC : Form
    {
        private readonly ICustomerComplaint _cus;

        string selectedImagepath = "";

        BindingList<CustomerModel> listdata = new BindingList<CustomerModel>();

        public AddExternalCC(ICustomerComplaint cus)
        {
            InitializeComponent();
            _cus = cus;

            if (listdata.Count == 0)
            {
                button3.Enabled = false;
                button3.BackColor = Color.Gray;
                button3.ForeColor = Color.White;
            }
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
                NGQty = Convert.ToInt32(EditNGText.Text),
                Details = EditProblemText.Text,
                Status = 1,
                CCtype = 0,
                UploadImage = ImageUpload
            };

            listdata.Add(obj);
            CustomDatagrid.DataSource = listdata;
            ResetDisplay();
            EditRegNo.Focus();

            button3.Enabled = true;
            button3.BackColor = Color.FromArgb(95, 34, 200);
            button3.ForeColor = Color.White;
          
        }

        public void ResetDisplay()
        {
            EditRegNo.Text = "";
            EditCustomerText.Text = "";
            EditLotText.Text = "";
            EditNGText.Text = "";
            EditProblemText.Text = "";
            EditModelText.Text = "";
            selectDepart.SelectedIndex = 0;

            EditRegNo.Focus();
        }

        private void AddExternalCC_Load(object sender, EventArgs e)
        {
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
                string.IsNullOrWhiteSpace(EditModelText.Text) ||
                selectDepart.SelectedIndex == 0)
            {
                MessageBox.Show("Please fill in all required fields.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void AddImagebtn_Click(object sender, EventArgs e)
        {
            
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void EditNGText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !EditNGText.Text.Contains("."))) ? false : true; // Allow the character
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagepath = ofd.FileName;
                pictureBox2.Image = Image.FromFile(selectedImagepath);
                pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in listdata)
                {
                    await _cus.InsertCustomerData(item, 0);
                }

                button3.Enabled = false;
                button3.BackColor = Color.Gray;
                button3.ForeColor = Color.White;

                MessageBox.Show("Add Data Successfully");
                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CustomDatagrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (CustomDatagrid.Columns[e.ColumnIndex].Name == "SectionID")
            {
                var sectionMap = new Dictionary<int, string>
                {
                    {1, "P1SA MOLDING"},
                    {2, "P1SA PRESS"},
                    {3, "P1SA ROTOR"},
                    {4, "P1SA WINDING"},
                    {5, "P1SA CIRCUIT"}
                };

                if (e.Value != null && int.TryParse(e.Value.ToString(), out int sectionID))
                {
                    if (sectionMap.TryGetValue(sectionID, out string sectionName))
                    {
                        e.Value = sectionName;
                        e.FormattingApplied = true;
                    }
                }
            }
        }
    }
}
