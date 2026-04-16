using FanTraceableSystem.Data;
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

namespace FanTraceableSystem
{
    public partial class FanTraceabilityAutoSearch : Form
    {
        //public int 

        List<TraceableShopOrderModel> data = new List<TraceableShopOrderModel>();

        public FanTraceabilityAutoSearch()
        {
            InitializeComponent();
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var obj = new TraceableShopOrderModel
                {
                    FinalShopOrder = Shoptext.Text,
                    PCBShopOrder = PCBText.Text,
                    Revision = RevText.Text,
                    DatePrepared = DatePrepared.Value,
                    Shift = 0,
                    TimeInput = DatePrepared.Value,
                    Customer = CustomerText.Text,
                    LotNo = LotText.Text,
                    CardCaseNo = Cardtext.Text,
                    Remarks = RemarkText.Text,
                    PCBIncharge = PCBtextcharge.Text,
                    PCBIssuer = PCBIssuerText.Text
                };
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Ignore header double-click
            if (e.RowIndex < 0)
                return;

            using (var add = new AddPCBShop())
            {
                if (add.ShowDialog(this) == DialogResult.OK)
                {
                    List<TracePCBModel> result = add.PCBList;

                    // Example usage
                    foreach (var item in result)
                    {
                        Console.WriteLine($"Order: {item.PCBShopOrder}, Qty: {item.Quantity}");
                    }

                    // OPTIONAL: bind to DataGridView
                    dataGridView1.DataSource = null;
                    dataGridView1.DataSource = result;
                }
            }
        }
    }
}
