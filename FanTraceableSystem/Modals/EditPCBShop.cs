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
    public partial class EditPCBShop : Form
    {
        public BindingList<TraceableSubAssyModel> subassy = new BindingList<TraceableSubAssyModel>();

        // Constructor for EDIT
        public EditPCBShop(BindingList<TraceableSubAssyModel> existingList)
        {
            InitializeComponent();

            // clone to avoid direct reference issues (recommended)
            subassy = new BindingList<TraceableSubAssyModel>(
                existingList.Select(x => new TraceableSubAssyModel
                {
                    SubAssyID = x.SubAssyID,
                    ShopOrder = x.ShopOrder,
                    PreparedQuantity = x.PreparedQuantity,
                    LotNo = x.LotNo,
                    Line = x.Line,
                    SubAssyIssued = x.SubAssyIssued,
                    Rev = x.Rev,
                    isAction = 1 // 👈 mark existing as EDIT by default (important)
                }).ToList()
            );
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(Shoptext.Text))
            {
                MessageBox.Show("PCB Shop Order is required");
                return;
            }


            // Example: collect values from controls
            subassy.Add(new TraceableSubAssyModel
            {
                ShopOrder = Shoptext.Text,
                PreparedQuantity = int.TryParse(QuanText.Text, out int qty) ? qty : 0,
                Rev = RevText.Text,
                LotNo = LotNotext.Text,
                Line = LineText.Text,
                SubAssyIssued = IssuerText.Text,
                isAction = 0
            });
            dataGridView1.DataSource = subassy.ToList(); // Refresh the grid    

            QuanText.Text = "";
            Shoptext.Text = "";
            LotNotext.Text = "";
            RevText.Text = "";
            LineText.Text = "";
            IssuerText.Text = "";

            Shoptext.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void SaveBtn_Click(object sender, EventArgs e)
        {


            //foreach (DataGridViewRow row in dataGridView1.Rows)
            //{
            //    if (row.IsNewRow) continue;


            //    subassy.Add(new TraceableSubAssyModel
            //    {
            //        SubAssyID = int.TryParse(row.Cells["SubAssyID"].Value?.ToString(), out int id) ? id : 0,
            //        Rev = row.Cells["Rev"].Value?.ToString(),
            //        ShopOrder = row.Cells["ShopOrder"].Value?.ToString(),
            //        SubAssyIssued = row.Cells["SubAssyIssued"].Value?.ToString(),
            //        LotNo = row.Cells["LotNo"].Value?.ToString(),
            //        Line = row.Cells["Line"].Value?.ToString(),
            //        PreparedQuantity = int.TryParse(row.Cells["PreparedQuantity"].Value?.ToString(), out int qty) ? qty : 0,
            //        isAction = int.TryParse(row.Cells["IsAction"].Value?.ToString(), out int act) ? act : 0,
            //    });
            //}

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void EditPCBShop_Load(object sender, EventArgs e)
        {
            dataGridView1.AutoGenerateColumns = true;

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = subassy;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void RevText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            var row = dataGridView1.Rows[e.RowIndex].DataBoundItem as EditTracePCBModel;
            if (row == null) return;

            if (row.isAction == 0)
            {
                // newly added → keep as ADD
                return;
            }

            // existing row → mark as edited
            row.isAction = 1;
        }

        private void dataGridView1_CurrentCellDirtyStateChanged(object sender, EventArgs e)
        {
            if (dataGridView1.IsCurrentCellDirty)
            {
                dataGridView1.CommitEdit(DataGridViewDataErrorContexts.Commit);
            }
        }

        private void LineText_TextChanged(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }
    }
}
