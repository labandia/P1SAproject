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

namespace NCR_system.View.AddForms
{
    public partial class AddMainRegistration : Form
    {
        public AddMainRegistration()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
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
                Process = 0
            };
        }
    }
}
