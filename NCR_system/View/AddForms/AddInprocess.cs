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
using static System.Net.Mime.MediaTypeNames;

namespace NCR_system.View.AddForms
{
    public partial class AddInprocess : Form
    {
        private readonly IInprocess _pro;
        public string selectedImagepath = "";

        BindingList<InprocessModel> listdata = new BindingList<InprocessModel>();


        private readonly string[] SectionName =
        {
            "P1SA-M", "P1SA-P", "P1SA-R", "P1SA-W", "P1SA-C", "P1SA-PC"
        };

        public AddInprocess(IInprocess pro)
        {
            InitializeComponent();
            _pro = pro; 

            if(listdata.Count == 0)
            {
                Finalizebtn.Enabled = false;    
                Finalizebtn.BackColor = Color.WhiteSmoke;
                Finalizebtn.ForeColor = Color.WhiteSmoke;
            }

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
                DateTime date = DateEncount.Value;
                //string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");

                var obj = new InprocessModel
                {
                    TitleEmail = InputHelper.GetText(EmailText),
                    DateEncounter = date,
                    Shift = Shiftselect.SelectedIndex,
                    Line = InputHelper.GetText(LineText),
                    Defect = InputHelper.GetText(DefectText),
                    Model = InputHelper.GetText(ModelText),
                    ShopOrder = InputHelper.GetText(ShopText),
                    NGQty = string.IsNullOrEmpty(QuanText.Text) ? 0 : int.Parse(QuanText.Text),
                    ProcEncounter = InputHelper.GetText(ProcText),
                    cause = InputHelper.GetText(CauseText),
                    Invest = InputHelper.GetText(reportpath),
                    SectionDep = SectionName[sectionbox.SelectedIndex],
                    SectionID = sectionbox.SelectedIndex + 1,
                    UploadImage = imagepath,
                    P1saStatus = p1saSelect.SelectedIndex,
                    Remarks = InputHelper.GetText(remarksText)
                };

                listdata.Add(obj);

                InprocessGrid.Columns["RecordID"].Visible = false;
                InprocessGrid.Height = 469;
                InprocessGrid.DataSource = listdata;
                InprocessGrid.BringToFront();


                ResetDisplay();
                EmailText.Focus();

                Finalizebtn.Enabled = true;
                Finalizebtn.BackColor = Color.FromArgb(95, 34, 200);
                Finalizebtn.ForeColor = Color.White;

            } catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            //try
            //{
            //    string imagepath = await UploadServices.SaveImageFolder(selectedImagepath);
            //    DateTime date = DateEncount.Value;
            //    string formattedDate = date.ToString("yyyy-MM-dd HH:mm:ss.fff");


            //    var obj = new InprocessModel
            //    {
            //        TitleEmail = InputHelper.GetText(EmailText),
            //        DateEncounter = formattedDate,
            //        Shift = Shiftselect.SelectedIndex,
            //        Line = InputHelper.GetText(LineText),
            //        Defect = InputHelper.GetText(DefectText),
            //        Model = InputHelper.GetText(ModelText),
            //        ShopOrder = InputHelper.GetText(ShopText),
            //        NGQty = string.IsNullOrEmpty(QuanText.Text) ? 0 : int.Parse(QuanText.Text),
            //        ProcEncounter = InputHelper.GetText(ProcText),
            //        cause = InputHelper.GetText(CauseText),  
            //        Invest = InputHelper.GetText(reportpath),
            //        SectionDep = SectionName[sectionbox.SelectedIndex],
            //        SectionID =  sectionbox.SelectedIndex + 1,
            //        UploadImage = imagepath,
            //        P1saStatus = p1saSelect.SelectedIndex,
            //        Remarks = InputHelper.GetText(remarksText)
            //    };


            //    bool result = await _pro.InsertInprocessData(obj);

            //    if (result)
            //    {
            //        MessageBox.Show("Data saved successfully.");
            //        DialogResult = DialogResult.OK;
            //        Close();
            //    }

            //}
            //catch(Exception ex)
            //{
            //    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                selectedImagepath = ofd.FileName;
                pictureBox1.Image = System.Drawing.Image.FromFile(selectedImagepath);
                pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void InprocessGrid_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void InprocessGrid_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (InprocessGrid.Columns[e.ColumnIndex].Name == "Shift")
            {
                int checkshift = (int)e.Value;

                e.Value = (checkshift == 0) ? "DS" : "NS";
            }
        }

        private async void Finalizebtn_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (var item in listdata)
                {
                    await _pro.InsertInprocessData(item);
                }

                Finalizebtn.Enabled = false;
                Finalizebtn.BackColor = Color.WhiteSmoke;
                Finalizebtn.ForeColor = Color.WhiteSmoke;

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public void ResetDisplay()
        {
            EmailText.Text = "";
            Shiftselect.SelectedIndex = 0;
            LineText.Text = "";
            DefectText.Text = "";
            ModelText.Text = "";
            ShopText.Text = "";
            QuanText.Text = "";
            ProcText.Text = "";
            CauseText.Text = "";
            reportpath.Text = "";
            selectedImagepath = ""; 
            p1saSelect.SelectedIndex = 0;
            remarksText.Text = "";

            // Scroll panel to top
            panel1.VerticalScroll.Value = 0;
            panel1.PerformLayout();
        }

        private void QuanText_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.Contains("."))
            {
                e.Handled = true;
            }

            // Only one minus at the beginning
            if (e.KeyChar == '-' && (tb.Text.Contains("-") || tb.SelectionStart > 0))
            {
                e.Handled = true;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Cancel_btn_Click(object sender, EventArgs e)
        {

        }

        private void AddInprocess_Load(object sender, EventArgs e)
        {

        }
    }
}
