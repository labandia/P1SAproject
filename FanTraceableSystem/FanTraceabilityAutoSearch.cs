using FanTraceableSystem.Data;
using FanTraceableSystem.Interface;
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
        private readonly ITraceable _trac;


        List<TraceableShopOrderModel> data = new List<TraceableShopOrderModel>();
        private List<TracePCBModel> _pcbList = new List<TracePCBModel>();
        public FanTraceabilityAutoSearch(ITraceable trac)
        {
            InitializeComponent();
            _trac = trac;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private async void SaveBtn_Click(object sender, EventArgs e)
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

                bool res = await _trac.AddTraceTransactions(obj, _pcbList);

            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error" + ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
          
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (var add = _pcbList.Any()
               ? new AddPCBShop(_pcbList)   // EDIT MODE
               : new AddPCBShop())          // ADD MODE
            {
                if (add.ShowDialog(this) == DialogResult.OK)
                {
                    _pcbList = add.PCBList;

                    string isAdd = _pcbList.Count > 0 ? $@"({_pcbList.Count})" : ""; 

                    button1.Text = "Add Shop Order " + isAdd;  
                }
            }
        }

        private async void filterbtn_Click(object sender, EventArgs e)
        {
            string filterText = SearchText.Text.Trim();

            var result = await _trac.TraceableShopOrder(
                filterText,
                dateTimePicker2.Checked ? dateTimePicker2.Value.Date : (DateTime?)null,
                dateTimePicker3.Checked ? dateTimePicker3.Value.Date : (DateTime?)null
            );
        }
    }
}
