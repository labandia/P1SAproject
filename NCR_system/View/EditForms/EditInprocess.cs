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

            ReadOnlyText(true);

            if (inp.UploadImage != null && inp.UploadImage != "")
            {
                selectedImagepath = inp.UploadImage;
                pictureBox1.Image = System.Drawing.Image.FromFile(inp.UploadImage);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        public void ReadOnlyText(bool checker)
        {
            // Textboxes
            EmailText.ReadOnly = checker;
            LineText.ReadOnly = checker;
            DefectText.ReadOnly = checker;
            ShopText.ReadOnly = checker;
            QuanText.ReadOnly = checker;
            ProcText.ReadOnly = checker;
            CauseText.ReadOnly = checker;
            reportpath.ReadOnly = checker;

            // Controls (disable when readonly)
            sectionbox.Enabled = !checker;
            Shiftselect.Enabled = !checker;
            p1saSelect.Enabled = !checker;

            Debug.WriteLine($@"Checker : {checker}");

            // Save Button
            button1.Enabled = !checker;
            button1.BackColor = checker
                ? Color.WhiteSmoke
                : Color.FromArgb(25, 131, 230);
            button1.ForeColor = checker
                ? Color.DarkGray
                : Color.White;

            // Edit Button
            Editbtn.Enabled = checker;
            Editbtn.BackColor = checker
                ? Color.WhiteSmoke
                : Color.FromArgb(25, 131, 230);
        }

        private void Shiftselect_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button12_Click(object sender, EventArgs e) => Close();

        private void Save_btn_Click(object sender, EventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void Editbtn_Click(object sender, EventArgs e)
        {
            ReadOnlyText(false);
        }
    }
}
