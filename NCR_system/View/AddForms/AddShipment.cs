using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using NCR_system.View.Module;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddShipment : Form
    {
        private readonly IShipRejected _ship;

        public int _proc;
        BindingList<RejectShipmentModel> listdata = new BindingList<RejectShipmentModel>();
        string selectedImagepath = "";

        public AddShipment(IShipRejected ship, int proc)
        {
            InitializeComponent();
            DisplayPlaceholder();

            bool process = (proc == 0);

            sectionbox.SelectedIndex = 0;

            StatsText.Visible = !process;
            label10.Visible = !process;

            label9.Text = (proc == 0) ? "Create Rejected lot" : "Create Shipment Delay";
            label12.Text = (proc == 0) ? "Provide details about the Rejected Lot" : "Provide details about the Shipment Delay";
            _ship = ship;
            _proc = proc;

            if (listdata.Count == 0)
            {
                button3.Enabled = false;
                button3.BackColor = Color.Gray;
                button3.ForeColor = Color.White;
            }
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
           
        }

       

        private void QuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only one decimal point
            if(e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

            // Only one minus at the beginning
            if (e.KeyChar == '-' && (tb.Text.Contains("-") || tb.SelectionStart > 0))
            {
                e.Handled = true;
            }
        }

        private void AddShipment_Load(object sender, EventArgs e)
        {
          
        }

        

        private void RegNoText_Click(object sender, EventArgs e)
        {
            RegNoText.Text = "";
            RegNoText.ForeColor = Color.Black;
        }
        private void ModelText_Click(object sender, EventArgs e)
        {
            ModelText.Text = "";
            ModelText.ForeColor = Color.Black;
        }
        private void QuanText_Click(object sender, EventArgs e)
        {
            QuanText.Text = "";
            QuanText.ForeColor = Color.Black;
        }
        private void ContentText_Click(object sender, EventArgs e)
        {
            ContentText.Text = "";
            ContentText.ForeColor = Color.Black;
        }
       

        public void DisplayPlaceholder()
        {
            ModelText.Text = "Enter Model No.";
            ModelText.ForeColor = Color.Gray;

            RegNoText.Text = "Enter Registration Forms...";
            RegNoText.ForeColor = Color.Gray;

            QuanText.Text = "Enter Quantity...";
            QuanText.ForeColor = Color.Gray;

            ContentText.Text = "Enter Contents...";
            ContentText.ForeColor = Color.Gray;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Close();
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

        private async void button2_Click(object sender, EventArgs e)
        {
            string ImageUpload = await UploadServices.SaveImageFolder(selectedImagepath);

            var obj = new RejectShipmentModel
            {
                RegNo = InputHelper.IsTextEmpty(RegNoText) ? "" : RegNoText.Text,
                DateIssued = DateissuedText.Text,
                IssueGroup = Issuedbox.Text,
                SectionID = sectionbox.SelectedIndex,
                ModelNo = string.IsNullOrEmpty(ModelText.Text) ? "" : ModelText.Text,
                Quantity = Convert.ToInt32(QuanText.Text),
                Contents = string.IsNullOrEmpty(ContentText.Text) ? "" : ContentText.Text,
                DateCloseReg = DateRegText.Text,
                Status = (_proc == 0) ? 1 :  StatsText.SelectedIndex,
                UploadImage = ImageUpload
            };

            listdata.Add(obj);

            RejectedGrid.DataSource = listdata;
            ResetDisplay();
            ModelText.Focus();

            button3.Enabled = true;
            button3.BackColor = Color.FromArgb(95, 34, 200);
        }


        public void ResetDisplay()
        {
            RegNoText.Text = "";
            DateissuedText.Text = "";
            Issuedbox.Text = "";
            ModelText.Text = "";
            QuanText.Text = "";
            DateRegText.Text = "";
            ContentText.Text = "";
            StatsText.Text = "";

            sectionbox.SelectedIndex = 0;   
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in listdata)
                {
                    await _ship.InsertShipRejectData(item, _proc);
                }

                MessageBox.Show("Data saved successfully.");

                button3.Enabled = false;
                button3.BackColor = Color.Gray;
                button3.ForeColor = Color.White;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
