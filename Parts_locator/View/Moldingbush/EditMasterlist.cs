using Parts_locator.Interface;
using Parts_locator.View.Moldingbush.Modules;
using System;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class EditMasterlist : Form
    {
        private readonly IRawMats _raw;
        private readonly BushMasterlist _bm;
        public int prodID {  get; set; }
        public int selectedindex { get; set; }  
       

        public EditMasterlist(BushMasterlist bm, IRawMats raw)
        {
            InitializeComponent();
            _bm = bm;   
            _raw = raw;
        }

        private void button2_Click(object sender, EventArgs e) => Visible = false;
        private async void button1_Click(object sender, EventArgs e)
        {
            string partnum = String.IsNullOrEmpty(Partnum.Text) ? "" : Partnum.Text.Trim();
            int qty = Int32.Parse(QuanText.Text);
            int rack = Int32.Parse(RacksText.Text);

            bool result = await _raw.EditMasterlist(partnum, qty, rack);
          
            if (result)
            {
                MessageBox.Show("Update successfully");
                _bm.UpdateDisplayTable();
                Visible = false;
            }

        }
    }
}
