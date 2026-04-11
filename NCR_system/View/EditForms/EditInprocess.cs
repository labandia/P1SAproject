using NCR_system.Interface;
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

namespace NCR_system.View.EditForms
{
    public partial class EditInprocess : Form
    {
        private readonly IInprocess _pro;
        public string selectedImagepath = "";
        public readonly int currentRecordID;

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
            sectionbox.SelectedIndex = inp.SectionID - 1;
            editselectfile.Visible = false;

            ReadOnlyText();

            if (inp.UploadImage != null && inp.UploadImage != "")
            {
                selectedImagepath = inp.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(inp.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public void ReadOnlyText()
        {
            EmailText.ReadOnly = true;
            LineText.ReadOnly = true;
            DefectText.ReadOnly = true;
            LineText.ReadOnly = true;
            ShopText.ReadOnly = true;
            QuanText.ReadOnly = true;
            ProcText.ReadOnly = true;
            CauseText.ReadOnly = true;
            reportpath.ReadOnly = true;
            //remarksText.ReadOnly = true;


            //DateEncount.Enabled = false;
            Shiftselect.Enabled = false;
            sectionbox.Enabled = false;
            p1saSelect.Enabled = false;

            //foldbtrn.Enabled = false;
        }

        private void Shiftselect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e) => Close();
    }
}
