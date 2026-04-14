using NCR_system.Interface;
using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.EditForms
{
    public partial class EditMainRegistration : Form
    {
        private readonly INCR _res;
        public string selectedImagepath = "";

        public readonly int currentRecordID;
        public bool isEditImage = false;

        public EditMainRegistration(MainNCRModel ncr, INCR res)
        {
            InitializeComponent();
            _res = res;



            RegNoText.Text = ncr.RegNo;
            ModelText.Text = ncr.ModelNo;
            CatSelection.SelectedIndex = ncr.Category;
            Issuedbox.SelectedItem = ncr.IssueGroup;
            sectionbox.SelectedIndex = ncr.SectionID;
            QuanText.Text = ncr.Quantity.ToString();
            circulatText.Text = ncr.CircularStatus;
            reportText.Text = ncr.FilePath;
            ContentText.Text = ncr.Contents;

            label19.Text = ncr.Process == 0 ? "NCR Main Registration Details" : "NCR Recurrence Details";
            label9.Text = ncr.Process == 0 ? "Update and preview the NCR Main Registration Details" : "Update and preview the Recurrence Details";   

            if (ncr.UploadImage != null && ncr.UploadImage != "")
            {
                selectedImagepath = ncr.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(ncr.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button12_Click(object sender, EventArgs e) => Close();

        public void LoaddataOnly()
        {
            RegNoText.Enabled = false;
            ModelText.Enabled = false;
            CatSelection.Enabled = false;
            Issuedbox.Enabled = false;
            sectionbox.Enabled = false;
            QuanText.Enabled = false;
            circulatText.Enabled = false;
            reportText.Enabled = false;
            ContentText.Enabled = false;
        }

        private void reportText_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = reportText.Text,
                UseShellExecute = true
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
