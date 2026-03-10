using NCR_system.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
        public AddInprocess()
        {
            InitializeComponent();
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

                MessageBox.Show("Full Path: " + fullPath +
                                "\nFile Name: " + fileName +
                                "\nFolder: " + directory);
            }
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new InprocessModel
                {
                    TitleEmail = string.IsNullOrEmpty(EmailText.Text) ? "" : EmailText.Text,
                    DateEncounter = DateEncount.Text,
                    Shift = Shiftselect.SelectedIndex,
                    Line = LineText.Text,
                    Defect = DefectText.Text,
                    Model = ModelText.Text,
                    ShopOrder = ShopText.Text, 
                    NGQty = string.IsNullOrEmpty(QuanText.Text) ? 0 : int.Parse(QuanText.Text),
                    ProcEncounter = ProcText.Text,
                    cause = CauseText.Text,
                    Invest = reportpath.Text

                };
            }catch(Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
