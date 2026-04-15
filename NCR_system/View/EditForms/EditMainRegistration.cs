using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
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

            ReadOnlyText(true);

            label19.Text = ncr.Process == 0 ? "NCR Main Registration Details" : "NCR Recurrence Details";
            label9.Text = ncr.Process == 0 ? "Update and preview the NCR Main Registration Details" : "Update and preview the Recurrence Details";   

            if (ncr.UploadImage != null && ncr.UploadImage != "")
            {
                selectedImagepath = ncr.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(ncr.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(UploadServices.noImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void button12_Click(object sender, EventArgs e) => Close();

      

        private void reportText_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start(new ProcessStartInfo
            {
                FileName = reportText.Text,
                UseShellExecute = true
            });
        }

    

        public void ReadOnlyText(bool ismode)
        {
            // Textboxes
            RegNoText.ReadOnly = ismode;
            ModelText.ReadOnly = ismode;
            CatSelection.Enabled = ismode;
            QuanText.ReadOnly = ismode;
            circulatText.ReadOnly = ismode;

            RegNoText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            ModelText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            CatSelection.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            Issuedbox.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            sectionbox.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            QuanText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            circulatText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            reportText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            ContentText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;

            // Controls (disable when readonly)
            Issuedbox.Enabled = !ismode;
            sectionbox.Enabled = !ismode;
            reportText.Enabled = !ismode;
            ContentText.Enabled = !ismode;

            Debug.WriteLine($@"Checker : {ismode}");


            button1.Enabled = !ismode;
            button1.BackColor = !ismode ? ApplicationColors.BackgroundColor : ApplicationColors.DisableColor;
            button1.ForeColor = !ismode ? Color.White : Color.DarkGray;

            Editbtn.Enabled = ismode;
            Editbtn.BackColor = ismode ? ApplicationColors.BackgroundColor : ApplicationColors.DisableColor;
            Editbtn.ForeColor = ismode ? Color.WhiteSmoke : Color.DarkGray;

            Modetext.Text = ismode ? "View Mode" : "Edit Mode";

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            ReadOnlyText(false);
        }
    }
}
