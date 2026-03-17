using MSDMonitoring.Data;
using NCR_system.Interface;
using NCR_system.Models;
using NCR_system.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NCR_system.View.Details
{
    public partial class InprocessDetails : Form
    {
        private readonly IInprocess _pro;
        public string selectedImagepath = "";
        public readonly int currentRecordID;

        public bool isEditImage = false;

        private readonly string[] SectionName =
        {
            "P1SA-M", "P1SA-P", "P1SA-R", "P1SA-W", "P1SA-C", "P1SA-PC"
        };

        public InprocessDetails(InprocessModel inp, IInprocess pro)
        {
            InitializeComponent();
            _pro = pro; 

            currentRecordID = inp.RecordID;
            EmailText.Text = inp.TitleEmail;
            Shiftselect.SelectedIndex =  inp.Shift;
            LineText.Text = inp.Line;
            DefectText.Text = inp.Defect;
            ModelText.Text = inp.Model;
            ShopText.Text = inp.ShopOrder;
            QuanText.Text = inp.NGQty.ToString();
            ProcText.Text = inp.ProcEncounter;
            CauseText.Text = inp.cause;
            reportpath.Text = inp.Invest;
            p1saSelect.SelectedIndex = inp.Status;
            remarksText.Text = inp.Remarks;
            sectionbox.SelectedIndex = inp.SectionID - 1;
            ReadOnlyText();

            if (inp.UploadImage != null && inp.UploadImage != "")
            {
                selectedImagepath = inp.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(inp.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }

        }

        private void button12_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Finalizebtn_Click(object sender, EventArgs e)
        {
            Cancel_btn.Visible = true;
            Save_btn.Visible = true;
            button2.Visible = true;
            Deletebtn.Visible = true;   
            Finalizebtn.Visible = false;
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

        private async void Save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                string imagepath = await UploadServices.SaveImageFolder(selectedImagepath);
                DateTime date = DateEncount.Value;

                var obj = new InprocessModel
                {
                    RecordID = currentRecordID, 
                    TitleEmail = InputHelper.GetText(EmailText),
                    Shift = Shiftselect.SelectedIndex,
                    Line = InputHelper.GetText(LineText),
                    Defect = InputHelper.GetText(DefectText),
                    Model = InputHelper.GetText(ModelText),
                    ShopOrder = InputHelper.GetText(ShopText),
                    NGQty = InputHelper.IsTextEmpty(QuanText) ? 0 : int.Parse(QuanText.Text),
                    ProcEncounter = InputHelper.GetText(ProcText),
                    cause = InputHelper.GetText(CauseText),
                    Invest = InputHelper.GetText(reportpath),
                    SectionDep = SectionName[sectionbox.SelectedIndex],
                    SectionID = sectionbox.SelectedIndex + 1,
                    UploadImage = isEditImage ? imagepath : selectedImagepath,
                    P1saStatus = p1saSelect.SelectedIndex,
                    Remarks = InputHelper.GetText(remarksText)
                };

                bool result = await _pro.UpdateInprocessData(obj);

                if (result)
                {
                    MessageBox.Show("Data updated successfully.");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            DialogResult exit = MessageBox.Show("Are you sure you want to delete this Attendance", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (exit == DialogResult.Yes)
            {
                bool result = await _pro.DeleteInprocessData(currentRecordID);
                if (result)
                {
                     DialogResult = DialogResult.OK;
                    Close();
                }
            }
        }



        public void ReadOnlyText()
        {
            EmailText.ReadOnly = true;   
            LineText.ReadOnly = true;
            DefectText.ReadOnly = true;
            ModelText.ReadOnly = true;
            ShopText.ReadOnly = true;
            QuanText.ReadOnly = true;
            ProcText.ReadOnly = true;
            CauseText.ReadOnly = true;
            reportpath.ReadOnly = true;
            remarksText.ReadOnly = true;
            

            DateEncount.Enabled = false;    
            Shiftselect.Enabled=false;
            sectionbox.Enabled = false;
            p1saSelect.Enabled = false;

            foldbtrn.Enabled = false;
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {
            ReadOnlyText();
            Finalizebtn.Visible = true;
            Cancel_btn.Visible = false;
            Save_btn.Visible = false;
            Deletebtn.Visible = false;
        }
    }
}
