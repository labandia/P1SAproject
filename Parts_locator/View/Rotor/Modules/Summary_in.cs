
using System;
using System.Data;
using System.Runtime.Remoting.Contexts;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Parts_locator.Modules
{
    public partial class Summary_in : UserControl
    {
      
        public DataGridView summaryingrid { get { return SummaryTable; } }
        public Label resultcount { get { return Result; } }

        public Summary_in()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            GlobalDb connect = new GlobalDb();
            string query;
            query = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput,  FORMAT(DateInput, 'hh:mm:ss tt') as Timein, " +
                    "ShopOrder, PartNumber, Quantity, Inputby " +
                    "FROM Part_transaction_shoporder_IN " +
                    "WHERE (ShopOrder LIKE '%"+ Partnumtext.Text +"%' OR PartNumber  LIKE '%"+ Partnumtext.Text +"%')";

            DataTable dt = connect.GetData(query);
            SummaryTable.DataSource = dt;
        }

        private void Partnumtext_TextChanged(object sender, EventArgs e)
        {
            GlobalDb connect = new GlobalDb();
            string query;
            query = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput,  FORMAT(DateInput, 'hh:mm:ss tt') as Timein, " +
                    "ShopOrder, PartNumber, Quantity, Inputby " +
                    "FROM Part_transaction_shoporder_IN " + 
                    "WHERE (ShopOrder LIKE '%"+ Partnumtext.Text +"%' OR PartNumber  LIKE '%"+ Partnumtext.Text +"%')";

            DataTable dt = connect.GetData(query);
            SummaryTable.DataSource = dt;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            GlobalDb connect = new GlobalDb();

            string startnow = dstart.Value.ToString("MM/dd/yyyy");
            string endDate = dend.Value.ToString("MM/dd/yyyy");

            string query;
            query = "SELECT FORMAT(DateInput, 'MM/dd/yyyy') as DateInput, " +
                    "FORMAT(DateInput, 'hh:mm:ss tt') as Timein, ShopOrder, PartNumber, Quantity, Inputby " +
                    "FROM Part_transaction_shoporder_IN " +
                    "WHERE  CAST(DateInput AS DATE) between '"+ startnow +"' AND '"+ endDate +"' ";

            DataTable dt = connect.GetData(query);
            SummaryTable.DataSource = dt;
        }
    }
}
