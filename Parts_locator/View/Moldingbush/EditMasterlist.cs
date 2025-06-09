using Parts_locator.Models;
using Parts_locator.View.Moldingbush.Modules;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.View.Moldingbush
{
    public partial class EditMasterlist : Form
    {
        private readonly BushMasterlist _bm;
        public int prodID {  get; set; }
        public int selectedindex { get; set; }  
       

        public EditMasterlist(BushMasterlist bm)
        {
            InitializeComponent();
            _bm = bm;   
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Visible = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            BushProducts b = new BushProducts();
          
            if (b.EditMasterlist(Partnum.Text, Int32.Parse(QuanText.Text), Int32.Parse(RacksText.Text)))
            {
                MessageBox.Show("Update successfully");
                _bm.UpdateDisplayTable(selectedindex);
                Visible = false;
            }

        }
    }
}
