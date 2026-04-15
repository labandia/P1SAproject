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

namespace NCR_system.View.EditForms
{
    public partial class EditInprocess : Form
    {
        private readonly IInprocess _pro;
        public string selectedImagepath = "";
        public string currentImagePath = "";
        public readonly int currentRecordID;

        public bool isUpload = false;
        public bool isEditImage = false;

        private readonly string[] SectionName =
       {
            "P1SA-M", "P1SA-P", "P1SA-R", "P1SA-W", "P1SA-C", "P1SA-PC"
        };

        public EditInprocess(InprocessModel inp, IInprocess pro)
        {
            InitializeComponent();
            _pro = pro;

            currentRecordID = inp.RecordID;
            EmailText.Text = inp.TitleEmail;
            Shiftselect.SelectedIndex = inp.Shift;

            LineText.Text = inp.Line;
            DefectText.Text = inp.Defect;
            ModelText.Text = inp.Model;
            ShopText.Text = inp.ShopOrder;
            QuanText.Text = inp.NGQty.ToString();
            ProcText.Text = inp.ProcEncounter;
            CauseText.Text = inp.cause;
            reportpath.Text = inp.Invest;
            p1saSelect.SelectedIndex = inp.Status;
            //remarksText.Text = inp.Remarks;
            sectionbox.SelectedIndex = inp.SectionID;
            Shiftselect.SelectedIndex = inp.Shift;
            AddImagebtn.Enabled = false;

            ReadOnlyText(true);

            if (inp.UploadImage != null && inp.UploadImage != "")
            {
                selectedImagepath = inp.UploadImage;
                currentImagePath = inp.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(inp.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
            else
            {
                pictureBox1.Image = System.Drawing.Image.FromFile(UploadServices.noImagePath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public void ReadOnlyText(bool ismode)
        {
            // Textboxes
            EmailText.ReadOnly = ismode;
            ModelText.ReadOnly = ismode;
            LineText.ReadOnly = ismode;
            DefectText.ReadOnly = ismode;
            ShopText.ReadOnly = ismode;
            QuanText.ReadOnly = ismode;
            ProcText.ReadOnly = ismode;
            CauseText.ReadOnly = ismode;
            reportpath.ReadOnly = ismode;


            EmailText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            ModelText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            LineText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            DefectText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            ShopText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            QuanText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            ProcText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            CauseText.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;
            reportpath.BackColor = ismode ? ApplicationColors.PrimaryColor : ApplicationColors.TextColor;

            // Controls (disable when readonly)
            sectionbox.Enabled = !ismode;
            Shiftselect.Enabled = !ismode;
            p1saSelect.Enabled = !ismode;

            editselectfile.Enabled = !ismode;
            AddImagebtn.Enabled = !ismode;

            button1.Enabled = !ismode;
            button1.BackColor = !ismode ? ApplicationColors.BackgroundColor : ApplicationColors.DisableColor;
            button1.ForeColor = !ismode ? Color.White : Color.DarkGray;

            Editbtn.Enabled = ismode;
            Editbtn.BackColor = ismode ? ApplicationColors.BackgroundColor : ApplicationColors.DisableColor;
            Editbtn.ForeColor = ismode ? Color.WhiteSmoke : Color.DarkGray;

            Modetext.Text = ismode ? "View Mode" : "Edit Mode";

        }

        private void Shiftselect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e) => Close();

        private void Save_btn_Click(object sender, EventArgs e)
        {

        }

        private async void button1_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new InprocessModel
                {
                    TitleEmail = EmailText.Text,
                    Model = ModelText.Text,
                    Line = LineText.Text,
                    Defect = DefectText.Text,
                    ShopOrder = ShopText.Text,
                    NGQty = int.TryParse(QuanText.Text, out int qty) ? qty : 0,
                    ProcEncounter = ProcText.Text,
                    cause = CauseText.Text,
                    Invest = reportpath.Text,
                    Remarks = remarksText.Text,
                    UploadImage = selectedImagepath,
                    RecordID = currentRecordID
                };


                bool result = await _pro.UpdateInprocessData(obj);

                if (!result)
                {
                    MessageBox.Show("Failed to update record.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (isUpload) DeleteFileExcel(currentImagePath);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error saving record: {ex.Message}");
            }
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            ReadOnlyText(false);
        }

        private void QuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar)) return;

            e.Handled = (char.IsDigit(e.KeyChar) || (e.KeyChar == '.' && !QuanText.Text.Contains("."))) ? false : true; // Allow the character
        }

        private void editselectfile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Excel Files (*.xlsx;*.xls)|*.xlsx;*.xls";

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                string fullPath = openFileDialog.FileName;                 // Full path
                string fileName = Path.GetFileName(openFileDialog.FileName); // File name only
                string directory = Path.GetDirectoryName(openFileDialog.FileName); // Folder path


                reportpath.Text = "";
                reportpath.Text = fullPath;
            }
        }

        private void AddImagebtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                isUpload = true;
                selectedImagepath = ofd.FileName;
                pictureBox1.Image = Image.FromFile(selectedImagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }


        public void DeleteFileExcel(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    MessageBox.Show("File deleted successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error deleting file: {ex.Message}");
            }

        }
    }
}
