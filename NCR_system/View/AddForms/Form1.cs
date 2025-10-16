using System;
using System.Windows.Forms;

namespace NCR_system.View.AddForms
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Save_btn_Click(object sender, EventArgs e)
        {
            var obj = new
            {
                ModelNo = ModelText.Text,
                LotNo = LotText.Text,
                NGQty = NGText.Text,
                Details = ProblemText.Text,
                SectionID = selectDepart.SelectedIndex + 1,
                CCtype = 1
            };
        }
    }
}
