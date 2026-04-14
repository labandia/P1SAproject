using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddMainRegistration : Form
    {
        private readonly INCR _ncr;
        string selectedImagepath = "";
        public int processSelected = 0;

        BindingList<NCRModels> listdata = new BindingList<NCRModels>();


        public AddMainRegistration(INCR ncr, int process)
        {
            InitializeComponent();
            _ncr = ncr;

            processSelected = process;

            CatSelection.SelectedIndex = 0;
            Issuedbox.SelectedIndex = 0;
            sectionbox.SelectedIndex = 0;
            statscombo.SelectedIndex = 0;

            label8.Text = process == 0 ? "Contents" : "Details";
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            string SaveImageFolder = await UploadServices.SaveImageFolder(selectedImagepath);

            var obj = new NCRModels
            {
                Category = CatSelection.SelectedIndex,
                RegNo = RegNoText.Text,
                DateIssued = DateissuedText.Value,
                SectionID = sectionbox.SelectedIndex + 1,
                ModelNo = ModelText.Text,
                Quantity = string.IsNullOrEmpty(QuanText.Text) ? 0 : int.Parse(QuanText.Text),
                Contents = ContentText.Text,
                DateRegist = DateRegText.Value,
                FilePath = Reporfile.Text,
                CircularStatus = CircularText.Text,
                Status = statscombo.SelectedIndex,
                Process = processSelected,
                UploadImage = SaveImageFolder
            };

            listdata.Add(obj);

            NCRGrid.Height = 469;
            NCRGrid.BringToFront();
            NCRGrid.DataSource = listdata;
            ResetDisplay();
            RegNoText.Focus();  

            Finalizebtn.Enabled = true;
            Finalizebtn.BackColor = Color.FromArgb(25, 131, 230);
            Finalizebtn.ForeColor = Color.White;
        }

        private void AddImagebtn_Click(object sender, EventArgs e)
        {
           
        }

        private void button2_Click(object sender, EventArgs e)
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

        public void ResetDisplay()
        {
            ModelText.Text = "";
            RegNoText.Text = "";
            sectionbox.SelectedIndex = -1;
            ModelText.Text = "";
            QuanText.Text = "";
            ContentText.Text = "";
            Reporfile.Text = "";
            CircularText.Text = "";
            statscombo.SelectedIndex = -1;
        }

        private async void button3_Click(object sender, EventArgs e)
        {
            try
            {

                foreach(var items in listdata)
                {
                    await _ncr.InsertNCRData(items);    
                }

                Finalizebtn.Enabled = false;
                Finalizebtn.BackColor = Color.WhiteSmoke;
                Finalizebtn.ForeColor = Color.WhiteSmoke;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($@"Error Message: {ex.Message}");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;                 // Full path
                string fileName = Path.GetFileName(openFileDialog.FileName); // File name only
                string directory = Path.GetDirectoryName(openFileDialog.FileName); // Folder path

                Reporfile.Text = fullPath;
            }
        }

      

        private void QuanText_KeyPress_1(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !QuanText.Text.Contains("."))) ? false : true; // Allow the character
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
