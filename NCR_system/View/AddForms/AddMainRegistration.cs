using NCR_system.Models;
using NCR_system.Utilities;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddMainRegistration : Form
    {
        string selectedImagepath = "";

        public AddMainRegistration()
        {
            InitializeComponent();
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            string SaveImageFolder = await UploadServices.SaveImageFolder(selectedImagepath);
            
            var obj = new NCRModels
            {
                Category = CatSelection.SelectedText,
                RegNo = RegNoText.Text,
                DateIssued = DateissuedText.Text,
                SectionID = sectionbox.SelectedIndex + 1,
                ModelNo = ModelText.Text,
                Quantity = string.IsNullOrEmpty(QuanText.Text) ? 0 : int.Parse(QuanText.Text),
                Contents = ContentText.Text,
                DateRegist = DateRegText.Text,
                FilePath = Reporfile.Text,
                DateCloseReg = Datecleared.Text,
                CircularStatus = selectCircular.Text,
                TargetDate = TargetText.Text,
                Status = statscombo.SelectedIndex,
                Process = 0, 
                UploadImage = SaveImageFolder
            };
        }

        private void AddImagebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagepath = ofd.FileName;
                pictureBox1.Image = Image.FromFile(selectedImagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }
    }
}
