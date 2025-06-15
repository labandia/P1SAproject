using Parts_locator.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Parts_locator.Modules
{
    public partial class summary_out : UserControl
    {
        
        public DataGridView summaryoutgrid { get { return SummaryTableout; } }
        public Label resultcount { get { return Result; } }

        public summary_out()
        {
            InitializeComponent();
        }

       

        

        private void button1_Click(object sender, EventArgs e)
        {
            //Transaction t = new Transaction();
            //string startnow = dstart.Value.ToString("MM/dd/yyyy");
            //string endDate = dend.Value.ToString("MM/dd/yyyy");

            //DataTable dt = t.GetMonitoringOUT(startnow, endDate);;
            //SummaryTableout.DataSource = dt;
        }

        private void summary_out_Load(object sender, EventArgs e)
        {
            
        }
    }
}
