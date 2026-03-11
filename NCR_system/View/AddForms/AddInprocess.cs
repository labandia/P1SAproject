using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class AddInprocess : Form
    {
        private readonly IInprocess _pro;
        public string selectedImagepath = "";


        private readonly string[] SectionName =
        {
            "P1SA-M", "P1SA-P", "P1SA-R", "P1SA-W", "P1SA-C", "P1SA-PC"
        };

        public AddInprocess(IInprocess pro)
        {
            InitializeComponent();
            _pro = pro; 
            Shiftselect.SelectedIndex = 0;  
            sectionbox.SelectedIndex = 0;
        }

        private void AddImagebtn_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void foldbtrn_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;                 // Full path
                string fileName = Path.GetFileName(openFileDialog.FileName); // File name only
                string directory = Path.GetDirectoryName(openFileDialog.FileName); // Folder path

                reportpath.Text = fullPath;
                //MessageBox.Show("Full Path: " + fullPath +
                //                "\nFile Name: " + fileName +
                //                "\nFolder: " + directory);
            }
        }

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string imagepath = await UploadServices.SaveImageFolder(selectedImagepath);
                DateTime date = dateTimePicker1.Value;
                string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");


                var obj = new InprocessModel
                {
                    TitleEmail = string.IsNullOrEmpty(EmailText.Text) ? "" : EmailText.Text,
                    DateEncounter = formattedDate,
                    Shift = Shiftselect.SelectedIndex,
                    Line = LineText.Text,
                    Defect = DefectText.Text,
                    Model = ModelText.Text,
                    ShopOrder = ShopText.Text,
                    NGQty = string.IsNullOrEmpty(QuanText.Text) ? 0 : int.Parse(QuanText.Text),
                    ProcEncounter = ProcText.Text,
                    cause = CauseText.Text,
                    Invest = reportpath.Text,
                    SectionDep = SectionName[sectionbox.SelectedIndex],
                    SectionID = sectionbox.SelectedIndex + 1,
                    UploadImage = imagepath,
                    P1saStatus = p1saSelect.SelectedIndex
                };

                Debug.WriteLine("InprocessModel Object:" + obj.SectionID);

                //bool result = await _pro.InsertInprocessData(obj);

                //if (result)
                //{
                //    MessageBox.Show("Data saved successfully.");
                //    DialogResult = DialogResult.OK;
                //    Close();
                //}

            }
            catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)
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
